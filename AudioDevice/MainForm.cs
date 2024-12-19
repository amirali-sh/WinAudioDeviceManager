using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using Windows.System;
using WinRT;

namespace AudioDevice
{
    public partial class MainForm : Form
    {
        // Types
        public enum ShortcutButtonState
        {
            Idle = 0,
            Recording
        }

        // Imported functions 
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // Windows codes
        private const int WM_HOTKEY = 0x0312;
        private const int MOD_ALT = 0x1;
        private const int MOD_CONTROL = 0x2;
        private const int MOD_SHIFT = 0x4;
        private const int MOD_WIN = 0x8;

        // Fields
        AudioDeviceUtility _audioDeviceUtility;
        private ShortcutButtonState _shortcutButtonState = ShortcutButtonState.Idle;
        private string[] _shortcutButtonLabels = { "Update Device Switch Shortcut Key", "Save Shortcut Key" };
        private Keys _shortcutButtonKeys = Keys.None;
        private Keys _shortcutModifierKeys = Keys.None;
        private Keys _temp_shortcutButtonKeys = Keys.None;
        private Keys _temp_shortcutModifierKeys = Keys.None;
        private const int _hotKeyID = 1;

        private UserSettings _userSettings;
        private const string _settingsFileName = "AudioDevicesSetting.xml";

        // Constructors
        public MainForm()
        {
            InitializeComponent();

            // audio devices
            _audioDeviceUtility = new AudioDeviceUtility();
            _audioDeviceUtility.AudioDevicesChanged += _audioDeviceUtility_AudioDevicesChanged;
            lb_AudioDevices.DataSourceChanged += Lb_AudioDevices_DataSourceChanged;
            lb_AudioDevices.DataSource = _audioDeviceUtility.AudioDeviceNames;
            lb_AudioDevices.Click += Lb_AudioDevices_Click;

            int activeIndex = _audioDeviceUtility.GetActiveDeviceIndex();
            lb_AudioDevices.SetItemChecked(activeIndex, true);
            lb_AudioDevices.SelectedIndex = activeIndex;

            // shortcut keys
            KeyDown += MainForm_KeyDown;
            FormClosing += MainForm_FormClosing;
            btn_UpdateDeviceSwitchShortcut.Text = _shortcutButtonLabels[(int)_shortcutButtonState];

            // settings
            _userSettings = new UserSettings(_settingsFileName);
            _userSettings.LoadSettings();
            _shortcutButtonKeys = _userSettings.ShortcutKeys;
            _shortcutModifierKeys = _userSettings.ModifierKeys;
            _UpdateModifierKeyText(_shortcutButtonKeys, _shortcutModifierKeys);
            _UpdateShortcutKeyRegistration();
        }

        // Utility Functions ----------------------------------------------------------------
        private void _UpdateAudioDevicesInterface()
        {
            lb_AudioDevices.Enabled = false;
            lb_AudioDevices.DataSource = null;
            lb_AudioDevices.DataSource = _audioDeviceUtility.AudioDeviceNames;
        }

        private void _UpdateInternalEnableState(bool state)
        {
            lb_AudioDevices.Enabled = state;
            btn_ClearShortcut.Enabled = state;
            btn_CancelKeyModification.Enabled = !state;
        }

        private void _UpdateModifierKeyText(Keys buttons, Keys modifiers)
        {
            bool ignore =
                ((int)buttons) == ((int)Keys.ControlKey) ||
                ((int)buttons) == ((int)Keys.Menu) ||
                ((int)buttons) == ((int)Keys.ShiftKey);

            tb_shortcutKey.Text = ((int)modifiers) != 0 && !ignore ? modifiers.ToString() + ", " + buttons.ToString() : buttons.ToString();
        }

        private void _UpdateShortcutKeyRegistration()
        {
            UnregisterHotKey(this.Handle, _hotKeyID);
            if (_shortcutButtonKeys != Keys.None)
            {
                // Extract modifiers and key
                uint modifiers = 0;
                if ((_shortcutModifierKeys & Keys.Alt) == Keys.Alt)
                    modifiers |= MOD_ALT;
                if ((_shortcutModifierKeys & Keys.Control) == Keys.Control)
                    modifiers |= MOD_CONTROL;
                if ((_shortcutModifierKeys & Keys.Shift) == Keys.Shift)
                    modifiers |= MOD_SHIFT;
                if ((_shortcutModifierKeys & Keys.LWin) == Keys.LWin || (_shortcutModifierKeys & Keys.RWin) == Keys.RWin)
                    modifiers |= MOD_WIN;

                uint keycode = (uint)_shortcutButtonKeys;

                RegisterHotKey(this.Handle, _hotKeyID, modifiers, keycode);
            }
        }

