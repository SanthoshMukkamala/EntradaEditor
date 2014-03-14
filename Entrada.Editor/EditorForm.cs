using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Commands;
using Entrada.Editor.Core;
using Entrada.Editor.Properties;
using PIA.SpMikeCtrl;
using Entrada.Editor.Data;

namespace Entrada.Editor
{
    public partial class EditorForm : RibbonForm
    {
        private enum BackStepAction { None, FootPedal, Keyboard };

        private SpeechMikeControl foot_pedal;
        private RepositoryItemComboBox job_type_demographics_combo;

        private bool rewinding;
        private bool fastforwarding;
        private bool scrubbing_pause;
        private bool trackbar_mousedown;
        private bool closing_tab;
        private bool closing_all_tabs;
        private bool has_audio;
        private bool must_change_password;

        private int left_words, right_words;
        private int male_words, female_words;

        public EditorForm()
        {
            InitializeComponent();

            // This conflicts with the Play button
            ribbonControl1.Manager.UseF10KeyForMenu = false;

            EditorCore.Documents.DocumentOpened += Documents_DocumentOpened;
            EditorCore.Documents.DocumentActivated += Documents_DocumentActivated;
            EditorCore.Documents.DocumentDeactivated += Documents_DocumentDeactivated;
            EditorCore.Documents.DocumentClosed += Documents_DocumentClosed;

            EditorCore.Jobs.JobRemoved += Jobs_JobListAddRemove;
            EditorCore.Jobs.JobAdded += Jobs_JobListAddRemove;
            EditorCore.Jobs.ClaimedJobsUpdated += Jobs_JobListUpdated;
            EditorCore.Jobs.WorkQueueRunningChanged += Jobs_WorkQueueRunningChanged;
            EditorCore.Jobs.CloseSoundRequested += Jobs_CloseSoundRequested;

            EditorCore.Background.StatusUpdated += Background_StatusUpdated;
            EditorCore.Background.StatusFinished += Background_StatusFinished;
            EditorCore.Background.ConfirmAction += Background_ConfirmAction;
            EditorCore.Background.ShowMessage += Background_ShowMessage;
            EditorCore.Background.RefreshDemographics += (o, _) => { grdDemographics.Refresh(); };
            EditorCore.Background.ShowIdleWarning += Background_ShowIdleWarning;

            FormClosing += EditorForm_FormClosing;
            KeyDown += EditorForm_KeyDown;
            documentManager1.View.DocumentActivated += View_DocumentActivated;
            documentManager1.View.DocumentDeactivated += View_DocumentDeactivated;
            documentManager1.View.DocumentClosing += View_DocumentClosing;

            grdDemographics.CellValueChanging += grdDemographics_CellValueChanging;
            lstDownloadedJobs.DoubleClick += DownloadedJobList_DoubleClick;

            barZoomSlider.Edit.EditValueChanging += ZoomSlider_EditorValueChanging;
            btnStartEditing.Enabled = true;

            trkAudioPosition.Edit.MouseDown += trkAudioPosition_MouseDown;
            trkAudioPosition.Edit.MouseUp += trkAudioPosition_MouseUp;
            trkAudioPosition.Edit.EditValueChanging += trkAudioPosition_EditValueChanging;

            var currentSkin = CommonSkins.GetSkin(LookAndFeel);
            lstDownloadedJobs.HighlightColor = currentSkin.TranslateColor(SystemColors.Highlight);

            // Create a default panel layout so we can reset to default if needed
            dockManager1.SaveLayoutToXml(EditorCore.Settings.DefaultLayoutSettingsFile);

            //try {
            //    if (File.Exists (EditorCore.Settings.LayoutSettingsFile))
            //        dockManager1.RestoreLayoutFromXml (EditorCore.Settings.LayoutSettingsFile);
            //} catch (Exception ex) {
            //    // Log and ignore
            //    EditorCore.LogException ("Could not restore LayoutSettingsFile.", ex);
            //}

            // Only for QA
            if (EditorCore.Settings.Editor.Type.ToLowerInvariant() == "qa")
            {
                btnStartEditing.Caption = "Start QA Queue";
                btnStartEditing.Hint = "Starts downloading jobs to be QA.";

                btnStopEditing.Caption = "Stop QA Queue";
                btnStopEditing.Hint = "Stop downloading jobs to be QA.";

                btnSplitJobType.Enabled = false;
                btnSplitPatient.Enabled = false;

                //btnFinishDocument.SetVisible(false);
                btnSendToQA.SetVisible(false);

              // For QA Category
                 barCategory.Enabled = false;
                barSubCategory.Enabled = false;

               

                List<Entities.QACategory> objCategory = JobRepository.GetAllQACategories().Result.ToList();
                Entities.QACategory obj = new Entities.QACategory
                {
                    Id = -1,
                    Name = "Select",
                    ParentId = -1,
                    Code = "",
                    Description = ""
                };
                objCategory.Add(obj);

                //Entities.QACategory obj = new Entities.QACategory { Id = -1, Name = "Select", ParentId = -1, Code = "", Description = "" };                
                rptLookUpEditCategory.DataSource = objCategory.OrderBy(p => p.Id); // QACategories.Result.ToList();
                rptLookUpEditCategory.ValueMember = "Id";
                rptLookUpEditCategory.DisplayMember = "Name";
                LookUpColumnInfo lookupColumnInfo = new LookUpColumnInfo { Caption = "", FieldName = "Name" };
                rptLookUpEditCategory.Columns.Add(lookupColumnInfo);
                barCategory.EditValue = -1;// QACategories.Result.ToList()[0].Id;
                LookUpColumnInfo lookupColumnInfo1 = new LookUpColumnInfo { Caption = "", FieldName = "Name" };
                rptLookUpEditSubCategory.Columns.Add(lookupColumnInfo1);
                //rptLookUpEditSubCategory.DataSource = null;
                txtQANote.Enabled = false;
            }
            else if (EditorCore.Settings.Editor.Type.ToLowerInvariant() == "editor")
            {
                grpQA.Visible = false;
            }

            ribbonControl1.Minimized = !EditorCore.Settings.RibbonExpanded;

            if (WindowState != FormWindowState.Maximized)
            {
                var size = EditorCore.Settings.GetSetting("editor-window-size", new Size(1024, 768));

                // Make sure a bogus value didn't get stored
                if (size.Height > 200 && size.Width > 200)
                    Size = size;
            }
        }

        public EditorForm(bool mustChangePassword)
            : this()
        {
            must_change_password = mustChangePassword;

            if (must_change_password)
            {
                btnStartEditing.Enabled = !must_change_password;
                btnManualDownload.Enabled = !must_change_password;
            }
        }

        #region Form Events
        private void EditorForm_Load(object sender, EventArgs e)
        {
            // Log some info about the sound capabilities of this PC
            var count = audioDjStudio1.GetOutputDevicesCount();
            var device_to_use = (short)0;

            EditorCore.LogDebug("PreferredAudioDevice: {0}", EditorCore.Settings.PreferredAudioDevice);
            EditorCore.LogDebug("Available output devices ({0}):", count);

            for (short i = 0; i < count; i++)
            {
                EditorCore.LogDebug("- " + i + ": " + audioDjStudio1.GetOutputDeviceDesc(i));

                if (string.Compare(audioDjStudio1.GetOutputDeviceDesc(i), EditorCore.Settings.PreferredAudioDevice, true) == 0)
                    device_to_use = i;
            }

            if (count == 0)
                MessageBox.Show("No audio output devices found, audio will be disabled.");
            else
                EditorCore.LogDebug("Going to use device: {0}", audioDjStudio1.GetOutputDeviceDesc(device_to_use));

            has_audio = count > 0;

            using (EditorCore.CreateStopwatch("InitSoundSystem"))
                audioDjStudio1.InitSoundSystem(1, device_to_use, 0, 0, 0);

            EditorCore.Settings.ActiveAudioDevice = device_to_use;

            // Spelling options
            spellChecker1.OptionsSpelling.IgnoreEmails = EditorCore.Settings.IgnoreEmails.ToDefaultBool();
            spellChecker1.OptionsSpelling.IgnoreMixedCaseWords = EditorCore.Settings.IgnoreMixedCaseWords.ToDefaultBool();
            spellChecker1.OptionsSpelling.IgnoreRepeatedWords = EditorCore.Settings.IgnoreRepeatedWords.ToDefaultBool();
            spellChecker1.OptionsSpelling.IgnoreUpperCaseWords = EditorCore.Settings.IgnoreUpperCaseWords.ToDefaultBool();
            spellChecker1.OptionsSpelling.IgnoreUrls = EditorCore.Settings.IgnoreUrls.ToDefaultBool();
            spellChecker1.OptionsSpelling.IgnoreWordsWithNumbers = EditorCore.Settings.IgnoreWordsWithNumbers.ToDefaultBool();

            audioDjStudio1.SetRewindOnEOF(false);
            audioDjStudio1.SoundDone += delegate(object s, AudioDjStudio.PlayerEventArgs e2) { UpdateAudioControls(); };

            tmrDownloadQueue.Start();

            InitializeFootPedal();

            // Set up our spell checker dictionaries
            spellChecker1.Dictionaries.AddRange(this.CreateDictionaries());
        }

