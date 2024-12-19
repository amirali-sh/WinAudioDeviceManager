using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CoreAudio;
using System.ComponentModel;
using System.Reflection;

namespace AudioDevice
{
    internal class AudioDeviceUtility
    {
        // Fields
        private List<string> _audioDeviceNames = new List<string>();
        private MMNotificationClient _audioDeviceNotificationClient;
        private MMDeviceEnumerator _audioDeviceEnumerator;
        public event EventHandler? AudioDevicesChanged;
        
        // Constructors
        public AudioDeviceUtility()
        {
            _audioDeviceEnumerator = new MMDeviceEnumerator();
            _audioDeviceNotificationClient = new MMNotificationClient(_audioDeviceEnumerator);

            _RegisterForDeviceNotifications();
            _RefreshAudioDevices();
        }

        // Properties
        public List<string> AudioDeviceNames => _audioDeviceNames;

        // Methods
        public int GetActiveDeviceIndex()
        {
            var audioDeviceCollection = _audioDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            for (int i = 0; i < audioDeviceCollection.Count; ++i)
            {
                if (audioDeviceCollection[i].Selected)
                {
                    return i;
                }
            }

            return 0;
        }

        public void SwitchToDevice(int index)
        {
            var audioDeviceCollection = _audioDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            audioDeviceCollection[index].Selected = true;
        }
        private void _RefreshAudioDevices()
        {
            // Update audio devices           
            var audioDeviceCollection = _audioDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

            // Update audio device names
            _audioDeviceNames.Clear();
            foreach (var device in audioDeviceCollection)
            {
                if (device.State == DeviceState.Active)
                {
                    _audioDeviceNames.Add(device.DeviceFriendlyName);
                }
            }

            OnListChanged(EventArgs.Empty);              
        }
        private void _RegisterForDeviceNotifications()
        {
            _audioDeviceNotificationClient.DeviceRemoved += _audioDeviceNotificationClient_DeviceRemoved;
            _audioDeviceNotificationClient.DeviceAdded += _audioDeviceNotificationClient_DeviceAdded;
            _audioDeviceNotificationClient.DeviceStateChanged += _audioDeviceNotificationClient_DeviceStateChanged;
        }

        private void _audioDeviceNotificationClient_DeviceStateChanged(object? sender, DeviceStateChangedEventArgs e)
        {
            _RefreshAudioDevices();
        }

        private void _audioDeviceNotificationClient_DeviceAdded(object? sender, DeviceNotificationEventArgs e)
        {
            _RefreshAudioDevices();
        }

        private void _audioDeviceNotificationClient_DeviceRemoved(object? sender, DeviceNotificationEventArgs e)
        {
            _RefreshAudioDevices();
        }

        protected virtual void OnListChanged(EventArgs e)
        {
            AudioDevicesChanged?.Invoke(this, e);
        }        
    }
}
