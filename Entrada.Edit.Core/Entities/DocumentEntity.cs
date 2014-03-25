using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Entrada.Entities;
using Entrada.Editor.Data;

namespace Entrada.Editor.Core
{
    public class DocumentEntity
    {
        private string storage_dir = EditorCore.Settings.JobsDirectory;
        private string job_number;
        private bool is_dirty;

        public string EditedText { get; set; }
        public string recognized_wordfile { get; set; }
        public MemoryStream EditedMemoryStream { get; set; }
        public string VRReturnText { get; set; }
        public bool IsPreview { get; set; }
        public SendTo PreviewFor { get; set; }
        public string JobNumber { get { return job_number; } }
        public string RecognizedText { get; private set; }
        public bool IsSplitJob { get; private set; }
        public bool HasWorkInProgress { get; private set; }
        public string SelectedDocumentTemplate { get; set; }
        public string QANote { get; set; }
        public string TddJobSeparator { get; set; }
        public string TddPatientSeparator { get; set; }

        public AudioEntity Audio { get; private set; }
        public DemographicsEntity Demographics { get; private set; }
        public MedicalJobEntity Job { get; private set; }
        public Dictionary<string, string> Macros { get; private set; }
        public Dictionary<string, string> Contractions { get; private set; }
        public Dictionary<string, string> AutoText { get; private set; }
        public Dictionary<string, string> SignOffs { get; private set; }
        public List<string> AvailableDocumentTemplates { get; private set; }
        public List<string> AvailableJobTypes { get; private set; }
        public List<CCEntity> CC { get; private set; }
        public List<TddJobType> TddJobTypes { get; private set; }
        public Dictionary<string, int> CustomDocumentProperties { get; private set; }
        public List<InsertedMacroEntity> InsertedMacros { get; private set; }

        public string CCFile { get { return Path.Combine(storage_dir, job_number, "cc.txt"); } }
        public string InsertedMacroFile { get { return Path.Combine(storage_dir, job_number, "macros.xml"); } }
        public string WipFile { get { return Path.Combine(storage_dir, job_number, "wip.docx"); } }
        public bool CanSendToSameQA { get { return Job.QAData.CanReturnTo(NextQAStatus.ReturnJobToSameQA); } }
        public bool CanSendToNextQA { get { return Job.QAData.CanReturnTo(NextQAStatus.ReturnJobToNextQA); } }
        public bool CanSendToEntradaQA { get { return Job.QAData.CanReturnTo(NextQAStatus.ReturnJobToEntradaQA); } }
        public bool CanSendToCR { get { return Job.QAData.CanReturnTo(NextQAStatus.ReturnJobToCR); } }

        public event EventHandler IsDirtyChanged;
        public event EventHandler SaveRequested;
        public event EventHandler CCChanged;

        public DocumentEntity(string jobNumber = null, Dictionary<string, string> demographics = null)
        {
            job_number = jobNumber;

            AutoText = new Dictionary<string, string>();
            AvailableJobTypes = new List<string>();
            CustomDocumentProperties = new Dictionary<string, int>();

            if (demographics != null)
                Demographics = new DemographicsEntity(demographics);
        }

        public DocumentEntity(MedicalJobEntity job)
            : this()
        {
            Job = job;
            job_number = job.JobNumber;

            CC = new List<CCEntity>();
            InsertedMacros = new List<InsertedMacroEntity>();

            var audio_file = Path.Combine(storage_dir, job_number, job_number + ".ogg");
            Audio = new AudioEntity(audio_file, 0);

            var xml_file = Path.Combine(storage_dir, job_number, job_number + ".xml");
            var xml_doc = new XmlDocument();

            using (var stream = EncryptedFileSystem.GetInputStream(xml_file))
                xml_doc.Load(stream);

            LoadJobInfo(xml_doc);

            // Try to load the .dat first
            if (!LoadDemographics())
                LoadXmlDemographics(xml_doc);

            FixupCustomFields(xml_doc);
            LoadMacros(xml_doc);
            RestoreCCData();
            RestoreMacroData();

            AvailableDocumentTemplates = new List<string>();

            var template_path = Path.Combine(storage_dir, job_number);

            var all_docs = Directory.EnumerateFiles(template_path, "*.doc");
            all_docs.Concat(Directory.EnumerateFiles(template_path, "*.docx"));

            AvailableDocumentTemplates.AddRange(all_docs.Where(p => Path.GetFileNameWithoutExtension(p) != job_number && Path.GetFileNameWithoutExtension(p) != "wip").Select(p => Path.GetFileName(p)));

            SelectedDocumentTemplate = FindBestTemplate();

            var recognized_textfile = "";
            if (EditorCore.Settings.Editor.Type.ToLowerInvariant() == "editor")
            {
                recognized_textfile = Path.Combine(storage_dir, job_number, job_number + ".txt");
            }
            else
            {
                recognized_textfile = Path.Combine(storage_dir, job_number, job_number + ".doc");
                recognized_wordfile = Path.Combine(storage_dir, job_number, job_number + ".doc");
            }

            if (EditorCore.Settings.Editor.Type.ToLowerInvariant() == "editor")
            {
                using (var sr = EncryptedFileSystem.GetStreamReader(recognized_textfile, Encoding.UTF7))
                {
                    RecognizedText = sr.ReadToEnd();
                    EditedText = RecognizedText;
                }
            }

            if (File.Exists(WipFile))
                HasWorkInProgress = true;
            QANote = string.Empty;

            job.Encounter.Patient = Demographics.ToPatient();
            Demographics.JOBS_CC = CC.Count > 0;

            LoadTddJobTypes(xml_doc);
        }