        private async void EditorForm_Shown(object sender, EventArgs e)
        {
            IEnumerable<Entrada.Entities.MedicalJobEntity> missing_jobs = null;

            try
            {
                missing_jobs = await EditorCore.Jobs.ReconcileJobs();
            }
            catch (ApplicationTerminatingException)
            {
                Application.Exit();
                return;
            }

            if (missing_jobs.Count() > 0)
            {
                using (var dialog = new JobCleanupDialog(missing_jobs.Select(p => p.JobNumber)))
                {
                    dialog.LabelText = "There are jobs that are currently assigned to you that aren't downloaded.  Do you want to Download them or Release them?";
                    dialog.Option1Text = "Download";
                    dialog.Option2Text = "Release";
                    dialog.AllowCancel = false;
                    dialog.Text = "Reconcile Claimed Jobs";
                    dialog.ContinueButtonText = "Continue";

                    dialog.Option1Action = (j) =>
                    {
                        return Task.Factory.StartNew(() =>
                        {
                            Invoke((Action)(() =>
                            {
                                var med_job = missing_jobs.FirstOrDefault(p => p.JobNumber == j);

                                if (med_job != null)
                                {
                                    med_job.SetStatus(DownloadedJobStatus.Claimed);
                                    EditorCore.Jobs.AddJob(med_job);
                                }
                            }));
                        });
                    };

                    dialog.Option2Action = (j) => { return EditorCore.Jobs.ReleaseJob(j); };

                    dialog.ShowDialog();
                }
            }

            if (must_change_password)
                MessageBox.Show("Your password has expired and must be changed before you can claim any more jobs.  To change your password, go to \"Change Password\" on the Advanced ribbon tab.");
        }

        private void EditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the automatic queue so we don't download new jobs when we release these
            EditorCore.Jobs.StopQueue();