        private void _ResetShortcutKeys(bool resetSettings = false)
        {
            _shortcutModifierKeys = Keys.None;
            _shortcutButtonKeys = Keys.None;
            _temp_shortcutButtonKeys = Keys.None;
            _temp_shortcutModifierKeys = Keys.None;
            UnregisterHotKey(this.Handle, _hotKeyID);

            if (resetSettings)
            {
                _userSettings.ShortcutKeys = _shortcutButtonKeys;
                _userSettings.ModifierKeys = _shortcutModifierKeys;
                _userSettings.SaveSettings();
            }
        }

        // Event Handlers ----------------------------------------------------------------

        private void Lb_AudioDevices_DataSourceChanged(object? sender, EventArgs e)
        {
            lb_AudioDevices.Enabled = true;
        }

        private void _audioDeviceUtility_AudioDevicesChanged(object? sender, EventArgs e)
        {
            if (lb_AudioDevices.InvokeRequired)
            {
                lb_AudioDevices.Invoke(new Action(_UpdateAudioDevicesInterface));
            }
            else
            {
                _UpdateAudioDevicesInterface();
            }
        }

        private void btn_switchToDevice_Click(object sender, EventArgs e)
        {
            while (!lb_AudioDevices.Enabled) { }
            _audioDeviceUtility.SwitchToDevice(lb_AudioDevices.SelectedIndex);
        }

        private void btn_UpdateDeviceSwitchShortcut_Click(object sender, EventArgs e)
        {
            switch (_shortcutButtonState)
            {
                case ShortcutButtonState.Idle:
                    _UpdateInternalEnableState(false);
                    _shortcutButtonState = ShortcutButtonState.Recording;
                    break;
                case ShortcutButtonState.Recording:
                    // Update interface
                    _UpdateInternalEnableState(true);

                    // Update hot key
                    _shortcutButtonState = ShortcutButtonState.Idle;
                    _shortcutButtonKeys = _temp_shortcutButtonKeys;
                    _shortcutModifierKeys = _temp_shortcutModifierKeys;
                    _UpdateShortcutKeyRegistration();
                    
                    // Settings
                    _userSettings.ModifierKeys = _shortcutModifierKeys;
                    _userSettings.ShortcutKeys = _shortcutButtonKeys;
                    _userSettings.SaveSettings();
                    break;
            }

            btn_UpdateDeviceSwitchShortcut.Text = _shortcutButtonLabels[(int)_shortcutButtonState];
        }

        private void MainForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (_shortcutButtonState == ShortcutButtonState.Recording)
            {
                _temp_shortcutButtonKeys = e.KeyCode;
                _temp_shortcutModifierKeys = e.Modifiers;
                _UpdateModifierKeyText(_temp_shortcutButtonKeys, _temp_shortcutModifierKeys);
            }
        }

        private void btn_CancelKeyModification_Click(object sender, EventArgs e)
        {
            _UpdateInternalEnableState(true);
            _shortcutButtonState = ShortcutButtonState.Idle;
            _UpdateModifierKeyText(_shortcutButtonKeys, _shortcutModifierKeys);
            btn_UpdateDeviceSwitchShortcut.Text = _shortcutButtonLabels[(int)_shortcutButtonState];
        }

        private void btn_ClearShortcut_Click(object sender, EventArgs e)
        {
            _ResetShortcutKeys(true);
            _UpdateModifierKeyText(_shortcutButtonKeys, _shortcutModifierKeys);
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _ResetShortcutKeys(false);
            _UpdateModifierKeyText(_shortcutButtonKeys, _shortcutModifierKeys);
        }

        private void Lb_AudioDevices_Click(object? sender, EventArgs e)
        {
            while (!lb_AudioDevices.Enabled) { } // wait for audiodevices to finish updating.

            _audioDeviceUtility.SwitchToDevice(lb_AudioDevices.SelectedIndex);

            // update check boxes.
            for (int i = 0; i < lb_AudioDevices.Items.Count; ++i)
            {
                if (lb_AudioDevices.SelectedIndex == i)
                {
                    lb_AudioDevices.SetItemChecked(i, true);
                }
                else
                {
                    lb_AudioDevices.SetItemChecked(i, false);
                }
            }
        }

        private void lb_AudioDevices_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // prvevent unchecking of the selected device by clicking on the same item
            if (e.NewValue == CheckState.Unchecked && e.Index == lb_AudioDevices.SelectedIndex)
            {
                e.NewValue = e.CurrentValue;
            }
        }

        private void ni_notifyIconTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
            this.Activate(); // Bring the form to the foreground
        }

        // Handle hot key press using window procedure. 
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == _hotKeyID)
            {
                int nextIndex = (lb_AudioDevices.SelectedIndex + 1) % lb_AudioDevices.Items.Count;
                lb_AudioDevices.SetSelected(nextIndex, true);
                Lb_AudioDevices_Click(lb_AudioDevices, EventArgs.Empty);
            }

            base.WndProc(ref m);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                ni_notifyIconTray.Visible = true;
            }
        }
    }
}