        public bool IsDirty
        {
            get { return is_dirty; }
            set
            {
                if (is_dirty != value)
                {
                    is_dirty = value;

                    if (Job.GetStatus() == DownloadedJobStatus.Downloaded && value)
                    {
                        Job.SetStatus(DownloadedJobStatus.InProgress);
                        EditorCore.Jobs.FireClaimedJobsChanged();
                    }

                    if (IsDirtyChanged != null)
                        IsDirtyChanged(null, EventArgs.Empty);
                }
            }
        }

        public bool IsTddJob
        {
            get { return TddJobTypes.Any(p => p.Name == Demographics.JOBS_JOBTYPE); }
        }

        public TddJobType TddJobType
        {
            get { return TddJobTypes.FirstOrDefault(p => p.Name == Demographics.JOBS_JOBTYPE); }
        }

        private bool LoadDemographics()
        {
            var demo = new Dictionary<string, string>();
            string data;

            var filename = Path.Combine(storage_dir, job_number, job_number + ".dat");

            if (!File.Exists(filename))
                return false;

            using (var reader = new StreamReader(EncryptedFileSystem.GetDecryptedStream(filename)))
                data = reader.ReadToEnd();

            var lines = data.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var s in lines)
            {
                if (!s.Contains('='))
                    throw new ApplicationException(string.Format("Invalid demographics for job {0}: {1}", job_number, s));

                var values = s.Split('=');
                demo.Add(values[0].ToUpper().Trim(), values[1].Trim());
            }

            Demographics = new DemographicsEntity(demo);

            return true;
        }

        private void SaveDemographics()
        {
            var filename = Path.Combine(storage_dir, job_number, job_number + ".dat");

            using (var sw = EncryptedFileSystem.GetStreamWriter(filename))
                foreach (var item in Demographics.Raw)
                    sw.WriteLine("{0}={1}", item.Key, item.Value);
        }

        private void LoadJobInfo(XmlDocument docXml)
        {
            foreach (XmlElement xe in docXml.DocumentElement)
            {
                if (xe.Name != "JobInfo")
                    continue;

                foreach (XmlElement item in xe.ChildNodes)
                {
                    switch (item.Name)
                    {
                        case "ParentJobNumber":
                            if (!string.IsNullOrWhiteSpace(item.InnerText))
                                IsSplitJob = true;

                            break;
                    }
                }
            }
        }

        private void LoadXmlDemographics(XmlDocument docXml)
        {
            var doc = docXml.DocumentElement;
            var demos = new Dictionary<string, string>();

            foreach (XmlElement xe in doc.ChildNodes)
                switch (xe.Name.ToUpperInvariant())
                {
                    case "JOBINFO":
                        AddXmlDemographics(demos, xe, "Jobs_", true);
                        break;
                    case "JOBPATIENTS":
                        AddXmlDemographics(demos, xe, "Patient_", true);
                        break;
                    case "JOBREFERRING":
                        AddXmlDemographics(demos, xe, "Referring_", true);
                        break;
                    case "DICTATOR":
                        AddXmlDemographics(demos, xe, "Dictator_", false);
                        break;
                    case "SIGNOFFS":
                        AddXmlDemographics(demos, xe, "Editor_", false);
                        break;
                    case "JOBCUSTOM":
                        AddXmlDemographics(demos, xe, "Custom_", true);
                        break;
                }

            Demographics = new DemographicsEntity(demos);
        }