            if (EditorCore.Jobs.ClaimedJobs.Count > 0)
            {
                using (var dialog = new JobCleanupDialog(EditorCore.Jobs.ClaimedJobs.Select(p => p.JobNumber + (p.GetStatus() == DownloadedJobStatus.InProgress ? " (modified)" : ""))))
                {
                    dialog.Option1Action = (j) =>
                    {
                        return Task.Factory.StartNew(() =>
                        {
                            var doc = EditorCore.Documents.GetDocument(j);

                            if (doc != null)
                                doc.Save();
                        });
                    };

                    dialog.Option2Action = (j) =>
                    {
                        var doc = EditorCore.Documents.GetDocument(j);

                        if (doc != null && (doc.IsDirty || doc.HasWorkInProgress))
                        {
                            var msg = string.Format("Job {0} has been modified. Do you really want to release it?", doc.JobNumber);

                            if (!EditorCore.Background.RaiseConfirmAction(msg))
                            {
                                doc.Save();
                                return Task.Factory.StartNew(() => { });
                            }
                        }

                        DoCloseSound();
                        return EditorCore.Jobs.ReleaseJob(j);
                    };

                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            if (WindowState != FormWindowState.Maximized)
                EditorCore.Settings.PutSetting("editor-window-size", Size);

            // Spelling options
            EditorCore.Settings.IgnoreEmails = spellChecker1.OptionsSpelling.IgnoreEmails.ToBool();
            EditorCore.Settings.IgnoreMixedCaseWords = spellChecker1.OptionsSpelling.IgnoreMixedCaseWords.ToBool();
            EditorCore.Settings.IgnoreRepeatedWords = spellChecker1.OptionsSpelling.IgnoreRepeatedWords.ToBool();
            EditorCore.Settings.IgnoreUpperCaseWords = spellChecker1.OptionsSpelling.IgnoreUpperCaseWords.ToBool();
            EditorCore.Settings.IgnoreUrls = spellChecker1.OptionsSpelling.IgnoreUrls.ToBool();
            EditorCore.Settings.IgnoreWordsWithNumbers = spellChecker1.OptionsSpelling.IgnoreWordsWithNumbers.ToBool();

            EditorCore.Settings.RibbonExpanded = !ribbonControl1.Minimized;
            EditorCore.Settings.SaveSettings();
            dockManager1.SaveLayoutToXml(EditorCore.Settings.LayoutSettingsFile);

            // Let our available jobs grid save its layout
            var tab = documentManager1.FindTab<AvailableJobsTab>();

            if (tab != null)
                tab.SaveSettings();

            EditorCore.Editor.EndWork();
        }

        private void DownloadedJobList_DoubleClick(object sender, EventArgs e)
        {
            if (lstDownloadedJobs.SelectedIndex < 0)
                return;

            var job = lstDownloadedJobs.SelectedJob;

            // If the job isn't available for editing, bail..
            switch (job.GetStatus())
            {
                case DownloadedJobStatus.Claimed:
                case DownloadedJobStatus.Downloading:
                case DownloadedJobStatus.Completed:
                case DownloadedJobStatus.Sending:
                    return;
            }

            if (EditorCore.Documents.IsDocumentOpen(job.JobNumber))
                EditorCore.Documents.ActivateDocument(EditorCore.Documents.GetDocument(job.JobNumber));
            else
                EditorCore.Documents.OpenDocument(job);
        }

        private void Background_ConfirmAction(object sender, ConfirmationEventArgs e)
        {
            if (MessageBox.Show(e.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
        }

        private void Background_ShowMessage(object sender, ConfirmationEventArgs e)
        {
            MessageBox.Show(e.Text, e.Title);
        }

        private async void Background_ShowIdleWarning(object sender, EventArgs e)
        {
            EditorCore.LogDebug("Showing idle warning dialog.");

            using (var dialog = new IdleWarningDialog())
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                {
                    EditorCore.LogDebug("Idle timeout, going to release all jobs.");

                    // Stop the download queue and release all already claimed jobs
                    EditorCore.Jobs.StopQueue();
                    closing_all_tabs = true;
                    EditorCore.Documents.CloseAllDocuments();
                    await EditorCore.Jobs.ReleaseAllJobs();
                    closing_all_tabs = false;

                    // Close the app
                    EditorCore.LogDebug("Jobs released, closing app.");
                    Close();
                }
                else
                {
                    EditorCore.LogDebug("Idle timeout canceled.");
                }
        }

        private void EditorForm_KeyDown(object sender, KeyEventArgs e)
        {
            var doc = EditorCore.Documents.ActiveDocument;

            if (doc == null)
                return;

            var rtf = this.GetActiveRichEditPanel();

            if (rtf == null)
                return;

            // If this is a TDD document, see if we need to handle this
            // key press as "apply TDD tag"
            if (!doc.IsPreview && doc.IsTddJob)
            {
                if (e.Alt && e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                {
                    var value = e.KeyValue - 48;

                    // We want the 0 key to act like a 10
                    if (value == 0)
                        value = 10;

                    if (value <= lstTags.ItemCount)
                    {
                        var tag = doc.TddJobType.Tags[value - 1];
                        ApplyTag(doc, rtf.RichEdit.Document, tag);
                        e.Handled = true;
                        return;
                    }
                }
            }

            // If we're in preview mode, we need to handle our own key presses
            // for F1 and F2 as the ribbon buttons are disabled
            if (!doc.IsPreview)
                return;

            if (e.KeyCode == Keys.F1)
            {
                btnFinishDocument_ItemClick(null, null);
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.F2)
            {
                btnSendToQA_ItemClick(null, null);
                e.Handled = true;
                return;
            }
        }

        private void lstTags_DoubleClick(object sender, EventArgs e)
        {
            if (lstTags.SelectedIndex < 0)
                return;

            var doc = EditorCore.Documents.ActiveDocument;

            if (doc == null)
                return;

            var rtf = this.GetActiveRichEditPanel();

            if (rtf == null)
                return;

            var tag = doc.TddJobType.Tags[lstTags.SelectedIndex];
            ApplyTag(doc, rtf.RichEdit.Document, tag);
        }

        private void grdDemographics_CellValueChanging(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            var doc = EditorCore.Documents.ActiveDocument;

            // If the user changed the job type, try to find a new template
            if (e.Row.Name == "rowJOBS_JOBTYPE")
            {
                doc.Demographics.JOBS_JOBTYPE = (string)e.Value;

                var template = doc.FindBestTemplate();

                if (!string.IsNullOrWhiteSpace(template))
                {
                    doc.SelectedDocumentTemplate = template;
                    barJobTemplate.Caption = string.Format("Template:  {0} ", Path.GetFileNameWithoutExtension(doc.SelectedDocumentTemplate));
                }

                lstTags.Items.Clear();

                if (doc.IsTddJob)
                    lstTags.Items.AddRange(doc.TddJobType.GetTagShortcuts().ToArray());
            }

            doc.IsDirty = true;
        }
        #endregion

        #region DocumentManager Event Handlers
        private DevExpress.XtraVerticalGrid.Rows.BaseRow GetCategoryRow(string name)
        {
            foreach (DevExpress.XtraVerticalGrid.Rows.BaseRow category in grdDemographics.Rows)
                if (category.Name == "category" + name)
                    return category;

            return null;
        }

        private async void Documents_DocumentActivated(object sender, DocumentEventArgs e)
        {
            EditorCore.LogDebug("Documents_DocumentActivated: null = {0}", e.Document == null);

            if (e.Document == null)
                return;

            EditorCore.LogDebug("Documents_DocumentActivated: {0}", e.Document.JobNumber);

            var doc = this.FindDocument(e.Document.JobNumber);

            if (doc == null)
                return;

            if (e.Document.AvailableJobTypes.Count == 0)
            {
                if (e.Document.Job.Encounter.Clinic.HasADT)
                {
                    var jts = await EditorCore.Jobs.GetAvailableJobTypes(e.Document.Job.Encounter.Clinic.Id);

                    if (jts != null)
                        e.Document.AvailableJobTypes.AddRange(jts);
                }
                else
                {
                    e.Document.AvailableJobTypes.Add(e.Document.Job.JobType);
                }
            }

            // Ensure this document wasn't closed while we were doing something
            // expensive like getting the available job types (race condition)
            if (doc.Control == null)
                return;

            grdDemographics.SelectedObject = e.Document.Demographics;

            // We have to set all custom fields to hidden, and then show ones
            // that are actually in use + change their caption to the field name
            var custom_fields_row = GetCategoryRow("Custom_Fields");

            foreach (DevExpress.XtraVerticalGrid.Rows.BaseRow field in custom_fields_row.ChildRows)
                field.Visible = false;

            for (var i = 1; i <= 50; i++)
            {
                var field_name = "CUSTOM" + i.ToString();
                var value = e.Document.Demographics.Raw["CUSTOM_" + field_name];
                var description = string.Empty;

                var custom_desc = e.Document.Demographics.CustomFieldDescriptions.FirstOrDefault(p => p.Key.ToUpperInvariant() == field_name);

                if (custom_desc.Key != null)
                    description = custom_desc.Value;

                if (string.IsNullOrWhiteSpace(description) && string.IsNullOrWhiteSpace(value))
                    continue;

                var row = custom_fields_row.ChildRows.GetRowByFieldName("CUSTOM_" + field_name);

                row.Visible = true;

                if (!string.IsNullOrWhiteSpace(description))
                    row.Properties.Caption = description;
            }

            // Populate the job type drop down in the demographics grid
            if (job_type_demographics_combo == null)
            {
                job_type_demographics_combo = new RepositoryItemComboBox();
                grdDemographics.RepositoryItems.Add(job_type_demographics_combo);
            }

            job_type_demographics_combo.Items.Clear();
            job_type_demographics_combo.Items.AddRange(e.Document.AvailableJobTypes);

            var job_row = GetCategoryRow("Job");
            var job_type_row = job_row.ChildRows.GetRowByFieldName("JOBS_JOBTYPE");

            job_type_row.Properties.RowEdit = job_type_demographics_combo;

            documentManager1.View.Controller.Activate(doc);
            richEditBarController1.Control = doc.GetPanel().RichEdit;

            if (has_audio)
            {
                audioDjStudio1.LoadSound(e.Document.Audio.AudioFile);

                audioDjStudio1.SetTempo(e.Document.Audio.Speed);
                audioDjStudio1.SetForwardRewindGranularity(EditorCore.Settings.FastForwardRewindSpeed);
                audioDjStudio1.Seek(e.Document.Audio.Position);
            }

            UpdateTrackbarLabel();

            if (doc.GetPanel() == null)
            {
                EditorCore.LogDebug("doc.GetPanel() is null");
                EditorCore.LogDebug("-- e.Document.JobNumber: {0}", e.Document.JobNumber);
                EditorCore.LogDebug("-- doc.caption: {0}", doc.Caption);
                EditorCore.LogDebug("-- doc.iseditor: {0}", doc.IsEditorTab());
                EditorCore.LogDebug("-- doc.getpanel: {0}", doc.GetPanel());
            }

            // Ensure this document wasn't closed while we were doing something
            // expensive like opening the sound file.
            if (doc.Control == null)
                return;

            UpdateRibbon(doc.GetPanel().Document.IsPreview);
            barZoomSlider.EditValue = (doc.Control as EditorTab).RichEdit.ActiveView.ZoomFactor * 100;

            UpdateAudioControls();
            UpdateJobTemplates();

            btnInsertMacro.Enabled = e.Document.Macros.Count > 0;
            //txtQANote.Text = e.Document.QANote;
            txtQANote.Text = e.Document.Job.QAData.LastQANote;
            txtQANote.Enabled = true;

            //////btnSendToQA.SetVisible (e.Document.CanSendToNextQA);
            barCreateNote.SetVisible(true);
            btnSameQA.SetVisible(e.Document.CanSendToSameQA);
            btnNextQA.SetVisible(e.Document.CanSendToNextQA);
            btnEntradaQA.SetVisible(e.Document.CanSendToEntradaQA);
            btnSendtoCR.SetVisible(e.Document.CanSendToCR);

            barCategory.EditValue = e.Document.Job.QAData.Category.ParentId;
            barSubCategory.EditValue = e.Document.Job.QAData.Category.Id;

            barCategory.SetVisible(e.Document.Job.QAData.CanReturnQACategory);
            barSubCategory.SetVisible(e.Document.Job.QAData.CanReturnQACategory);

            var has_adt = e.Document.Job.Encounter.Clinic.HasADT;

            if (EditorCore.Settings.Editor.Type.ToLowerInvariant() == "qa")
            {
                btnSplitJobType.Enabled = false;
                btnSplitPatient.Enabled = false;
                txtQANote.Enabled = false;
            }

            btnPatients.Enabled = has_adt;
            btnReferring.Enabled = has_adt;
            btnCC.Enabled = has_adt;
            btnSameQA.Enabled = has_adt;
            btnNextQA.Enabled = has_adt;
            btnEntradaQA.Enabled = has_adt;
            btnSendtoCR.Enabled = has_adt;
            barCategory.Enabled = has_adt;
            barSubCategory.Enabled = has_adt;
            barCreateNote.Enabled = has_adt;

            lstTags.Items.Clear();

            if (e.Document.IsTddJob)
                lstTags.Items.AddRange(e.Document.TddJobType.GetTagShortcuts().ToArray());

            EditorCore.LogDebug("Documents_DocumentActivated: COMPLETE");
        }

        private void SubCategoryByParentId(object sender, EventArgs e)
        {
            rptLookUpEditSubCategory.DataSource = null;

            int iParentID = Convert.ToInt32(barCategory.EditValue.ToString());
            List<Entities.QACategory> objCategory;
            if (iParentID != -1)
            {
                objCategory = JobRepository.GetSubQACategories(iParentID).Result.ToList();
            }
            else
            {
                objCategory = new List<Entities.QACategory>();
            }

            Entities.QACategory obj = new Entities.QACategory
            {
                Id = -1,
                Name = "Select",
                ParentId = -1,
                Code = "",
                Description = ""
            };
            objCategory.Add(obj);
            //Task<Entrada.Entities.QACategory[]> QASubCategories = JobRepository.GetSubQACategories(iParentID);

            rptLookUpEditSubCategory.DataSource = objCategory.OrderBy(a => a.Id);

            rptLookUpEditSubCategory.ValueMember = "Id";
            rptLookUpEditSubCategory.DisplayMember = "Name";
            barSubCategory.EditValue = -1;//QASubCategories.Result.ToList()[0].Id;

        }

        private void View_DocumentClosing(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentCancelEventArgs e)
        {
            EditorCore.LogDebug("View_DocumentClosing: null = {0}", e.Document == null);

            if (e.Document == null)
                return;

            EditorCore.LogDebug("View_DocumentClosing: {0}", e.Document.Control);

            if (e.Document.IsAvailableJobsTab())
                e.Document.GetAvailableJobsTab().OnClosing();

            if (e.Document.IsEditorTab())
            {
                var rtf = e.Document.GetEditorTab();

                // If we no longer have it claimed, we're closing it because we 
                // released it, so don't save it
                if (EditorCore.Jobs.ClaimedJobs.Contains(rtf.Document.Job))
                {
                    rtf.RichEdit.Document.ClearAllColorWords();
                    rtf.Document.Save();
                }

                closing_tab = true;
                EditorCore.Documents.CloseDocument(rtf.Document);
                closing_tab = false;
            }

            EditorCore.LogDebug("View_DocumentClosing: COMPLETE");
        }

        private void Documents_DocumentDeactivated(object sender, DocumentEventArgs e)
        {
            EditorCore.LogDebug("Documents_DocumentDeactivated: null = {0}", e.Document == null);

            if (e.Document == null)
                return;

            EditorCore.LogDebug("Documents_DocumentDeactivated: {0}", e.Document.JobNumber);

            grdDemographics.SelectedObject = null;
            lstTags.Items.Clear();

            rewinding = false;
            fastforwarding = false;
            tmrRewindForward.Stop();

            if (has_audio)
                audioDjStudio1.Pause();

            UpdateAudioControls();

            grpDocuments.Enabled = false;
            grpClientData.Enabled = false;
            grpQuickFix.Enabled = false;
            grpQA.Enabled = false;
            btnReleaseJob.Enabled = false;

            // Disabling the group doesn't disable shortcut keys,
            // so we need to disable the buttons too
            btnSplitJobType.Enabled = false;
            btnSplitPatient.Enabled = false;

            btnPatients.Enabled = false;
            btnReferring.Enabled = false;
            btnInsertMacro.Enabled = false;
            btnCC.Enabled = false;

            btnRewindAudio.Enabled = false;
            btnPlayAudio.Enabled = false;
            btnStopAudio.Enabled = false;
            btnFastForwardAudio.Enabled = false;
            btnDecreaseSpeed.Enabled = false;
            btnIncreaseSpeed.Enabled = false;

            btnAutoSentence.Enabled = false;
            btnNextAnomaly.Enabled = false;
            btnPreviousAnomaly.Enabled = false;
            btnSpellCheck.Enabled = false;
            btnMakeUpperCase.Enabled = false;

            barJobTemplate.SetVisible(false);
            trkAudioPosition.SetVisible(false);
            lblAudioPosition.SetVisible(false);
            barZoomSlider.SetVisible(false);

            btnSameQA.Enabled = false;
            btnNextQA.Enabled = false;
            btnEntradaQA.Enabled = false;
            btnSendtoCR.Enabled = false;
            barCategory.Enabled = false;
            barSubCategory.Enabled = false;
            barCreateNote.Enabled = false;

            e.Document.QANote = txtQANote.Text;
            txtQANote.Text = string.Empty;
            txtQANote.Enabled = false;

            EditorCore.LogDebug("Documents_DocumentDeactivated: COMPLETE");
        }

        private void Documents_DocumentClosed(object sender, DocumentEventArgs e)
        {
            EditorCore.LogDebug("Documents_DocumentClosed: {0}", e.Document.JobNumber);

            DoCloseSound();

            var doc = this.FindDocument(e.Document.JobNumber);

            // doc shouldn't ever be null, but stack traces say sometimes it is
            if (doc != null)
                doc.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.True;

            if (closing_tab)
                return;

            if (doc != null)
                documentManager1.View.Controller.Close(doc);

            if (!closing_all_tabs)
                EditorCore.Jobs.OpenNextJob();

            EditorCore.LogDebug("Documents_DocumentClosed: COMPLETE");
        }

        private void Documents_DocumentOpened(object sender, DocumentEventArgs e)
        {
            EditorCore.LogDebug("Documents_DocumentOpened: {0}", e.Document.JobNumber);

            var panel = new EditorTab(e.Document);

            panel.RichEdit.SpellChecker = spellChecker1;

            //if (e.Document.Job.IsGeneric)
            //    panel.AddStatusBanner ("generic", "Generic job, patient demographics must be entered.");
            if (e.Document.IsSplitJob)
                panel.AddStatusBanner("split", "Split job, audio will still contain other job(s).");

            var tab = this.CreateTab(panel, string.Format("Job {0}", e.Document.JobNumber), Resources.page, e.Document.JobNumber, true);

            e.Document.IsDirtyChanged += (o, _) => { tab.Caption = string.Format("Job {0}", e.Document.JobNumber) + (e.Document.IsDirty ? "*" : ""); };

            if (has_audio)
            {
                audioDjStudio1.LoadSound(e.Document.Audio.AudioFile);
                e.Document.Audio.Duration = audioDjStudio1.GetSoundDuration();
            }

            panel.Focus();

            EditorCore.LogDebug("Documents_DocumentOpened: COMPLETE");
        }

        private void View_DocumentActivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            if (e.Document.GetPanel() != null)
                EditorCore.Documents.ActivateDocument(e.Document.GetPanel().Document);
        }

        private void View_DocumentDeactivated(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e)
        {
            if (e.Document == null)
                return;

            if (e.Document.GetPanel() != null)
                EditorCore.Documents.DeactivateDocument(e.Document.GetPanel().Document);
        }
        #endregion

        #region JobManager Event Handlers
        private void Jobs_JobListUpdated(object sender, EventArgs e)
        {
            lstDownloadedJobs.Items.Clear();
            lstDownloadedJobs.Items.AddRange(EditorCore.Jobs.ClaimedJobs.ToArray());

            // If we're not closing all jobs and there's not a currently opened editor, open the next one
            // NOTE: This is for opening the next job when there wasn't already a job
            // open, like opening the first job of the session            
            if (!closing_all_tabs && this.documentManager1.FindTab<EditorTab>() == null)
                EditorCore.Jobs.OpenNextJob();
        }

        private void Jobs_JobListAddRemove(object sender, EventArgs e)
        {
            var text = "Downloaded Jobs";

            if (EditorCore.Jobs.ClaimedJobs.Count > 0)
                text += " (" + EditorCore.Jobs.ClaimedJobs.Count + ")";

            dockPanel4.Text = text;
        }

        private void Jobs_WorkQueueRunningChanged(object sender, EventArgs e)
        {
            btnStartEditing.Enabled = !EditorCore.Jobs.WorkQueueRunning;
            btnStopEditing.Enabled = EditorCore.Jobs.WorkQueueRunning;

            // Fire the queue timer now rather than waiting for the default length
            if (EditorCore.Jobs.WorkQueueRunning)
                tmrDownloadQueue.Interval = 1000;
        }
        #endregion

        #region Ribbon
        private void UpdateRibbon(bool preview)
        {
            grpDocuments.Enabled = !preview;
            btnReleaseJob.Enabled = !preview;
            grpClientData.Enabled = !preview;
            grpQuickFix.Enabled = !preview;
            grpAudio.Enabled = !preview;
            grpQA.Enabled = !preview;

            btnSplitJobType.Enabled = !preview;
            btnSplitPatient.Enabled = !preview;

            btnPatients.Enabled = !preview;
            btnReferring.Enabled = !preview;
            btnInsertMacro.Enabled = !preview;
            btnCC.Enabled = !preview;

            btnRewindAudio.Enabled = !preview;
            btnPlayAudio.Enabled = !preview;
            btnStopAudio.Enabled = !preview;
            btnFastForwardAudio.Enabled = !preview;
            btnDecreaseSpeed.Enabled = !preview;
            btnIncreaseSpeed.Enabled = !preview;

            btnAutoSentence.Enabled = !preview;
            btnNextAnomaly.Enabled = !preview;
            btnPreviousAnomaly.Enabled = !preview;
            btnSpellCheck.Enabled = !preview;
            btnMakeUpperCase.Enabled = !preview;

            trkAudioPosition.SetVisible(!preview);
            barZoomSlider.SetVisible(!preview);
            barJobTemplate.SetVisible(!preview);
            lblAudioPosition.SetVisible(!preview);

            btnSameQA.Enabled = !preview;
            btnNextQA.Enabled = !preview;
            btnEntradaQA.Enabled = !preview;
            btnSendtoCR.Enabled = !preview;
            barCategory.Enabled = !preview;
            barSubCategory.Enabled = !preview;
            barCreateNote.Enabled = !preview;

            grdDemographics.OptionsBehavior.Editable = !preview;
        }

        private void btnLaunchPortal_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("iexplore", "https://myentrada.entradahealth.net");
        }

        private void btnLaunchGuide_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("iexplore", "http://entradahealth.com/user-guides/Entrada_EditOne_Guide.pdf");
        }
        #endregion

        #region Status Bar
        private void Background_StatusUpdated(object sender, BackgroundStatusEventArgs e)
        {
            statusBarLabel.Caption = e.Text;

            if (!string.IsNullOrWhiteSpace(e.ImageHint))
                switch (e.ImageHint.ToLowerInvariant())
                {
                    case "refresh":
                        statusBarLabel.Glyph = Resources.update;
                        break;
                    case "merge":
                        statusBarLabel.Glyph = Resources.arrow_join;
                        break;
                    case "download":
                        statusBarLabel.Glyph = Resources.download;
                        break;
                    case "claim":
                        statusBarLabel.Glyph = Resources.document_todo;
                        break;
                    case "split":
                        statusBarLabel.Glyph = Resources.arrow_divide;
                        break;
                    case "release":
                        statusBarLabel.Glyph = Resources.document_import;
                        break;
                    case "reconcile":
                        statusBarLabel.Glyph = Resources.table_relationship;
                        break;
                }

            statusBarLabel.SetVisible(true);

            // Give the render loop time to redraw the status bar
            Application.DoEvents();
        }

        private void Background_StatusFinished(object sender, EventArgs e)
        {
            statusBarLabel.SetVisible(false);

            // Give the render loop time to redraw the status bar
            Application.DoEvents();
        }

        private void ZoomSlider_EditorValueChanging(object sender, ChangingEventArgs e)
        {
            var rtf = this.GetActiveRichEditPanel();
            rtf.RichEdit.ActiveView.ZoomFactor = ((float)(int)e.NewValue) / 100.0f;
        }

        private void UpdateJobTemplates()
        {
            var rtf = this.GetActiveRichEditPanel();

            if (rtf == null)
                return;

            var doc = rtf.Document;

            barJobTemplate.ClearLinks();

            foreach (var template in doc.AvailableDocumentTemplates)
            {
                var item = new BarButtonItem();
                item.Caption = template;
                item.ItemClick += (o, e) =>
                {
                    doc.SelectedDocumentTemplate = template;
                    barJobTemplate.Caption = string.Format("Template:  {0} ", Path.GetFileNameWithoutExtension(doc.SelectedDocumentTemplate));
                };

                barJobTemplate.AddItems(new BarItem[] { item });
            }

            if (!string.IsNullOrWhiteSpace(doc.SelectedDocumentTemplate))
                barJobTemplate.Caption = string.Format("Template:  {0} ", Path.GetFileNameWithoutExtension(doc.SelectedDocumentTemplate));
            else
                barJobTemplate.Caption = "Template:  None ";
        }
        #endregion

        #region Status Bar - Audio Trackbar
        private void UpdateTrackbarLabel()
        {
            var current = audioDjStudio1.GetSoundPositionString();
            var total = audioDjStudio1.GetSoundDuration();

            var format = string.Format("{0} / {1:00}:{2:00}:{3:00}", current, total / 3600000, total % 3600000 / 60000, total / 1000 % 60);
            lblAudioPosition.Caption = format;

            repositoryItemTrackBar1.Maximum = total / 1000;
            repositoryItemTrackBar1.LargeChange = total / 1000 / 10;

            if (!audioDjStudio1.IsSoundLoaded())
                return;

            var pos = audioDjStudio1.GetSoundPosition();

            trkAudioPosition.EditValue = (int)pos / 1000;

            var doc = EditorCore.Documents.ActiveDocument;

            if (doc != null)
            {
                doc.Audio.Position = (uint)audioDjStudio1.GetSoundPosition();
            }
        }

        private void UpdateAudioControls()
        {
            if (EditorCore.Documents.ActiveDocument == null || !has_audio)
            {
                btnRewindAudio.Enabled = false;
                btnPlayAudio.Enabled = false;
                btnStopAudio.Enabled = false;
                btnFastForwardAudio.Enabled = false;
                btnDecreaseSpeed.Enabled = false;
                btnIncreaseSpeed.Enabled = false;
                trkAudioPosition.Enabled = false;
                return;
            }

            btnRewindAudio.Enabled = true;
            btnPlayAudio.Enabled = true;
            btnStopAudio.Enabled = true;
            btnFastForwardAudio.Enabled = true;
            btnDecreaseSpeed.Enabled = true;
            btnIncreaseSpeed.Enabled = true;
            trkAudioPosition.Enabled = true;

            if (audioDjStudio1.GetStatus() == AudioDjStudio.enumPlayerStatus.SOUND_PLAYING)
            {
                btnPlayAudio.Enabled = false;
                btnStopAudio.Enabled = true;
            }
            else
            {
                btnPlayAudio.Enabled = true;
                btnStopAudio.Enabled = rewinding || fastforwarding;
            }

            btnIncreaseSpeed.Enabled = EditorCore.Documents.ActiveDocument.Audio.CanSpeedUp;
            btnDecreaseSpeed.Enabled = EditorCore.Documents.ActiveDocument.Audio.CanSlowDown;
        }

        private void tmrAudioPosition_Tick(object sender, EventArgs e)
        {
            UpdateTrackbarLabel();
        }

        private void trkAudioPosition_MouseDown(object sender, EventArgs e)
        {
            if (audioDjStudio1.GetStatus() == AudioDjStudio.enumPlayerStatus.SOUND_PLAYING)
            {
                scrubbing_pause = true;
                audioDjStudio1.Pause();
            }

            trackbar_mousedown = true;
            tmrAudioPosition.Stop();
        }

        private void trkAudioPosition_MouseUp(object sender, MouseEventArgs e)
        {
            if (scrubbing_pause)
                audioDjStudio1.Play();

            scrubbing_pause = false;
            trackbar_mousedown = false;
            tmrAudioPosition.Start();
        }

        private void trkAudioPosition_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (trackbar_mousedown)
                audioDjStudio1.Seek((uint)((int)e.NewValue * 1000));
        }
        #endregion

