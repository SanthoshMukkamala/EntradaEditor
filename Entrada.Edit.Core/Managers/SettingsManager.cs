using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using DevExpress.XtraRichEdit;
using Entrada.Entities;

namespace Entrada.Editor.Core
{
    public class SettingsManager
    {
        private Dictionary<string, object> settings;

        public EditorEntity Editor { get; set; }
        public int TimeSheetID { get; set; }
        public int ActiveAudioDevice { get; set; }

        public string ApplicationDirectory { get; set; }
        public string DictionaryDirectory { get; set; }
        public string JobsDirectory { get; set; }
        public string UserDataDirectory { get; set; }
        public string LogsDirectory { get; set; }

        public bool RibbonExpanded { get { return GetSetting ("RibbonExpanded", true); } set { PutSetting ("RibbonExpanded", value); } }
        public int FastForwardRewindSpeed { get { return GetSetting ("FastForwardRewindSpeed", 400); } set { PutSetting ("FastForwardRewindSpeed", value); } }
        public int DefaultDocumentZoom { get { return GetSetting ("DefaultDocumentZoom", 100); } set { PutSetting ("DefaultDocumentZoom", value); } }
        public float FootPedalBounceBack { get { return GetSetting ("FootPedalBounceBack", 2.0f); } set { PutSetting ("FootPedalBounceBack", value); } }
        public float KeyboardBounceBack { get { return GetSetting ("KeyboardBounceBack", 2.0f); } set { PutSetting ("KeyboardBounceBack", value); } }
        public bool AutoCorrectTwoCaps { get { return GetSetting ("AutoCorrectTwoCaps", true); } set { PutSetting ("AutoCorrectTwoCaps", value); } }
        public bool AutoCorrectSpellCheck { get { return GetSetting ("AutoCorrectSpellCheck", false); } set { PutSetting ("AutoCorrectSpellCheck", value); } }
        public int DefaultDownloadTimerDuration { get { return 5000; } }
        public EditEnvironment EditEnvironment { get { return (EditEnvironment)GetSetting ("EditEnvironment", 1); } set { PutSetting ("EditEnvironment", (int)value); } }
        public string PreferredAudioDevice { get { return GetSetting ("PreferredAudioDevice", ""); } set { PutSetting ("PreferredAudioDevice", value); } }

        // Spelling options
        public bool IgnoreEmails { get { return GetSetting ("IgnoreEmails", true); } set { PutSetting ("IgnoreEmails", value); } }
        public bool IgnoreMixedCaseWords { get { return GetSetting ("IgnoreMixedCaseWords", true); } set { PutSetting ("IgnoreMixedCaseWords", value); } }
        public bool IgnoreRepeatedWords { get { return GetSetting ("IgnoreRepeatedWords", false); } set { PutSetting ("IgnoreRepeatedWords", value); } }
        public bool IgnoreUpperCaseWords { get { return GetSetting ("IgnoreUpperCaseWords", true); } set { PutSetting ("IgnoreUpperCaseWords", value); } }
        public bool IgnoreUrls { get { return GetSetting ("IgnoreUrls", true); } set { PutSetting ("IgnoreUrls", value); } }
        public bool IgnoreWordsWithNumbers { get { return GetSetting ("IgnoreWordsWithNumbers", true); } set { PutSetting ("IgnoreWordsWithNumbers", value); } }

        public SortedDictionary<string, string> AutoCorrectTextList { get; set; }

        public event EventHandler SettingsChanged;
        public event EventHandler AutoCorrectListChanged;

        public SettingsManager ()
        {
            try {
                TimeSheetID = -1;
                ActiveAudioDevice = -1;

                ApplicationDirectory = Path.GetDirectoryName (Environment.GetCommandLineArgs ()[0]);
                DictionaryDirectory = Path.Combine (ApplicationDirectory, "Dictionaries");
                UserDataDirectory = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.LocalApplicationData), "Entrada Editor");
                JobsDirectory = Path.Combine (UserDataDirectory, "Jobs");
                LogsDirectory = Path.Combine (UserDataDirectory, "Logs");

                // Make sure our directories exist
                Directory.CreateDirectory (UserDataDirectory);
                Directory.CreateDirectory (JobsDirectory);
                Directory.CreateDirectory (LogsDirectory);