        private void AddXmlDemographics(Dictionary<string, string> demos, XmlElement xe, string prefix, bool skipJobNumber)
        {
            foreach (XmlElement child in xe.ChildNodes)
            {
                if (skipJobNumber && child.Name.ToLowerInvariant() == "jobnumber")
                    continue;

                demos.Add((prefix + child.Name).ToUpperInvariant(), child.InnerText);
            }
        }

        private void FixupCustomFields(XmlDocument doc)
        {
            // If a custom field is in use, it will have a Description attribute.
            // In this case, we want to display the field with it's Description,
            // rather than CUSTOM1.
            foreach (XmlElement xe in doc.DocumentElement["JobCustom"])
            {
                if (xe.Name == "JobNumber")
                    continue;

                var name = xe.Name;
                var description = xe.GetAttribute("Description");

                if (string.IsNullOrWhiteSpace(description))
                    continue;

                Demographics.CustomFieldDescriptions.Add(name, description);
            }
        }

        private void LoadMacros(XmlDocument docXml)
        {
            Macros = new Dictionary<string, string>();
            Contractions = new Dictionary<string, string>();
            AutoText = new Dictionary<string, string>();
            SignOffs = new Dictionary<string, string>();

            var doc = docXml.DocumentElement;

            foreach (XmlElement xe in doc.ChildNodes)
            {
                switch (xe.Name.ToUpperInvariant())
                {

                    case "MACROS":
                        foreach (XmlElement macro in xe.ChildNodes)
                        {
                            var name = macro.ChildNodes[0].InnerText;
                            var content_node = (XmlElement)macro.ChildNodes[1];

                            // Remove any style="font-size: ..." attributes
                            RemoveFontSizeStyles(content_node);
                            var content = content_node.InnerXml;

                            if (content.EndsWith("\n"))
                                content = content.Substring(0, content.Length - 1);

                            if (content.StartsWith("<![CDATA["))
                            {
                                content = content.Substring(9); //Remove <![CDATA[
                                content = content.Substring(0, content.Length - 3); //Remove ]]>
                            }

                            Macros.Add(name, ReplaceMacroFields(content));
                        }

                        break;

                    case "CONTRACTIONS":
                        foreach (XmlElement macro in xe.ChildNodes)
                        {
                            var name = macro.ChildNodes[0].InnerText;
                            var content = macro.ChildNodes[1].InnerXml;

                            if (content.EndsWith("\n"))
                                content = content.Substring(0, content.Length - 1);

                            if (content.StartsWith("<![CDATA["))
                            {
                                content = content.Substring(9); //Remove <![CDATA[
                                content = content.Substring(0, content.Length - 3); //Remove ]]>
                            }

                            Contractions.Add(name, content);

                            // Handle both "he'll" and "He'll"
                            var cap_name = Capitalize(name);

                            if (name != cap_name)
                                Contractions.Add(cap_name, Capitalize(content));
                        }

                        break;

                    case "AUTOTEXT":
                        foreach (XmlElement auto in xe.ChildNodes)
                        {
                            var sAutoTextName = auto["Name"].InnerText;
                            var sAutoText = auto["Value"].InnerXml;

                            if (!string.IsNullOrWhiteSpace(sAutoTextName))
                                AutoText.Add(sAutoTextName, sAutoText);
                        }

                        break;

                    case "SIGNOFFS":
                        foreach (XmlElement signoff in xe.ChildNodes)
                        {
                            var sName = signoff.Name.Substring(signoff.Name.IndexOf('_') + 1);
                            SignOffs.Add(sName, signoff.HasChildNodes ? signoff.ChildNodes[0].InnerText : "");
                        }

                        break;
                }
            }
        }

        private string Capitalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            if (text.Length == 1)
                return text.ToUpperInvariant();

