using System.IO;
using System.Xml.Serialization;

namespace AudioDevice
{
    public class UserSettings
    {
        public Keys ShortcutKeys { get; set; }
        public Keys ModifierKeys { get; set; }
        public string SettingsFileName { get; set; }

        public UserSettings() 
        {
            SettingsFileName = "";
        }

        public UserSettings(string filename) 
        {
            ShortcutKeys = Keys.None;
            ModifierKeys = Keys.None;
            SettingsFileName = filename;

            LoadSettings();
        }

        public void LoadSettings()
        {
            try
            {
                if (!File.Exists(SettingsFileName))
                {
                    SaveSettings();
                }

                XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));
                using (StreamReader reader =  new StreamReader(SettingsFileName)) 
                {
                    if (serializer != null)
                    {
                        object? obj = serializer.Deserialize(reader);
                        if (obj != null)
                        {
                            UserSettings settings = (UserSettings)obj;
                            if (settings != null)
                            {
                                ModifierKeys = settings.ModifierKeys;
                                ShortcutKeys = settings.ShortcutKeys;
                            }
                            else
                            {
                                throw new Exception("Could not open settings file for writing.");
                            }
                        }
                        else
                        {
                            throw new Exception("Could not open settings file for writing.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                try
                {
                    if (ex.Source == "System.Private.Xml")
                    {
                        File.Delete(SettingsFileName);
                        SaveSettings();
                    }
                }
                catch
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void SaveSettings()
        {
            try
            {
                if (!File.Exists(SettingsFileName))
                {
                    FileStream fs = File.Create(SettingsFileName);                    
                    fs.Close();
                }

                XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));
                using (StreamWriter writer = new StreamWriter(SettingsFileName)) 
                { 
                    serializer.Serialize(writer, this);
                    writer.Close();
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}