                LoadSettings ();
                LoadAutoCorrectList ();

                // Try to delete logs > 14 days old
                foreach (var log in Directory.EnumerateFiles (LogsDirectory)) {
                    DateTime dt;

                    if (DateTime.TryParse (Path.GetFileNameWithoutExtension (log), out dt))
                        if (DateTime.Now.Subtract (dt).TotalDays > 14)
                            File.Delete (log);
                }
            } catch {
                // This fails while in design mode
            }
        }

        public string DefaultLayoutSettingsFile { get { return Path.Combine (UserDataDirectory, "default_layout.xml"); } }
        public string LayoutSettingsFile { get { return Path.Combine (UserDataDirectory, "layout2.xml"); } }
        public string DefaultAvailableJobsGridFile { get { return Path.Combine (UserDataDirectory, "default_manual_jobs.xml"); } }
        public string AvailableJobsGridFile { get { return Path.Combine (UserDataDirectory, "manual_jobs.xml"); } }
        public string AvailableJobsGridFile1 { get { return Path.Combine(UserDataDirectory, "manual_jobs1.xml"); } }
        public string SettingsFile { get { return Path.Combine (UserDataDirectory, "settings.xml"); } }
        public string AutoCorrectFile { get { return Path.Combine (UserDataDirectory, "autocorrect.txt"); } }

        public T GetSetting<T> (string key, T defaultValue)
        {
            if (!settings.ContainsKey (key))
                return defaultValue;

            return (T)settings[key];
        }

        public void PutSetting (string key, object value)
        {
            settings[key] = value;

            if (SettingsChanged != null)
                SettingsChanged (null, EventArgs.Empty);
        }

        private void LoadSettings ()
        {
            settings = new Dictionary<string, object> ();

            if (!File.Exists (SettingsFile))
                return;

            var doc = new XmlDocument ();
            doc.Load (SettingsFile);

            foreach (XmlElement setting in doc.DocumentElement.ChildNodes) {
                var type = Type.GetType (setting.GetAttribute ("type"));
                settings[setting.GetAttribute ("name")] = Parse (setting.InnerText, setting.GetAttribute ("type"));
            }

            if (SettingsChanged != null)
                SettingsChanged (null, EventArgs.Empty);
        }

        public void SaveSettings ()
        {
            using (var xw = new XmlTextWriter (SettingsFile, System.Text.Encoding.UTF8)) {
                xw.Formatting = Formatting.Indented;
                xw.WriteStartElement ("settings");

                foreach (var item in settings) {
                    xw.WriteStartElement ("setting");
                    xw.WriteAttributeString ("name", item.Key);
                    xw.WriteAttributeString ("type", item.Value.GetType ().AssemblyQualifiedName);
                    xw.WriteValue (TypeDescriptor.GetConverter (item.Value).ConvertToString (item.Value));
                    xw.WriteEndElement ();
                }

                xw.WriteEndElement ();
            }
        }

        private void LoadAutoCorrectList ()
        {
            AutoCorrectTextList = new SortedDictionary<string,string> (StringComparer.InvariantCultureIgnoreCase);

            if (!File.Exists (AutoCorrectFile))
                return;

            foreach (var s in File.ReadAllLines (AutoCorrectFile)) {
                var index = s.IndexOf ('=');

                if (index < 0)
                    break;

                var key = s.Substring (0, index);
                var value = s.Substring (index + 1);

                AutoCorrectTextList[key] = value;
            }
        }

        public void SaveAutoCorrectList ()
        {
            // I'm guessing people would be unhappy if we lost their file,
            // so let's start with a backup and then switch
            var backup_file = Path.Combine (UserDataDirectory, "autocorrect2.txt");

            using (var sw = new StreamWriter (backup_file)) {
                foreach (var ac in AutoCorrectTextList) {
                    sw.WriteLine ("{0}={1}", ac.Key, ac.Value);
                }
            }

            File.Copy (backup_file, AutoCorrectFile, true);
            File.Delete (backup_file);

            if (AutoCorrectListChanged != null)
                AutoCorrectListChanged (null, EventArgs.Empty);
        }

        private static object Parse (string text, string type)
        {
            var t = Type.GetType (type);
            return TypeDescriptor.GetConverter (t).ConvertFromInvariantString (text);
        }
    }
}