            return char.ToUpperInvariant(text[0]) + text.Substring(1);
        }

        private string ReplaceMacroFields(string text)
        {
            while (true)
            {
                var iPos = text.IndexOf("##");

                if (iPos == -1)
                    break;

                var iPos2 = text.IndexOf("##", iPos + 1);

                if (iPos2 == -1)
                    break;

                var field = text.Substring(iPos + 2, iPos2 - iPos - 2);

                if (field != "" && Demographics.Raw.ContainsKey(field.ToUpperInvariant()))
                {
                    var field_text = Demographics.Raw[field.ToUpperInvariant()];

                    text = text.Substring(0, iPos) + field_text + text.Substring(iPos2 + 2);
                }
                else
                    break;
            }

            return text;
        }

        private void RemoveFontSizeStyles(XmlNode xe)
        {
            if (xe.Attributes != null)
                foreach (XmlAttribute attr in xe.Attributes)
                    if (attr.Name.ToLowerInvariant() == "style" && attr.Value.ToLowerInvariant().Contains("font-size"))
                    {
                        xe.Attributes.Remove(attr);
                        break;
                    }

            foreach (XmlNode child in xe)
                RemoveFontSizeStyles(child);
        }

        private void LoadTddJobTypes(XmlDocument doc)
        {
            TddJobSeparator = doc.DocumentElement["Clinic"]["JobTag"].InnerText;
            TddPatientSeparator = doc.DocumentElement["Clinic"]["PatientTag"].InnerText;

            TddJobTypes = new List<TddJobType>();

            if (doc.DocumentElement["TddJobTypes"] == null)
                return;

            foreach (XmlElement xml_type in doc.DocumentElement["TddJobTypes"])
            {
                var jt = new TddJobType();

                jt.Name = xml_type.GetAttribute("Name");
                jt.AllTagged = bool.Parse(xml_type.GetAttribute("AllTagged"));

                foreach (XmlElement xml_tag in xml_type["Tags"])
                {
                    var name = xml_tag.GetAttribute("Name");
                    var required = bool.Parse(xml_tag.GetAttribute("Required"));

                    jt.Tags.Add(new TddTag(name, required));
                }

                TddJobTypes.Add(jt);
            }
        }

        private void SaveCCData()
        {
            // Just delete the file if we don't have any CC's.
            if (CC.Count == 0)
            {
                if (File.Exists(CCFile))
                    File.Delete(CCFile);

                return;
            }

            using (var sw = EncryptedFileSystem.GetOutputStream(CCFile))
            {
                var x = new System.Xml.Serialization.XmlSerializer(typeof(CCEntity[]));
                x.Serialize(sw, CC.ToArray());
            }
        }

        private void RestoreCCData()
        {
            if (!File.Exists(CCFile))
                return;

            using (var sr = EncryptedFileSystem.GetInputStream(CCFile))
            {
                var x = new System.Xml.Serialization.XmlSerializer(typeof(CCEntity[]));
                var cc = (CCEntity[])x.Deserialize(sr);

                CC.AddRange(cc);
            }
        }

        private void SaveMacroData()
        {
            // Just delete the file if we don't have any macros.
            if (InsertedMacros.Count == 0)
            {
                if (File.Exists(InsertedMacroFile))
                    File.Delete(InsertedMacroFile);

                return;
            }

            using (var sw = EncryptedFileSystem.GetOutputStream(InsertedMacroFile))
            {
                var x = new System.Xml.Serialization.XmlSerializer(typeof(InsertedMacroEntity[]));
                x.Serialize(sw, InsertedMacros.ToArray());
            }
        }

        private void RestoreMacroData()
        {
            if (!File.Exists(InsertedMacroFile))
                return;

            using (var sr = EncryptedFileSystem.GetInputStream(InsertedMacroFile))
            {
                var x = new System.Xml.Serialization.XmlSerializer(typeof(InsertedMacroEntity[]));
                var macros = (InsertedMacroEntity[])x.Deserialize(sr);

                InsertedMacros.AddRange(macros);
            }
        }

        public string FindBestTemplate()
        {
            // If there's only one, let's use it
            if (AvailableDocumentTemplates.Count == 1)
                return AvailableDocumentTemplates[0];

            var jt = Demographics.JOBS_JOBTYPE;

            if (string.IsNullOrWhiteSpace(jt))
                return null;

            // Try to find one with a matching name as the job type
            var best = AvailableDocumentTemplates.Where(p => p.ToUpperInvariant().Replace(" ", "").Contains(jt.Trim().ToUpperInvariant().Replace(" ", ""))).FirstOrDefault();

            return best;
        }

        public void Save()
        {
            // Save demographics to a .dat
            SaveDemographics();

            // Save CC data
            SaveCCData();

            // Save inserted macro data
            SaveMacroData();

            if (SaveRequested != null)
                SaveRequested(null, EventArgs.Empty);

            HasWorkInProgress = true;
        }

        public void RaiseCCChanged()
        {
            if (CCChanged != null)
                CCChanged(null, EventArgs.Empty);
        }
    }
}
