namespace AudioDevice
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }


            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            label_AudioDeviceList = new Label();
            btn_UpdateDeviceSwitchShortcut = new Button();
            label_DeviceSwitchShortcut = new Label();
            tb_shortcutKey = new TextBox();
            btn_CancelKeyModification = new Button();
            btn_ClearShortcut = new Button();
            gb_devices = new GroupBox();
            lb_AudioDevices = new CheckedListBox();
            gb_deviceRotation = new GroupBox();
            ni_notifyIconTray = new NotifyIcon(components);
            cm_trayMenu = new ContextMenuStrip(components);
            gb_devices.SuspendLayout();
            gb_deviceRotation.SuspendLayout();
            SuspendLayout();
            // 
            // label_AudioDeviceList
            // 
            label_AudioDeviceList.AutoSize = true;
            label_AudioDeviceList.BackColor = SystemColors.Control;
            label_AudioDeviceList.Location = new Point(6, 23);
            label_AudioDeviceList.Name = "label_AudioDeviceList";
            label_AudioDeviceList.Size = new Size(124, 20);
            label_AudioDeviceList.TabIndex = 2;
            label_AudioDeviceList.Text = "Audio Device List";
            // 
            // btn_UpdateDeviceSwitchShortcut
            // 
            btn_UpdateDeviceSwitchShortcut.Location = new Point(6, 54);
            btn_UpdateDeviceSwitchShortcut.Name = "btn_UpdateDeviceSwitchShortcut";
            btn_UpdateDeviceSwitchShortcut.Size = new Size(463, 29);
            btn_UpdateDeviceSwitchShortcut.TabIndex = 4;
            btn_UpdateDeviceSwitchShortcut.Text = "Update Device Switch Shortcut";
            btn_UpdateDeviceSwitchShortcut.UseVisualStyleBackColor = true;
            btn_UpdateDeviceSwitchShortcut.Click += btn_UpdateDeviceSwitchShortcut_Click;
            // 
            // label_DeviceSwitchShortcut
            // 
            label_DeviceSwitchShortcut.AutoSize = true;
            label_DeviceSwitchShortcut.Location = new Point(6, 23);
            label_DeviceSwitchShortcut.Name = "label_DeviceSwitchShortcut";
            label_DeviceSwitchShortcut.Size = new Size(92, 20);
            label_DeviceSwitchShortcut.TabIndex = 6;
            label_DeviceSwitchShortcut.Text = "Shortcut Key";
            // 
            // tb_shortcutKey
            // 
            tb_shortcutKey.BorderStyle = BorderStyle.FixedSingle;
            tb_shortcutKey.Enabled = false;
            tb_shortcutKey.Location = new Point(104, 21);
            tb_shortcutKey.Name = "tb_shortcutKey";
            tb_shortcutKey.ReadOnly = true;
            tb_shortcutKey.ShortcutsEnabled = false;
            tb_shortcutKey.Size = new Size(365, 27);
            tb_shortcutKey.TabIndex = 5;
            tb_shortcutKey.TabStop = false;
            tb_shortcutKey.TextAlign = HorizontalAlignment.Center;
            // 
            // btn_CancelKeyModification
            // 
            btn_CancelKeyModification.Enabled = false;
            btn_CancelKeyModification.Location = new Point(249, 89);
            btn_CancelKeyModification.Name = "btn_CancelKeyModification";
            btn_CancelKeyModification.Size = new Size(220, 29);
            btn_CancelKeyModification.TabIndex = 7;
            btn_CancelKeyModification.Text = "Cancel";
            btn_CancelKeyModification.UseVisualStyleBackColor = true;
            btn_CancelKeyModification.Click += btn_CancelKeyModification_Click;
            // 
            // btn_ClearShortcut
            // 
            btn_ClearShortcut.Location = new Point(6, 89);
            btn_ClearShortcut.Name = "btn_ClearShortcut";
            btn_ClearShortcut.Size = new Size(220, 29);
            btn_ClearShortcut.TabIndex = 8;
            btn_ClearShortcut.Text = "Clear Shortcut";
            btn_ClearShortcut.UseVisualStyleBackColor = true;
            btn_ClearShortcut.Click += btn_ClearShortcut_Click;
            // 
            // gb_devices
            // 
            gb_devices.Controls.Add(lb_AudioDevices);
            gb_devices.Controls.Add(label_AudioDeviceList);
            gb_devices.Location = new Point(12, 12);
            gb_devices.Name = "gb_devices";
            gb_devices.Size = new Size(475, 356);
            gb_devices.TabIndex = 9;
            gb_devices.TabStop = false;
            gb_devices.Text = "Devices";
            // 
            // lb_AudioDevices
            // 
            lb_AudioDevices.FormattingEnabled = true;
            lb_AudioDevices.Location = new Point(6, 46);
            lb_AudioDevices.Name = "lb_AudioDevices";
            lb_AudioDevices.Size = new Size(463, 290);
            lb_AudioDevices.TabIndex = 3;
            lb_AudioDevices.ItemCheck += lb_AudioDevices_ItemCheck;
            // 
            // gb_deviceRotation
            // 
            gb_deviceRotation.Controls.Add(label_DeviceSwitchShortcut);
            gb_deviceRotation.Controls.Add(btn_UpdateDeviceSwitchShortcut);
            gb_deviceRotation.Controls.Add(btn_ClearShortcut);
            gb_deviceRotation.Controls.Add(tb_shortcutKey);
            gb_deviceRotation.Controls.Add(btn_CancelKeyModification);
            gb_deviceRotation.Location = new Point(12, 374);
            gb_deviceRotation.Name = "gb_deviceRotation";
            gb_deviceRotation.Size = new Size(475, 130);
            gb_deviceRotation.TabIndex = 10;
            gb_deviceRotation.TabStop = false;
            gb_deviceRotation.Text = "Device Rotation Shortcut Key";
            // 
            // ni_notifyIconTray
            // 
            ni_notifyIconTray.ContextMenuStrip = cm_trayMenu;
            ni_notifyIconTray.Icon = (Icon)resources.GetObject("ni_notifyIconTray.Icon");
            ni_notifyIconTray.Text = "Audio Device Selector";
            ni_notifyIconTray.Visible = true;
            ni_notifyIconTray.MouseDoubleClick += ni_notifyIconTray_MouseDoubleClick;
            // 
            // cm_trayMenu
            // 
            cm_trayMenu.ImageScalingSize = new Size(20, 20);
            cm_trayMenu.Name = "contextMenuStrip1";
            cm_trayMenu.Size = new Size(61, 4);
            cm_trayMenu.Text = "Audio Device Selector";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(497, 514);
            Controls.Add(gb_deviceRotation);
            Controls.Add(gb_devices);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "MainForm";
            ShowInTaskbar = false;
            Text = "Audio Device Selector";
            Resize += MainForm_Resize;
            gb_devices.ResumeLayout(false);
            gb_devices.PerformLayout();
            gb_deviceRotation.ResumeLayout(false);
            gb_deviceRotation.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label_AudioDeviceList;
        private Button btn_UpdateDeviceSwitchShortcut;
        private Label label_DeviceSwitchShortcut;
        private TextBox tb_shortcutKey;
        private Button btn_CancelKeyModification;
        private Button btn_ClearShortcut;
        private GroupBox gb_devices;
        private GroupBox gb_deviceRotation;
        private CheckedListBox lb_AudioDevices;
        private NotifyIcon ni_notifyIconTray;
        private ContextMenuStrip cm_trayMenu;
    }
}