        #region Home Ribbon Group Handlers
        private void btnStartEditing_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditorCore.Jobs.StartQueue();
        }

        private void btnStopEditing_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditorCore.Jobs.StopQueue();
        }

        private void btnManualDownload_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (documentManager1.ActivateTab<AvailableJobsTab>())
                return;

            var panel = new AvailableJobsTab();
            this.CreateTab(panel, "Available Jobs", Resources.google_custom_search);
        }

        private async void btnFinishDocument_ItemClick(object sender, ItemClickEventArgs e)
        {
            var rtf = this.GetActiveRichEditPanel();

            if (rtf == null)
                return;

            var doc = EditorCore.Documents.ActiveDocument;

            if (EditorCore.Settings.Editor.Type.ToLowerInvariant() != "qa")
            {
                if (!doc.IsPreview)
                {
                    doc.PreviewFor = SendTo.Finished;
                    await PreviewDocument(rtf, doc);
                    return;
                }
            }

            rtf.RemoveStatusBanner("noqanote");

            if (doc.PreviewFor == SendTo.QA && string.IsNullOrWhiteSpace(txtQANote.Text))
            {
                rtf.AddStatusBanner("noqanote", "QA Note must be filled in to send a job to QA.");
                return;
            }

            // The document is already in final preview mode and the user hit
            // finish again, this means send it off to the client
            if (doc.PreviewFor == SendTo.Finished)
            {
                var dr = MessageBox.Show("Are you sure you want send this job to the Client?", "Confirm Document Finished", MessageBoxButtons.YesNo);

                if (dr == DialogResult.No)
                    return;
            }

            SaveAndSendDocument(rtf, doc);
        }

        private async void btnSendToQA_ItemClick(object sender, ItemClickEventArgs e)
        {
            var rtf = this.GetActiveRichEditPanel();

            if (rtf == null)
                return;

            var doc = EditorCore.Documents.ActiveDocument;

            if (!doc.IsPreview)
            {
                rtf.RemoveStatusBanner("noqanote");
                rtf.RemoveStatusBanner("notemplate");
                rtf.RemoveStatusBanner("anomaly");
                rtf.RemoveStatusBanner("demographics");
                rtf.RemoveStatusBanner("demo_error");
                rtf.RemoveStatusBanner("tdd_all_tagged");
                rtf.RemoveStatusBanner("tdd_invalid_tags");
                rtf.RemoveStatusBanner("tdd_missing_tags");
                rtf.RemoveStatusBanner("tdd_duplicate_tags");

                if (string.IsNullOrWhiteSpace(txtQANote.Text))
                {
                    rtf.AddStatusBanner("noqanote", "QA Note must be filled in to send a job to QA.");
                    return;
                }

                doc.PreviewFor = SendTo.QA;
                doc.Job.QAData.LastQANote = txtQANote.Text;
                await PreviewDocument(rtf, doc);
                return;
            }

            // The document is already in final preview mode and the user hit
            // F2, this means go back to edit mode
            if (doc.IsPreview)
            {
                rtf.RichEdit.LoadDocument(rtf.EditedDocument, DocumentFormat.OpenXml);
                rtf.EditedDocument.Dispose();
                rtf.EditedDocument = null;

                doc.IsPreview = false;

                try
                {
                    rtf.RichEdit.ActiveViewType = RichEditViewType.Simple;
                }
                catch (Exception)
                {
                    // We're going to ignore errors here.  It only shows up on
                    // Kelley's machine as a caret creation exception.
                }

                rtf.RemoveStatusBanner("finalpreview");
                UpdateRibbon(false);

                return;
            }
        }

        private async void btnSplitJobType_ItemClick(object sender, ItemClickEventArgs e)
        {
            var rtf = this.GetActiveRichEditPanel().RichEdit;
            var selection = rtf.Document.Selection;

            var text = rtf.Document.GetText(selection);

            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("No text is selected for the new job.  Please select the text that needs to used for the new job.");
                return;
            }

            using (var dialog = new SplitByJobTypeDialog(EditorCore.Documents.ActiveDocument))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var orig_job = EditorCore.Documents.ActiveDocument.JobNumber;
                    var jobtype = dialog.SelectedJobType;
                    var pat = EditorCore.Documents.ActiveDocument.Demographics.ToPatient();

                    var success = await EditorCore.Jobs.SplitJobByJobType(orig_job, jobtype, pat, text);

                    if (success)
                        if (dialog.CopyOrMove == SplitByJobTypeDialog.CopyMove.Move)
                            rtf.Document.Delete(selection);
                }
            }
        }

        private async void btnSplitPatient_ItemClick(object sender, ItemClickEventArgs e)
        {
            var rtf = this.GetActiveRichEditPanel().RichEdit;
            var selection = rtf.Document.Selection;

            var text = rtf.Document.GetText(selection);

            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("No text is selected for the new job.  Please select the text that needs to used for the new job.");
                return;
            }

            PatientDialog dialog = null;
            var word = rtf.Document.GetWordAtCaret();

            if (word.IsNumber())
                dialog = new PatientDialog(word);
            else
                dialog = new PatientDialog();

            dialog.AcceptButtonText = "&Split Job";
            dialog.AllowGenericSplit = true;

            using (dialog)
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var orig_job = EditorCore.Documents.ActiveDocument.JobNumber;

                    // This really shouldn't happen..
                    if (dialog.SelectedPatient == null)
                        return;

                    var success = await EditorCore.Jobs.SplitJobByPatient(orig_job, dialog.SelectedPatient, text);

                    if (success)
                        rtf.Document.Delete(selection);
                }
        }

        private void btnPatients_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var panel = this.GetActiveRichEditPanel();

            if (panel == null)
                return;

            var word = panel.RichEdit.Document.GetWordAtCaret();

            PatientDialog dialog = null;

            if (word.IsNumber())
                dialog = new PatientDialog(word);
            else
                dialog = new PatientDialog();

            using (dialog)
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    panel.RemoveStatusBanner("generic");

                    var doc = EditorCore.Documents.ActiveDocument;

                    // This really shouldn't happen..
                    if (dialog.SelectedPatient == null)
                        return;

                    doc.Demographics.FromPatient(dialog.SelectedPatient);
                    doc.Job.Encounter.Patient = dialog.SelectedPatient;

                    EditorCore.Jobs.FireClaimedJobsChanged();

                    if (dialog.HasSelectedAppointment)
                    {
                        doc.Demographics.JOBS_APPOINTMENTDATE = dialog.SelectedDate.ToShortDateString();
                        doc.Demographics.JOBS_APPOINTMENTTIME = dialog.SelectedTime.HasValue ? dialog.SelectedTime.Value.ToString() : "";
                    }

                    // Tell the grid to read the Demographics changes
                    grdDemographics.Refresh();
                }
        }

        private void btnReferring_ItemClick(object sender, ItemClickEventArgs e)
        {
            var panel = this.GetActiveRichEditPanel();

            if (panel == null)
                return;

            var word = panel.RichEdit.Document.GetWordAtCaret();

            ReferringDialog dialog = null;

            if (word.IsNumber())
                dialog = new ReferringDialog(word);
            else
                dialog = new ReferringDialog();

            using (dialog)
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var doc = EditorCore.Documents.ActiveDocument;

                    // This really shouldn't happen..
                    if (dialog.SelectedPhysician == null)
                        return;

                    doc.Demographics.FromPhysician(dialog.SelectedPhysician);

                    var text = dialog.SelectedPhysician.ToFullFormat();

                    if (!string.IsNullOrWhiteSpace(text))
                        Clipboard.SetText(text);

                    // Tell the grid to read the Demographics changes
                    grdDemographics.Refresh();
                }
        }

        private void btnInsertMacro_ItemClick(object sender, ItemClickEventArgs e)
        {
            var panel = this.GetActiveRichEditPanel();
            //this.do
            if (panel == null)
                return;

            var rich_edit = panel.RichEdit;
            var document = panel.Document;

            using (var dialog = new MacroDialog(document))
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var macro = dialog.SelectedMacroText;

                    var result = rich_edit.Document.InsertHtmlText(rich_edit.Document.CaretPosition, macro, InsertOptions.MatchDestinationFormatting);

                    document.InsertedMacros.Add(new InsertedMacroEntity(dialog.SelectedMacroName, result.Length));
                }

            rich_edit.Focus();
        }

        private void btnCC_ItemClick(object sender, ItemClickEventArgs e)
        {
            var panel = this.GetActiveRichEditPanel();

            // This shouldn't happen, but a crash report says it is
            if (panel == null)
                return;

            string word = null;

            if (panel.RichEdit != null && panel.RichEdit.Document != null)
                word = panel.RichEdit.Document.GetWordAtCaret();

            ReferringDialog dialog = null;

            if (word != null && word.IsNumber())
                dialog = new ReferringDialog(word);
            else
                dialog = new ReferringDialog();

            dialog.AcceptButtonText = "Add CC";

            using (dialog)
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var doc = EditorCore.Documents.ActiveDocument;

                    // This really shouldn't happen..
                    if (dialog.SelectedPhysician == null)
                        return;

                    doc.CC.Add(new CCEntity(dialog.SelectedPhysician));
                    doc.RaiseCCChanged();

                    doc.Demographics.JOBS_CC = doc.CC.Count > 0;
                    grdDemographics.Refresh();

                    var text = dialog.SelectedPhysician.ToFullFormat();

                    if (!string.IsNullOrWhiteSpace(text))
                        Clipboard.SetText(text);

                    doc.IsDirty = true;
                }
        }

        private void btnRewindAudio_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.GetActiveRichEditPanel() == null)
                return;

            DoRewind();
        }

        private void btnPlayAudio_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.GetActiveRichEditPanel() == null)
                return;

            DoPlay();
        }

        private void btnStopAudio_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.GetActiveRichEditPanel() == null)
                return;

            DoStop(BackStepAction.Keyboard);
        }

        private void btnFastForwardAudio_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.GetActiveRichEditPanel() == null)
                return;

            DoFastForward();
        }

        private void btnDecreaseSpeed_ItemClick(object sender, ItemClickEventArgs e)
        {
            var doc = EditorCore.Documents.ActiveDocument;

            if (doc != null)
            {
                doc.Audio.SlowDown();
                audioDjStudio1.SetTempo(doc.Audio.Speed);
                UpdateAudioControls();
            }
        }

        private void btnIncreaseSpeed_ItemClick(object sender, ItemClickEventArgs e)
        {
            var doc = EditorCore.Documents.ActiveDocument;

            if (doc != null)
            {
                doc.Audio.SpeedUp();
                audioDjStudio1.SetTempo(doc.Audio.Speed);
                UpdateAudioControls();
            }
        }
        #endregion

        #region Format Ribbon Group Handlers
        private void btnAutoSentence_ItemClick(object sender, ItemClickEventArgs e)
        {
            var panel = this.GetActiveRichEditPanel();
            panel.RichEdit.Document.AutoSentence();
        }

        private void btnNextAnomaly_ItemClick(object sender, ItemClickEventArgs e)
        {
            var panel = this.GetActiveRichEditPanel();
            var anom = panel.RichEdit.Document.FindNextAnomaly();

            if (anom != null)
                panel.RichEdit.Document.Selection = anom;
        }

        private void btnPreviousAnomaly_ItemClick(object sender, ItemClickEventArgs e)
        {
            var panel = this.GetActiveRichEditPanel();
            var anom = panel.RichEdit.Document.FindPreviousAnomaly();

            if (anom != null)
                panel.RichEdit.Document.Selection = anom;
        }

        private void btnSpellCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            var panel = this.GetActiveRichEditPanel();

            try
            {
                spellChecker1.Check(panel.RichEdit);
            }
            catch (IndexOutOfRangeException)
            {
                // Ignore this error that the spell check sometimes throws
            }
            catch (ArgumentOutOfRangeException)
            {
                // Ignore this error that the spell check sometimes throws
            }
        }

        private void btnMakeUpperCase_ItemClick(object sender, ItemClickEventArgs e)
        {
            var panel = this.GetActiveRichEditPanel();

            if (panel != null && panel.RichEdit != null)
            {
                var text = panel.RichEdit.Document.GetSelectedTextOrWord();

                RichEditCommand command;

                if (text != text.ToUpperInvariant())
                    command = new MakeTextUpperCaseCommand(panel.RichEdit);
                else
                    command = new MakeTextLowerCaseCommand(panel.RichEdit);

                command.ForceExecute(command.CreateDefaultCommandUIState());
            }
        }
        #endregion

        #region Advanced Ribbon Group Handlers
        private async void btnReleaseJob_ItemClick(object sender, ItemClickEventArgs e)
        {
            var rtf = this.GetActiveRichEditPanel();

            if (rtf == null)
                return;

            // Prevent button from being double clicked
            btnReleaseJob.Enabled = false;

            var doc = EditorCore.Documents.ActiveDocument;

            // Make sure we aren't playing or RW/FF the sound
            DoStop(BackStepAction.None);

            if (doc.IsDirty || doc.HasWorkInProgress)
            {
                var msg = string.Format("Job {0} has been modified. Do you really want to release it?", doc.JobNumber);

                if (!EditorCore.Background.RaiseConfirmAction(msg))
                {
                    btnReleaseJob.Enabled = true;
                    return;
                }
            }

            DoCloseSound();
            var success = await EditorCore.Jobs.ReleaseJob(doc.JobNumber);

            if (success)
                EditorCore.Documents.CloseDocument(doc);
        }

        private void btnWorkSummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (documentManager1.ActivateTab<WorkSummaryTab>())
                return;

            var panel = new WorkSummaryTab();
            this.CreateTab(panel, "Work Summary", Resources.chart_line);
        }

        private void btnPreferences_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (documentManager1.ActivateTab<PreferencesTab>())
                return;

            var panel = new PreferencesTab(audioDjStudio1);
            this.CreateTab(panel, "Preferences", Resources.interface_preferences);
        }

        private void btnAutoCorrect_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (documentManager1.ActivateTab<AutoCorrectOptionsTab>())
                return;

            var panel = new AutoCorrectOptionsTab();
            var tab = this.CreateTab(panel, "AutoCorrect", Resources.wand);

            panel.ModifiedChanged += (o, _) => { tab.Caption = string.Format("AutoCorrect {0}", panel.Modified ? "*" : ""); };
        }

        private void btnResetLayout_ItemClick(object sender, ItemClickEventArgs e)
        {
            // SHOULD always exist, but just in case..
            try
            {
                if (File.Exists(EditorCore.Settings.DefaultLayoutSettingsFile))
                    dockManager1.RestoreLayoutFromXml(EditorCore.Settings.DefaultLayoutSettingsFile);
            }
            catch (Exception ex)
            {
                // Log and ignore
                EditorCore.LogException("Could not restore DefaultLayoutSettingsFile.", ex);
            }

            // Delete existing layout customization file
            if (File.Exists(EditorCore.Settings.LayoutSettingsFile))
                File.Delete(EditorCore.Settings.LayoutSettingsFile);

            // Delete any available job grid customizations
            if (File.Exists(EditorCore.Settings.AvailableJobsGridFile))
                File.Delete(EditorCore.Settings.AvailableJobsGridFile);

            // Reset if the AvailableJobs tab is currently open
            var tab = documentManager1.FindTab<AvailableJobsTab>();

            if (tab != null)
                tab.ResetToDefaultSettings();
        }

        private void btnChangePassword_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (EditorCore.Jobs.ClaimedJobs.Count > 0)
            {
                var msg = "You currently have claimed jobs.  You will need to complete or release them before you can change your password.";
                MessageBox.Show(msg);
                return;
            }

            using (var dialog = new PasswordChangeDialog(true))
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    MessageBox.Show("Password successfully changed.");
                    must_change_password = false;
                    btnStartEditing.Enabled = true;
                    btnManualDownload.Enabled = true;
                }
        }

        private void btnLogFolder_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("explorer.exe", EditorCore.Settings.LogsDirectory);
        }

        private void btnSendLogs_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var dialog = new EmailLogsDialog())
                dialog.ShowDialog();
        }
        #endregion

        #region Document
        private async Task PreviewDocument(EditorTab rtf, DocumentEntity doc)
        {
            var valid = true;

            rtf.RemoveStatusBanner("noqanote");
            rtf.RemoveStatusBanner("notemplate");
            rtf.RemoveStatusBanner("anomaly");
            rtf.RemoveStatusBanner("demographics");
            rtf.RemoveStatusBanner("demo_error");
            rtf.RemoveStatusBanner("tdd_all_tagged");
            rtf.RemoveStatusBanner("tdd_invalid_tags");
            rtf.RemoveStatusBanner("tdd_missing_tags");
            rtf.RemoveStatusBanner("tdd_duplicate_tags");

            // Make sure a job template is selected
            if (string.IsNullOrWhiteSpace(doc.SelectedDocumentTemplate))
            {
                rtf.AddStatusBanner("notemplate", "No template is selected for this job.");
                valid = false;
            }

            // Make sure there are no anomalies left
            if (doc.PreviewFor == SendTo.Finished && rtf.RichEdit.Document.FindAnomalies().Count() > 0)
            {
                rtf.AddStatusBanner("anomaly", "This job cannot be finished because it still has anomalies ( ____ ).");
                valid = false;
            }

            // Make sure there are no anomalies left
            if (doc.PreviewFor == SendTo.Finished && doc.Demographics.GetRequiredDemographics().Count() > 0)
            {
                rtf.AddStatusBanner("demographics", string.Format("Required demographics missing: {0}.", string.Join(", ", doc.Demographics.GetRequiredDemographics())));
                valid = false;
            }

            // Make sure none of the Demographics are in an error state
            if (grdDemographics.ActiveEditor != null && !string.IsNullOrWhiteSpace(grdDemographics.ActiveEditor.ErrorText))
            {
                rtf.AddStatusBanner("demo_error", "There is invalid data in the demographics list.");
                valid = false;
            }

            if (CheckForGenderDiscrepancies(rtf))
                valid = false;

            if (CheckForDirectionalDiscrepancies(rtf))
                valid = false;

            // If this is a TDD job, let's do some TDD validation!
            if (doc.IsTddJob)
            {
                var sections = DocumentTagger.GetTags(rtf.RichEdit.Document, doc.TddJobSeparator, doc.TddPatientSeparator, true);

                // If all text must be tagged, ensure that it is
                if (doc.TddJobType.AllTagged && sections.Any(p => !(p is TaggedSection) && !p.IsEmpty))
                {
                    rtf.AddStatusBanner("tdd_all_tagged", "This job type requires all job content to be tagged.");
                    valid = false;
                }

                var tags = sections.Where(p => p is TaggedSection).Cast<TaggedSection>();

                // Check that all found tags are valid for this document
                var invalid_tags = tags.Where(p => !doc.TddJobType.IsValidTag(p.Name)).Select(p => p.Name);

                if (invalid_tags.Count() > 0)
                {
                    rtf.AddStatusBanner("tdd_invalid_tags", "The following tags are invalid: " + string.Join(", ", invalid_tags) + ".");
                    valid = false;
                }

                // Check that all required tags are in the document
                var required_tags = doc.TddJobType.Tags.Where(p => p.Required).Select(p => p.Name);
                var missing_tags = required_tags.Except(tags.Select(p => p.Name));

                if (missing_tags.Count() > 0)
                {
                    rtf.AddStatusBanner("tdd_missing_tags", "The following required tags are missing: " + string.Join(", ", missing_tags) + ".");
                    valid = false;
                }

                // Ensure that a tag isn't used twice
                var duplicates = tags.Select(p => p.Name).GroupBy(p => p).Where(p => p.Count() > 1).Select(p => p.Key);

                if (duplicates.Count() > 0)
                {
                    rtf.AddStatusBanner("tdd_duplicate_tags", "The following tags are used more than once: " + string.Join(", ", duplicates) + ".");
                    valid = false;
                }
            }

            if (!valid)
                return;

            // Remove any highlighted gender/directional words
            rtf.RemoveStatusBanner("gender");
            rtf.RemoveStatusBanner("direction");
            rtf.RichEdit.Document.ClearAllColorWords();

            btnSpellCheck_ItemClick(null, null);

            UpdateRibbon(true);

            using (EditorCore.CreateStatusUpdate("Merging document..", "merge"))
            {
                // Save the editor's work
                var old = rtf.RichEdit.Document;

                var ms = new MemoryStream();
                rtf.RichEdit.SaveDocument(ms, DocumentFormat.OpenXml);

#if DEBUG_DOCUMENTS
                rtf.RichEdit.SaveDocument (@"C:\temp\stored.docx", DocumentFormat.OpenXml);
#endif
                ms.Position = 0;
                rtf.EditedDocument = ms;

                // We're going to overwrite the clipboard, so
                // try to preserve any text there
                var old_text = string.Empty;

                if (Clipboard.ContainsText())
                    old_text = Clipboard.GetText();

                // Put the editor's work on the clipboard
                old.Copy(old.Range);

                // Also put the editor's work in the DocumentEntity
                doc.EditedText = rtf.RichEdit.HtmlText;
                doc.VRReturnText = rtf.RichEdit.Text;

                // Use Aspose to do the mail merge that DevExpress can't handle
                using (var s = await doc.Merge())
                using (EditorCore.CreateStopwatch("Loading merged doc"))
                    rtf.RichEdit.LoadDocument(s, DocumentFormat.Doc);

                // Insert the editor's work into the finished document
                using (EditorCore.CreateStopwatch("Replace TEXT_BODY_VR"))
                {
                    var vr = rtf.RichEdit.Document.FindAll("«TEXT_BODY_VR»", SearchOptions.None);

                    // Some templates may not have a place to replace text
                    if (vr != null && vr.Length > 0)
                    {
                        rtf.RichEdit.Document.Selection = vr[0];
                        rtf.RichEdit.Document.Paste();
                    }
                }

                // Place any text back on the clipboard
                if (string.IsNullOrWhiteSpace(old_text))
                    Clipboard.Clear();
                else
                    try
                    {
                        Clipboard.SetText(old_text);
                    }
                    catch (Exception ex)
                    {
                        // Log and ignore
                        EditorCore.LogException("Clipboard.SetText exception:", ex);
                    }

#if DEBUG_DOCUMENTS
                rtf.RichEdit.SaveDocument (@"C:\temp\vr_inserted.doc", DocumentFormat.Doc);
#endif

                // Set up the UI for the print preview
                rtf.RichEdit.ActiveViewType = RichEditViewType.PrintLayout;

                if (doc.PreviewFor == SendTo.Finished)
                    rtf.AddStatusBanner("finalpreview", "Final preview: Press F1 to send to client, F2 to go back to editing.");
                else if (doc.PreviewFor == SendTo.QA)
                    rtf.AddStatusBanner("finalpreview", "QA preview: Press F1 to send to QA, F2 to go back to editing.");

                doc.IsPreview = true;
            }
        }

        private void SaveAndSendDocument(EditorTab rtf, DocumentEntity doc)
        {
            // Save the demographics
            doc.Save();

            Directory.CreateDirectory(Path.Combine(EditorCore.Settings.JobsDirectory, doc.JobNumber, "Output"));

            // Save the final document
            var final = Path.Combine(EditorCore.Settings.JobsDirectory, doc.JobNumber, "Output", doc.JobNumber + ".doc");

            using (var final_stream = new MemoryStream())
            {
                rtf.RichEdit.SaveDocument(final_stream, DocumentFormat.Doc);

#if DEBUG_DOCUMENTS
                rtf.RichEdit.SaveDocument (@"C:\temp\before_add_properties.doc", DocumentFormat.Doc);
#endif

                // DevExpress doesn't support document properties, so we have to
                // use Aspose to put them back in case QA needs them
                using (var final_aspose_stream = doc.AddCustomProperties(final_stream))
                    EncryptedFileSystem.WriteMemoryStream(final, final_aspose_stream);
            }

            // Save the "truth text" to send back to VR test
            var vr = Path.Combine(EditorCore.Settings.JobsDirectory, doc.JobNumber, "Output", doc.JobNumber + ".txt");

            using (var truth_stream = EncryptedFileSystem.GetStreamWriter(vr))
                truth_stream.Write(doc.VRReturnText);

            // Save the inserted macro data
            var macro = Path.Combine(EditorCore.Settings.JobsDirectory, doc.JobNumber, "Output", doc.JobNumber + ".cnt");

            using (var macro_stream = EncryptedFileSystem.GetStreamWriter(macro))
            using (var xw = XmlWriter.Create(macro_stream))
            {
                xw.WriteStartElement("Macros");

                foreach (var m in doc.InsertedMacros)
                {
                    xw.WriteStartElement("Macro");
                    xw.WriteAttributeString("Name", m.Name);
                    xw.WriteElementString("NumCharsUnEdited", m.UneditedCharacterCount.ToString());
                    xw.WriteEndElement();
                }

                xw.WriteEndElement();
            }

            EditorCore.Documents.SendDocument(doc);
        }

        private void ApplyTag(DocumentEntity entity, Document doc, TddTag tag)
        {
            doc.ApplyTag(entity.TddJobSeparator, tag);
        }

        private bool CheckForGenderDiscrepancies(EditorTab rtf)
        {
            var doc = rtf.RichEdit.Document;

            var f_ranges = doc.FindFemininePronouns();
            var m_ranges = doc.FindMasculinePronouns();

            var existing = rtf.HasBanner("gender");

            // Either male or female is 0, so everything is good
            if (f_ranges.Count() == 0 || m_ranges.Count() == 0)
            {
                rtf.RemoveStatusBanner("gender");
                return false;
            }

            // If we've already shown the banner and the counts haven't changed,
            // then the editor must be satisfied with the discrepancy
            if (existing && female_words == f_ranges.Count() && male_words == m_ranges.Count())
                return false;

            // This is either the first time we've shown the banner, or we showed it 
            // and they fixed some of them, show it again in case they thought they
            // fixed all of them but missed some
            rtf.RemoveStatusBanner("direction");
            rtf.AddStatusBanner("gender", string.Format("Double-check gender pronouns ({0} masculine, {1} feminine).", m_ranges.Count(), f_ranges.Count()));

            // Color all the words!
            doc.BeginUpdate();
            doc.ColorWords(f_ranges, Color.Pink);
            doc.ColorWords(m_ranges, Color.LightBlue);
            doc.EndUpdate();

            female_words = f_ranges.Count();
            male_words = m_ranges.Count();

            return true;
        }

        private bool CheckForDirectionalDiscrepancies(EditorTab rtf)
        {
            var doc = rtf.RichEdit.Document;

            var l_ranges = doc.FindLeftDirectionWords();
            var r_ranges = doc.FindRightDirectionWords();

            var existing = rtf.HasBanner("direction");

            // Either left or right is 0, so everything is good
            if (l_ranges.Count() == 0 || r_ranges.Count() == 0)
            {
                rtf.RemoveStatusBanner("direction");
                return false;
            }

            // If we've already shown the banner and the counts haven't changed,
            // then the editor must be satisfied with the discrepancy
            if (existing && left_words == l_ranges.Count() && right_words == r_ranges.Count())
                return false;

            // This is either the first time we've shown the banner, or we showed it 
            // and they fixed some of them, show it again in case they thought they
            // fixed all of them but missed some
            rtf.RemoveStatusBanner("direction");
            rtf.AddStatusBanner("direction", string.Format("Double-check directional words ({0} left, {1} right).", l_ranges.Count(), r_ranges.Count()));

            // Color all the words!
            doc.BeginUpdate();
            doc.ColorWords(l_ranges, Color.LightGreen);
            doc.ColorWords(r_ranges, Color.Yellow);
            doc.EndUpdate();

            left_words = l_ranges.Count();
            right_words = r_ranges.Count();

            return true;
        }
        #endregion

        #region Foot Pedal
        private void InitializeFootPedal()
        {
            try
            {
                foot_pedal = new SpeechMikeControl();
                foot_pedal.Initialize(false);
                foot_pedal.Activate();
                foot_pedal.SPMEventButton += FootPedal_PedalPressed;
            }
            catch (Exception ex)
            {
                foot_pedal = null;
                EditorCore.LogException("Error initializing foot pedal!!", ex);
            }
        }

        private void FootPedal_PedalPressed(int lDeviceID, spmControlDeviceEvent EventId)
        {
            switch (EventId)
            {
                case spmControlDeviceEvent.spmFastForwardPressed:
                    DoFastForward();
                    break;
                case spmControlDeviceEvent.spmFastForwardReleased:
                    DoStop(BackStepAction.None);
                    break;
                case spmControlDeviceEvent.spmFastRewindPressed:
                    DoRewind();
                    break;
                case spmControlDeviceEvent.spmFastRewindReleased:
                    DoStop(BackStepAction.None);
                    break;
                case spmControlDeviceEvent.spmPlayPressed:
                    DoPlay();
                    break;
                case spmControlDeviceEvent.spmPlayReleased:
                    DoStop(BackStepAction.FootPedal);
                    break;
            }
        }
        #endregion

        #region Audio Actions
        private void DoPlay()
        {
            rewinding = false;
            fastforwarding = false;
            tmrRewindForward.Stop();

            audioDjStudio1.Play();
            tmrAudioPosition.Start();
            UpdateAudioControls();
        }

        private void DoStop(BackStepAction back)
        {
            rewinding = false;
            fastforwarding = false;
            tmrRewindForward.Stop();

            audioDjStudio1.Pause();

            float bounce_back = 0f;

            if (back == BackStepAction.Keyboard)
                bounce_back = EditorCore.Settings.KeyboardBounceBack * 1000;
            else if (back == BackStepAction.FootPedal)
                bounce_back = EditorCore.Settings.FootPedalBounceBack * 1000;

            try
            {
                if (bounce_back > 0)
                {
                    var cur_pos = audioDjStudio1.GetSoundPosition();
                    var new_pos = cur_pos - bounce_back;

                    if (new_pos < 0)
                        new_pos = 0;

                    audioDjStudio1.Seek((uint)new_pos);
                }
            }
            catch (InvalidOperationException)
            {
                // Somehow GetSoundPosition is throwing an ERR_SOUND_NOT_LOADED,
                // despite the fact that the pause up above succeeded.
            }

            UpdateAudioControls();
        }

        private void DoFastForward()
        {
            audioDjStudio1.Pause();

            rewinding = false;
            fastforwarding = true;
            UpdateAudioControls();

            tmrRewindForward.Start();
        }

        private void DoRewind()
        {
            audioDjStudio1.Pause();

            rewinding = true;
            fastforwarding = false;
            UpdateAudioControls();

            tmrRewindForward.Start();
        }

        private void Jobs_CloseSoundRequested(object sender, CloseSoundEventArgs e)
        {
            if (Path.GetFileName(audioDjStudio1.GetLoadedSoundFile(0)) == Path.GetFileName(e.SoundFile))
                DoCloseSound();
        }

        private void DoCloseSound()
        {
            rewinding = false;
            fastforwarding = false;
            tmrRewindForward.Stop();

            audioDjStudio1.Pause();
            audioDjStudio1.CloseSound();
        }
        #endregion

        #region Timers
        private async void tmrDownloadQueue_Tick(object sender, EventArgs e)
        {
            tmrDownloadQueue.Stop();
            tmrDownloadQueue.Interval = EditorCore.Settings.DefaultDownloadTimerDuration;

            var out_of_jobs = await EditorCore.Jobs.BackgroundPulse();

            if (out_of_jobs)
                tmrDownloadQueue.Interval = 60000;

            tmrDownloadQueue.Start();
        }

        private void tmrRewindForward_Tick(object sender, EventArgs e)
        {
            // This is probably hiding a bug elsewhere, but it'll
            // be extremely hard to cover every case correctly.
            if (!audioDjStudio1.IsSoundLoaded())
                return;

            if (rewinding)
            {
                if (audioDjStudio1.GetSoundPosition() - EditorCore.Settings.FastForwardRewindSpeed < 0)
                {
                    rewinding = false;
                    UpdateAudioControls();
                    tmrRewindForward.Stop();
                    return;
                }

                audioDjStudio1.Rewind();
            }
            else if (fastforwarding)
            {
                if (audioDjStudio1.GetSoundPosition() + EditorCore.Settings.FastForwardRewindSpeed > audioDjStudio1.GetSoundDuration())
                {
                    fastforwarding = false;
                    UpdateAudioControls();
                    tmrRewindForward.Stop();
                    return;
                }

                audioDjStudio1.FastForward();
            }
        }
        #endregion


        private void btnSendtoCR_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dr = MessageBox.Show("Are you sure you want send this job to Client Review?", "Confirm Document Client Review", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
                return;

            SendQAJob(Entities.NextQAStatus.ReturnJobToCR);
        }

        private void barCreateNote_ItemClick(object sender, ItemClickEventArgs e)
        {
            var panel = this.GetActiveRichEditPanel();
            //this.do
            if (panel == null)
                return;

            var rich_edit = panel.RichEdit;
            var document = panel.Document;


            using (var dialog = new QANote(document))
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    dialog.Controls["memoeditNote"].Text = document.QANote;
                    // dialog.Controls["memoeditNote"].Text
                }

            rich_edit.Focus();
        }

        private void btnSameQA_ItemClick(object sender, ItemClickEventArgs e)
        {
            SendQAJob(Entities.NextQAStatus.ReturnJobToSameQA);
        }

        private void btnNextQA_ItemClick(object sender, ItemClickEventArgs e)
        {
            SendQAJob(Entities.NextQAStatus.ReturnJobToNextQA);
        }

        private void btnEntradaQA_ItemClick(object sender, ItemClickEventArgs e)
        {
            SendQAJob(Entities.NextQAStatus.ReturnJobToEntradaQA);
        }
        // This Method is used to Send Jobs QA 
        private void SendQAJob(Entities.NextQAStatus NextStage)
        {            
            var rtf = this.GetActiveRichEditPanel();

            if (rtf == null)
                return;
            var doc = EditorCore.Documents.ActiveDocument;
            doc.Job.QAData.NextQAStatusFlags = NextStage;
            
            if (doc.Job.QAData.CanReturnQACategory)
            {
                if (barCategory.EditValue.ToString() == "-1")
                {
                    MessageBox.Show("Please enter a Valid QA Category!", "Entrada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (barSubCategory.EditValue.ToString() == "-1")
                {
                    MessageBox.Show("Please enter a Valid QA SubCategory!", "Entrada", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                doc.Job.QAData.Category.Id = Convert.ToInt32(barSubCategory.EditValue);
                doc.Job.QAData.Category.ParentId = Convert.ToInt32(barCategory.EditValue);
            }

            if (doc.QANote.Trim() != "")
            {
                doc.Job.QAData.LastQANote = doc.QANote;
                doc.QANote = "";
            }      

            SaveAndSendDocument(rtf, doc);
        }
    }
}
