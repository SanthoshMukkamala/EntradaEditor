namespace Entrada.Editor
{
	partial class EditorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::Entrada.Editor.SplashDialog), false, false);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel1 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.XtraEditors.Repository.TrackBarLabel trackBarLabel2 = new DevExpress.XtraEditors.Repository.TrackBarLabel();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup1 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraSpellChecker.OptionsSpelling optionsSpelling1 = new DevExpress.XtraSpellChecker.OptionsSpelling();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.smallImages = new DevExpress.Utils.ImageCollection(this.components);
            this.trkAudioPosition = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
            this.lblAudioPosition = new DevExpress.XtraBars.BarStaticItem();
            this.btnStartEditing = new DevExpress.XtraBars.BarButtonItem();
            this.btnStopEditing = new DevExpress.XtraBars.BarButtonItem();
            this.pasteItem1 = new DevExpress.XtraRichEdit.UI.PasteItem();
            this.cutItem1 = new DevExpress.XtraRichEdit.UI.CutItem();
            this.copyItem1 = new DevExpress.XtraRichEdit.UI.CopyItem();
            this.pasteSpecialItem1 = new DevExpress.XtraRichEdit.UI.PasteSpecialItem();
            this.barButtonGroup2 = new DevExpress.XtraBars.BarButtonGroup();
            this.changeFontNameItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontNameItem();
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.changeFontSizeItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontSizeItem();
            this.repositoryItemRichEditFontSizeEdit1 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit();
            this.fontSizeIncreaseItem1 = new DevExpress.XtraRichEdit.UI.FontSizeIncreaseItem();
            this.fontSizeDecreaseItem1 = new DevExpress.XtraRichEdit.UI.FontSizeDecreaseItem();
            this.barButtonGroup3 = new DevExpress.XtraBars.BarButtonGroup();
            this.toggleFontBoldItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontBoldItem();
            this.toggleFontItalicItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontItalicItem();
            this.toggleFontUnderlineItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontUnderlineItem();
            this.toggleFontDoubleUnderlineItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontDoubleUnderlineItem();
            this.toggleFontStrikeoutItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontStrikeoutItem();
            this.toggleFontDoubleStrikeoutItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontDoubleStrikeoutItem();
            this.toggleFontSuperscriptItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontSuperscriptItem();
            this.toggleFontSubscriptItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontSubscriptItem();
            this.barButtonGroup4 = new DevExpress.XtraBars.BarButtonGroup();
            this.changeFontColorItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontColorItem();
            this.changeFontBackColorItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontBackColorItem();
            this.changeTextCaseItem1 = new DevExpress.XtraRichEdit.UI.ChangeTextCaseItem();
            this.makeTextUpperCaseItem1 = new DevExpress.XtraRichEdit.UI.MakeTextUpperCaseItem();
            this.makeTextLowerCaseItem1 = new DevExpress.XtraRichEdit.UI.MakeTextLowerCaseItem();
            this.toggleTextCaseItem1 = new DevExpress.XtraRichEdit.UI.ToggleTextCaseItem();
            this.clearFormattingItem1 = new DevExpress.XtraRichEdit.UI.ClearFormattingItem();
            this.barButtonGroup5 = new DevExpress.XtraBars.BarButtonGroup();
            this.toggleBulletedListItem1 = new DevExpress.XtraRichEdit.UI.ToggleBulletedListItem();
            this.toggleNumberingListItem1 = new DevExpress.XtraRichEdit.UI.ToggleNumberingListItem();
            this.toggleMultiLevelListItem1 = new DevExpress.XtraRichEdit.UI.ToggleMultiLevelListItem();
            this.barButtonGroup6 = new DevExpress.XtraBars.BarButtonGroup();
            this.decreaseIndentItem1 = new DevExpress.XtraRichEdit.UI.DecreaseIndentItem();
            this.increaseIndentItem1 = new DevExpress.XtraRichEdit.UI.IncreaseIndentItem();
            this.toggleShowWhitespaceItem1 = new DevExpress.XtraRichEdit.UI.ToggleShowWhitespaceItem();
            this.barButtonGroup7 = new DevExpress.XtraBars.BarButtonGroup();
            this.toggleParagraphAlignmentLeftItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentLeftItem();
            this.toggleParagraphAlignmentCenterItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentCenterItem();
            this.toggleParagraphAlignmentRightItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentRightItem();
            this.toggleParagraphAlignmentJustifyItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentJustifyItem();
            this.barButtonGroup8 = new DevExpress.XtraBars.BarButtonGroup();
            this.changeParagraphLineSpacingItem1 = new DevExpress.XtraRichEdit.UI.ChangeParagraphLineSpacingItem();
            this.setSingleParagraphSpacingItem1 = new DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem();
            this.setSesquialteralParagraphSpacingItem1 = new DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem();
            this.setDoubleParagraphSpacingItem1 = new DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem();
            this.showLineSpacingFormItem1 = new DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem();
            this.addSpacingBeforeParagraphItem1 = new DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem();
            this.removeSpacingBeforeParagraphItem1 = new DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem();
            this.addSpacingAfterParagraphItem1 = new DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem();
            this.removeSpacingAfterParagraphItem1 = new DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem();
            this.changeParagraphBackColorItem1 = new DevExpress.XtraRichEdit.UI.ChangeParagraphBackColorItem();
            this.galleryChangeStyleItem1 = new DevExpress.XtraRichEdit.UI.GalleryChangeStyleItem();
            this.findItem1 = new DevExpress.XtraRichEdit.UI.FindItem();
            this.replaceItem1 = new DevExpress.XtraRichEdit.UI.ReplaceItem();
            this.btnRewindAudio = new DevExpress.XtraBars.BarButtonItem();
            this.btnPlayAudio = new DevExpress.XtraBars.BarButtonItem();
            this.btnStopAudio = new DevExpress.XtraBars.BarButtonItem();
            this.btnFastForwardAudio = new DevExpress.XtraBars.BarButtonItem();
            this.btnIncreaseSpeed = new DevExpress.XtraBars.BarButtonItem();
            this.btnDecreaseSpeed = new DevExpress.XtraBars.BarButtonItem();
            this.btnFinishDocument = new DevExpress.XtraBars.BarButtonItem();
            this.btnSendToQA = new DevExpress.XtraBars.BarButtonItem();
            this.btnSplitJobType = new DevExpress.XtraBars.BarButtonItem();
            this.btnSplitPatient = new DevExpress.XtraBars.BarButtonItem();
            this.btnPatients = new DevExpress.XtraBars.BarButtonItem();
            this.btnReferring = new DevExpress.XtraBars.BarButtonItem();
            this.btnInsertMacro = new DevExpress.XtraBars.BarButtonItem();
            this.btnManualDownload = new DevExpress.XtraBars.BarButtonItem();
            this.barZoomSlider = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemZoomTrackBar2 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.statusBarLabel = new DevExpress.XtraBars.BarStaticItem();
            this.btnPreferences = new DevExpress.XtraBars.BarButtonItem();
            this.btnAutoSentence = new DevExpress.XtraBars.BarButtonItem();
            this.btnNextAnomaly = new DevExpress.XtraBars.BarButtonItem();
            this.btnPreviousAnomaly = new DevExpress.XtraBars.BarButtonItem();
            this.btnResetLayout = new DevExpress.XtraBars.BarButtonItem();
            this.barJobTemplate = new DevExpress.XtraBars.BarSubItem();
            this.btnLogFolder = new DevExpress.XtraBars.BarButtonItem();
            this.btnAutoCorrect = new DevExpress.XtraBars.BarButtonItem();
            this.btnReleaseJob = new DevExpress.XtraBars.BarButtonItem();
            this.btnLaunchPortal = new DevExpress.XtraBars.BarStaticItem();
            this.btnWorkSummary = new DevExpress.XtraBars.BarButtonItem();
            this.btnCC = new DevExpress.XtraBars.BarButtonItem();
            this.btnSendLogs = new DevExpress.XtraBars.BarButtonItem();
            this.btnSpellCheck = new DevExpress.XtraBars.BarButtonItem();
            this.btnMakeUpperCase = new DevExpress.XtraBars.BarButtonItem();
            this.btnChangePassword = new DevExpress.XtraBars.BarButtonItem();
            this.btnLaunchGuide = new DevExpress.XtraBars.BarStaticItem();
            this.btnSameQA = new DevExpress.XtraBars.BarButtonItem();
            this.btnNextQA = new DevExpress.XtraBars.BarButtonItem();
            this.btnEntradaQA = new DevExpress.XtraBars.BarButtonItem();
            this.btnSendtoCR = new DevExpress.XtraBars.BarButtonItem();
            this.barCategory = new DevExpress.XtraBars.BarEditItem();
            this.rptLookUpEditCategory = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barSubCategory = new DevExpress.XtraBars.BarEditItem();
            this.rptLookUpEditSubCategory = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barCreateNote = new DevExpress.XtraBars.BarButtonItem();
            this.largeImages = new DevExpress.Utils.ImageCollection(this.components);
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grpDownloadQueue = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grpDocuments = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grpClientData = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grpQA = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.grpAudio = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.homeRibbonPage1 = new DevExpress.XtraRichEdit.UI.HomeRibbonPage();
            this.clipboardRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.ClipboardRibbonPageGroup();
            this.fontRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.FontRibbonPageGroup();
            this.paragraphRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.ParagraphRibbonPageGroup();
            this.editingRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.EditingRibbonPageGroup();
            this.grpQuickFix = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.grpAdvancedDocuments = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemZoomTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBox3 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemComboBox4 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemComboBox5 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemComboBox6 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.panelContainer2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelContainer3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel4 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel4_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.lstDownloadedJobs = new Entrada.Editor.DownloadedJobsListBox();
            this.dckTags = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.lstTags = new DevExpress.XtraEditors.ListBoxControl();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.grdDemographics = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.pnlQANotes = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel5_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.txtQANote = new DevExpress.XtraEditors.MemoEdit();
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.richEditBarController1 = new DevExpress.XtraRichEdit.UI.RichEditBarController();
            this.audioDjStudio1 = new AudioDjStudio.AudioDjStudio();
            this.tmrAudioPosition = new System.Windows.Forms.Timer(this.components);
            this.spellChecker1 = new DevExpress.XtraSpellChecker.SpellChecker(this.components);
            this.tmrDownloadQueue = new System.Windows.Forms.Timer(this.components);
            this.tmrRewindForward = new System.Windows.Forms.Timer(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smallImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptLookUpEditCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptLookUpEditSubCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.largeImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox6)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.panelContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel3.SuspendLayout();
            this.panelContainer3.SuspendLayout();
            this.dockPanel4.SuspendLayout();
            this.dockPanel4_Container.SuspendLayout();
            this.dckTags.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstTags)).BeginInit();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDemographics)).BeginInit();
            this.pnlQANotes.SuspendLayout();
            this.dockPanel5_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQANote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.richEditBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationIcon = global::Entrada.Editor.Properties.Resources.information;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Images = this.smallImages;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.trkAudioPosition,
            this.barButtonGroup1,
            this.lblAudioPosition,
            this.btnStartEditing,
            this.btnStopEditing,
            this.pasteItem1,
            this.cutItem1,
            this.copyItem1,
            this.pasteSpecialItem1,
            this.barButtonGroup2,
            this.changeFontNameItem1,
            this.changeFontSizeItem1,
            this.fontSizeIncreaseItem1,
            this.fontSizeDecreaseItem1,
            this.barButtonGroup3,
            this.toggleFontBoldItem1,
            this.toggleFontItalicItem1,
            this.toggleFontUnderlineItem1,
            this.toggleFontDoubleUnderlineItem1,
            this.toggleFontStrikeoutItem1,
            this.toggleFontDoubleStrikeoutItem1,
            this.toggleFontSuperscriptItem1,
            this.toggleFontSubscriptItem1,
            this.barButtonGroup4,
            this.changeFontColorItem1,
            this.changeFontBackColorItem1,
            this.changeTextCaseItem1,
            this.makeTextUpperCaseItem1,
            this.makeTextLowerCaseItem1,
            this.toggleTextCaseItem1,
            this.clearFormattingItem1,
            this.barButtonGroup5,
            this.toggleBulletedListItem1,
            this.toggleNumberingListItem1,
            this.toggleMultiLevelListItem1,
            this.barButtonGroup6,
            this.decreaseIndentItem1,
            this.increaseIndentItem1,
            this.barButtonGroup7,
            this.toggleParagraphAlignmentLeftItem1,
            this.toggleParagraphAlignmentCenterItem1,
            this.toggleParagraphAlignmentRightItem1,
            this.toggleParagraphAlignmentJustifyItem1,
            this.toggleShowWhitespaceItem1,
            this.barButtonGroup8,
            this.changeParagraphLineSpacingItem1,
            this.setSingleParagraphSpacingItem1,
            this.setSesquialteralParagraphSpacingItem1,
            this.setDoubleParagraphSpacingItem1,
            this.showLineSpacingFormItem1,
            this.addSpacingBeforeParagraphItem1,
            this.removeSpacingBeforeParagraphItem1,
            this.addSpacingAfterParagraphItem1,
            this.removeSpacingAfterParagraphItem1,
            this.changeParagraphBackColorItem1,
            this.galleryChangeStyleItem1,
            this.findItem1,
            this.replaceItem1,
            this.btnRewindAudio,
            this.btnPlayAudio,
            this.btnStopAudio,
            this.btnFastForwardAudio,
            this.btnIncreaseSpeed,
            this.btnDecreaseSpeed,
            this.btnFinishDocument,
            this.btnSendToQA,
            this.btnSplitJobType,
            this.btnSplitPatient,
            this.btnPatients,
            this.btnReferring,
            this.btnInsertMacro,
            this.btnManualDownload,
            this.barZoomSlider,
            this.statusBarLabel,
            this.btnPreferences,
            this.btnAutoSentence,
            this.btnNextAnomaly,
            this.btnPreviousAnomaly,
            this.btnResetLayout,
            this.barJobTemplate,
            this.btnLogFolder,
            this.btnAutoCorrect,
            this.btnReleaseJob,
            this.btnLaunchPortal,
            this.btnWorkSummary,
            this.btnCC,
            this.btnSendLogs,
            this.btnSpellCheck,
            this.btnMakeUpperCase,
            this.btnChangePassword,
            this.btnLaunchGuide,
            this.btnSameQA,
            this.btnNextQA,
            this.btnEntradaQA,
            this.btnSendtoCR,
            this.barCategory,
            this.barSubCategory,
            this.barCreateNote});
            this.ribbonControl1.LargeImages = this.largeImages;
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 140;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.PageHeaderItemLinks.Add(this.btnLaunchGuide);
            this.ribbonControl1.PageHeaderItemLinks.Add(this.btnLaunchPortal);
            this.ribbonControl1.PageHeaderItemLinks.Add(this.ribbonControl1.ExpandCollapseItem);
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1,
            this.homeRibbonPage1,
            this.ribbonPage2});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTrackBar1,
            this.repositoryItemZoomTrackBar1,
            this.repositoryItemFontEdit1,
            this.repositoryItemRichEditFontSizeEdit1,
            this.repositoryItemZoomTrackBar2,
            this.repositoryItemComboBox1,
            this.repositoryItemComboBox2,
            this.repositoryItemComboBox3,
            this.repositoryItemImageComboBox1,
            this.repositoryItemComboBox4,
            this.repositoryItemLookUpEdit1,
            this.repositoryItemComboBox5,
            this.repositoryItemLookUpEdit2,
            this.repositoryItemComboBox6,
            this.rptLookUpEditCategory,
            this.rptLookUpEditSubCategory});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowFullScreenButton = DevExpress.Utils.DefaultBoolean.True;
            this.ribbonControl1.Size = new System.Drawing.Size(1282, 144);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // smallImages
            // 
            this.smallImages.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("smallImages.ImageStream")));
            this.smallImages.Images.SetKeyName(0, "bullet_black.png");
            this.smallImages.Images.SetKeyName(1, "flag_red.png");
            this.smallImages.Images.SetKeyName(2, "document_comments.png");
            this.smallImages.Images.SetKeyName(3, "global_telecom.png");
            this.smallImages.Images.SetKeyName(4, "text_uppercase.png");
            this.smallImages.Images.SetKeyName(5, "spellcheck.png");
            this.smallImages.Images.SetKeyName(6, "report.png");
            // 
            // trkAudioPosition
            // 
            this.trkAudioPosition.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.trkAudioPosition.Caption = "Duration:";
            this.trkAudioPosition.Edit = this.repositoryItemTrackBar1;
            this.trkAudioPosition.Id = 9;
            this.trkAudioPosition.Name = "trkAudioPosition";
            this.trkAudioPosition.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.trkAudioPosition.Width = 150;
            // 
            // repositoryItemTrackBar1
            // 
            this.repositoryItemTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemTrackBar1.HighlightSelectedRange = false;
            this.repositoryItemTrackBar1.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemTrackBar1.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            trackBarLabel1.Label = "00:00:00";
            trackBarLabel2.Label = "00:30:00";
            this.repositoryItemTrackBar1.Labels.AddRange(new DevExpress.XtraEditors.Repository.TrackBarLabel[] {
            trackBarLabel1,
            trackBarLabel2});
            this.repositoryItemTrackBar1.Maximum = 30;
            this.repositoryItemTrackBar1.Name = "repositoryItemTrackBar1";
            this.repositoryItemTrackBar1.TickFrequency = 5;
            // 
            // barButtonGroup1
            // 
            this.barButtonGroup1.Caption = "barButtonGroup1";
            this.barButtonGroup1.Id = 11;
            this.barButtonGroup1.Name = "barButtonGroup1";
            // 
            // lblAudioPosition
            // 
            this.lblAudioPosition.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.lblAudioPosition.Caption = "00:00:00 / 00:00:00";
            this.lblAudioPosition.Id = 12;
            this.lblAudioPosition.Name = "lblAudioPosition";
            this.lblAudioPosition.TextAlignment = System.Drawing.StringAlignment.Near;
            this.lblAudioPosition.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btnStartEditing
            // 
            this.btnStartEditing.Caption = "Start Edit Queue";
            this.btnStartEditing.Enabled = false;
            this.btnStartEditing.Hint = "Starts downloading jobs to be edited.";
            this.btnStartEditing.Id = 13;
            this.btnStartEditing.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.U));
            this.btnStartEditing.LargeImageIndex = 0;
            this.btnStartEditing.Name = "btnStartEditing";
            this.btnStartEditing.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStartEditing_ItemClick);
            // 
            // btnStopEditing
            // 
            this.btnStopEditing.Caption = "Stop Edit Queue";
            this.btnStopEditing.Enabled = false;
            this.btnStopEditing.Id = 14;
            this.btnStopEditing.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Alt) 
                | System.Windows.Forms.Keys.U));
            this.btnStopEditing.LargeImageIndex = 1;
            this.btnStopEditing.Name = "btnStopEditing";
            toolTipTitleItem1.Text = "Stop Editing (Shift+Alt+U)";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Stops downloading new jobs to edit.\r\n\r\nWill not release downloaded but unedited j" +
    "obs.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.btnStopEditing.SuperTip = superToolTip1;
            this.btnStopEditing.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStopEditing_ItemClick);
            // 
            // pasteItem1
            // 
            this.pasteItem1.Caption = "Paste";
            this.pasteItem1.Enabled = false;
            this.pasteItem1.Id = 22;
            this.pasteItem1.Name = "pasteItem1";
            // 
            // cutItem1
            // 
            this.cutItem1.Caption = "Cut";
            this.cutItem1.Enabled = false;
            this.cutItem1.Id = 23;
            this.cutItem1.Name = "cutItem1";
            // 
            // copyItem1
            // 
            this.copyItem1.Caption = "Copy";
            this.copyItem1.Enabled = false;
            this.copyItem1.Id = 24;
            this.copyItem1.Name = "copyItem1";
            // 
            // pasteSpecialItem1
            // 
            this.pasteSpecialItem1.Caption = "Paste Special";
            this.pasteSpecialItem1.Enabled = false;
            this.pasteSpecialItem1.Id = 25;
            this.pasteSpecialItem1.Name = "pasteSpecialItem1";
            // 
            // barButtonGroup2
            // 
            this.barButtonGroup2.Id = 15;
            this.barButtonGroup2.ItemLinks.Add(this.changeFontNameItem1);
            this.barButtonGroup2.Name = "barButtonGroup2";
            this.barButtonGroup2.Tag = "{97BBE334-159B-44d9-A168-0411957565E8}";
            // 
            // changeFontNameItem1
            // 
            this.changeFontNameItem1.Edit = this.repositoryItemFontEdit1;
            this.changeFontNameItem1.Enabled = false;
            this.changeFontNameItem1.Id = 26;
            this.changeFontNameItem1.Name = "changeFontNameItem1";
            // 
            // repositoryItemFontEdit1
            // 
            this.repositoryItemFontEdit1.AutoHeight = false;
            this.repositoryItemFontEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit1.Name = "repositoryItemFontEdit1";
            // 
            // changeFontSizeItem1
            // 
            this.changeFontSizeItem1.Edit = this.repositoryItemRichEditFontSizeEdit1;
            this.changeFontSizeItem1.Enabled = false;
            this.changeFontSizeItem1.Id = 27;
            this.changeFontSizeItem1.Name = "changeFontSizeItem1";
            // 
            // repositoryItemRichEditFontSizeEdit1
            // 
            this.repositoryItemRichEditFontSizeEdit1.AutoHeight = false;
            this.repositoryItemRichEditFontSizeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditFontSizeEdit1.Control = null;
            this.repositoryItemRichEditFontSizeEdit1.Name = "repositoryItemRichEditFontSizeEdit1";
            // 
            // fontSizeIncreaseItem1
            // 
            this.fontSizeIncreaseItem1.Caption = "Grow Font";
            this.fontSizeIncreaseItem1.Enabled = false;
            this.fontSizeIncreaseItem1.Id = 28;
            this.fontSizeIncreaseItem1.Name = "fontSizeIncreaseItem1";
            // 
            // fontSizeDecreaseItem1
            // 
            this.fontSizeDecreaseItem1.Caption = "Shrink Font";
            this.fontSizeDecreaseItem1.Enabled = false;
            this.fontSizeDecreaseItem1.Id = 29;
            this.fontSizeDecreaseItem1.Name = "fontSizeDecreaseItem1";
            // 
            // barButtonGroup3
            // 
            this.barButtonGroup3.Id = 16;
            this.barButtonGroup3.ItemLinks.Add(this.toggleFontBoldItem1);
            this.barButtonGroup3.ItemLinks.Add(this.toggleFontItalicItem1);
            this.barButtonGroup3.ItemLinks.Add(this.changeFontSizeItem1);
            this.barButtonGroup3.ItemLinks.Add(this.fontSizeIncreaseItem1);
            this.barButtonGroup3.ItemLinks.Add(this.fontSizeDecreaseItem1);
            this.barButtonGroup3.Name = "barButtonGroup3";
            this.barButtonGroup3.Tag = "{433DA7F0-03E2-4650-9DB5-66DD92D16E39}";
            // 
            // toggleFontBoldItem1
            // 
            this.toggleFontBoldItem1.Caption = "Bold";
            this.toggleFontBoldItem1.Enabled = false;
            this.toggleFontBoldItem1.Id = 30;
            this.toggleFontBoldItem1.Name = "toggleFontBoldItem1";
            // 
            // toggleFontItalicItem1
            // 
            this.toggleFontItalicItem1.Caption = "Italic";
            this.toggleFontItalicItem1.Enabled = false;
            this.toggleFontItalicItem1.Id = 31;
            this.toggleFontItalicItem1.Name = "toggleFontItalicItem1";
            // 
            // toggleFontUnderlineItem1
            // 
            this.toggleFontUnderlineItem1.Caption = "Underline";
            this.toggleFontUnderlineItem1.Enabled = false;
            this.toggleFontUnderlineItem1.Id = 32;
            this.toggleFontUnderlineItem1.Name = "toggleFontUnderlineItem1";
            // 
            // toggleFontDoubleUnderlineItem1
            // 
            this.toggleFontDoubleUnderlineItem1.Caption = "Double Underline";
            this.toggleFontDoubleUnderlineItem1.Enabled = false;
            this.toggleFontDoubleUnderlineItem1.Id = 33;
            this.toggleFontDoubleUnderlineItem1.Name = "toggleFontDoubleUnderlineItem1";
            // 
            // toggleFontStrikeoutItem1
            // 
            this.toggleFontStrikeoutItem1.Caption = "Strikethrough";
            this.toggleFontStrikeoutItem1.Enabled = false;
            this.toggleFontStrikeoutItem1.Id = 34;
            this.toggleFontStrikeoutItem1.Name = "toggleFontStrikeoutItem1";
            // 
            // toggleFontDoubleStrikeoutItem1
            // 
            this.toggleFontDoubleStrikeoutItem1.Caption = "Double Strikethrough";
            this.toggleFontDoubleStrikeoutItem1.Enabled = false;
            this.toggleFontDoubleStrikeoutItem1.Id = 35;
            this.toggleFontDoubleStrikeoutItem1.Name = "toggleFontDoubleStrikeoutItem1";
            // 
            // toggleFontSuperscriptItem1
            // 
            this.toggleFontSuperscriptItem1.Caption = "Superscript";
            this.toggleFontSuperscriptItem1.Enabled = false;
            this.toggleFontSuperscriptItem1.Id = 36;
            this.toggleFontSuperscriptItem1.Name = "toggleFontSuperscriptItem1";
            // 
            // toggleFontSubscriptItem1
            // 
            this.toggleFontSubscriptItem1.Caption = "Subscript";
            this.toggleFontSubscriptItem1.Enabled = false;
            this.toggleFontSubscriptItem1.Id = 37;
            this.toggleFontSubscriptItem1.Name = "toggleFontSubscriptItem1";
            // 
            // barButtonGroup4
            // 
            this.barButtonGroup4.Id = 17;
            this.barButtonGroup4.Name = "barButtonGroup4";
            this.barButtonGroup4.Tag = "{DF8C5334-EDE3-47c9-A42C-FE9A9247E180}";
            // 
            // changeFontColorItem1
            // 
            this.changeFontColorItem1.Caption = "Font Color";
            this.changeFontColorItem1.Enabled = false;
            this.changeFontColorItem1.Id = 38;
            this.changeFontColorItem1.Name = "changeFontColorItem1";
            // 
            // changeFontBackColorItem1
            // 
            this.changeFontBackColorItem1.Caption = "Text Highlight Color";
            this.changeFontBackColorItem1.Enabled = false;
            this.changeFontBackColorItem1.Id = 39;
            this.changeFontBackColorItem1.Name = "changeFontBackColorItem1";
            // 
            // changeTextCaseItem1
            // 
            this.changeTextCaseItem1.Caption = "Change Case";
            this.changeTextCaseItem1.Enabled = false;
            this.changeTextCaseItem1.Id = 40;
            this.changeTextCaseItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.makeTextUpperCaseItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.makeTextLowerCaseItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTextCaseItem1)});
            this.changeTextCaseItem1.Name = "changeTextCaseItem1";
            // 
            // makeTextUpperCaseItem1
            // 
            this.makeTextUpperCaseItem1.Caption = "UPPERCASE";
            this.makeTextUpperCaseItem1.Enabled = false;
            this.makeTextUpperCaseItem1.Id = 41;
            this.makeTextUpperCaseItem1.Name = "makeTextUpperCaseItem1";
            // 
            // makeTextLowerCaseItem1
            // 
            this.makeTextLowerCaseItem1.Caption = "lowercase";
            this.makeTextLowerCaseItem1.Enabled = false;
            this.makeTextLowerCaseItem1.Id = 42;
            this.makeTextLowerCaseItem1.Name = "makeTextLowerCaseItem1";
            // 
            // toggleTextCaseItem1
            // 
            this.toggleTextCaseItem1.Caption = "tOGGLE cASE";
            this.toggleTextCaseItem1.Enabled = false;
            this.toggleTextCaseItem1.Id = 43;
            this.toggleTextCaseItem1.Name = "toggleTextCaseItem1";
            // 
            // clearFormattingItem1
            // 
            this.clearFormattingItem1.Caption = "Clear Formatting";
            this.clearFormattingItem1.Enabled = false;
            this.clearFormattingItem1.Id = 44;
            this.clearFormattingItem1.Name = "clearFormattingItem1";
            // 
            // barButtonGroup5
            // 
            this.barButtonGroup5.Id = 18;
            this.barButtonGroup5.ItemLinks.Add(this.toggleBulletedListItem1);
            this.barButtonGroup5.ItemLinks.Add(this.toggleNumberingListItem1);
            this.barButtonGroup5.ItemLinks.Add(this.toggleMultiLevelListItem1);
            this.barButtonGroup5.Name = "barButtonGroup5";
            this.barButtonGroup5.Tag = "{0B3A7A43-3079-4ce0-83A8-3789F5F6DC9F}";
            // 
            // toggleBulletedListItem1
            // 
            this.toggleBulletedListItem1.Caption = "Bullets";
            this.toggleBulletedListItem1.Enabled = false;
            this.toggleBulletedListItem1.Id = 45;
            this.toggleBulletedListItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.T));
            this.toggleBulletedListItem1.Name = "toggleBulletedListItem1";
            // 
            // toggleNumberingListItem1
            // 
            this.toggleNumberingListItem1.Caption = "Numbering";
            this.toggleNumberingListItem1.Enabled = false;
            this.toggleNumberingListItem1.Id = 46;
            this.toggleNumberingListItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T));
            this.toggleNumberingListItem1.Name = "toggleNumberingListItem1";
            // 
            // toggleMultiLevelListItem1
            // 
            this.toggleMultiLevelListItem1.Caption = "Multilevel list";
            this.toggleMultiLevelListItem1.Enabled = false;
            this.toggleMultiLevelListItem1.Id = 47;
            this.toggleMultiLevelListItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.Alt) 
                | System.Windows.Forms.Keys.T));
            this.toggleMultiLevelListItem1.Name = "toggleMultiLevelListItem1";
            // 
            // barButtonGroup6
            // 
            this.barButtonGroup6.Id = 19;
            this.barButtonGroup6.ItemLinks.Add(this.decreaseIndentItem1);
            this.barButtonGroup6.ItemLinks.Add(this.increaseIndentItem1);
            this.barButtonGroup6.ItemLinks.Add(this.toggleShowWhitespaceItem1);
            this.barButtonGroup6.Name = "barButtonGroup6";
            this.barButtonGroup6.Tag = "{4747D5AB-2BEB-4ea6-9A1D-8E4FB36F1B40}";
            // 
            // decreaseIndentItem1
            // 
            this.decreaseIndentItem1.Caption = "Decrease Indent";
            this.decreaseIndentItem1.Enabled = false;
            this.decreaseIndentItem1.Id = 48;
            this.decreaseIndentItem1.Name = "decreaseIndentItem1";
            // 
            // increaseIndentItem1
            // 
            this.increaseIndentItem1.Caption = "Increase Indent";
            this.increaseIndentItem1.Enabled = false;
            this.increaseIndentItem1.Id = 49;
            this.increaseIndentItem1.Name = "increaseIndentItem1";
            // 
            // toggleShowWhitespaceItem1
            // 
            this.toggleShowWhitespaceItem1.Caption = "Show/Hide ¶";
            this.toggleShowWhitespaceItem1.Enabled = false;
            this.toggleShowWhitespaceItem1.Id = 50;
            this.toggleShowWhitespaceItem1.Name = "toggleShowWhitespaceItem1";
            // 
            // barButtonGroup7
            // 
            this.barButtonGroup7.Id = 20;
            this.barButtonGroup7.ItemLinks.Add(this.toggleParagraphAlignmentLeftItem1);
            this.barButtonGroup7.ItemLinks.Add(this.toggleParagraphAlignmentCenterItem1);
            this.barButtonGroup7.ItemLinks.Add(this.toggleParagraphAlignmentRightItem1);
            this.barButtonGroup7.ItemLinks.Add(this.toggleParagraphAlignmentJustifyItem1);
            this.barButtonGroup7.Name = "barButtonGroup7";
            this.barButtonGroup7.Tag = "{8E89E775-996E-49a0-AADA-DE338E34732E}";
            // 
            // toggleParagraphAlignmentLeftItem1
            // 
            this.toggleParagraphAlignmentLeftItem1.Caption = "Align Text Left";
            this.toggleParagraphAlignmentLeftItem1.Enabled = false;
            this.toggleParagraphAlignmentLeftItem1.Id = 51;
            this.toggleParagraphAlignmentLeftItem1.Name = "toggleParagraphAlignmentLeftItem1";
            // 
            // toggleParagraphAlignmentCenterItem1
            // 
            this.toggleParagraphAlignmentCenterItem1.Caption = "Center";
            this.toggleParagraphAlignmentCenterItem1.Enabled = false;
            this.toggleParagraphAlignmentCenterItem1.Id = 52;
            this.toggleParagraphAlignmentCenterItem1.Name = "toggleParagraphAlignmentCenterItem1";
            // 
            // toggleParagraphAlignmentRightItem1
            // 
            this.toggleParagraphAlignmentRightItem1.Caption = "Align Text Right";
            this.toggleParagraphAlignmentRightItem1.Enabled = false;
            this.toggleParagraphAlignmentRightItem1.Id = 53;
            this.toggleParagraphAlignmentRightItem1.Name = "toggleParagraphAlignmentRightItem1";
            // 
            // toggleParagraphAlignmentJustifyItem1
            // 
            this.toggleParagraphAlignmentJustifyItem1.Caption = "Justify";
            this.toggleParagraphAlignmentJustifyItem1.Enabled = false;
            this.toggleParagraphAlignmentJustifyItem1.Id = 54;
            this.toggleParagraphAlignmentJustifyItem1.Name = "toggleParagraphAlignmentJustifyItem1";
            // 
            // barButtonGroup8
            // 
            this.barButtonGroup8.Id = 21;
            this.barButtonGroup8.ItemLinks.Add(this.changeParagraphLineSpacingItem1);
            this.barButtonGroup8.ItemLinks.Add(this.changeParagraphBackColorItem1);
            this.barButtonGroup8.Name = "barButtonGroup8";
            this.barButtonGroup8.Tag = "{9A8DEAD8-3890-4857-A395-EC625FD02217}";
            // 
            // changeParagraphLineSpacingItem1
            // 
            this.changeParagraphLineSpacingItem1.Caption = "Line Spacing";
            this.changeParagraphLineSpacingItem1.Enabled = false;
            this.changeParagraphLineSpacingItem1.Id = 55;
            this.changeParagraphLineSpacingItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.setSingleParagraphSpacingItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setSesquialteralParagraphSpacingItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setDoubleParagraphSpacingItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.showLineSpacingFormItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.addSpacingBeforeParagraphItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.removeSpacingBeforeParagraphItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.addSpacingAfterParagraphItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.removeSpacingAfterParagraphItem1)});
            this.changeParagraphLineSpacingItem1.Name = "changeParagraphLineSpacingItem1";
            // 
            // setSingleParagraphSpacingItem1
            // 
            this.setSingleParagraphSpacingItem1.Caption = "1.0";
            this.setSingleParagraphSpacingItem1.Enabled = false;
            this.setSingleParagraphSpacingItem1.Id = 56;
            this.setSingleParagraphSpacingItem1.Name = "setSingleParagraphSpacingItem1";
            // 
            // setSesquialteralParagraphSpacingItem1
            // 
            this.setSesquialteralParagraphSpacingItem1.Caption = "1.5";
            this.setSesquialteralParagraphSpacingItem1.Enabled = false;
            this.setSesquialteralParagraphSpacingItem1.Id = 57;
            this.setSesquialteralParagraphSpacingItem1.Name = "setSesquialteralParagraphSpacingItem1";
            // 
            // setDoubleParagraphSpacingItem1
            // 
            this.setDoubleParagraphSpacingItem1.Caption = "2.0";
            this.setDoubleParagraphSpacingItem1.Enabled = false;
            this.setDoubleParagraphSpacingItem1.Id = 58;
            this.setDoubleParagraphSpacingItem1.Name = "setDoubleParagraphSpacingItem1";
            // 
            // showLineSpacingFormItem1
            // 
            this.showLineSpacingFormItem1.Caption = "Line Spacing Options...";
            this.showLineSpacingFormItem1.Enabled = false;
            this.showLineSpacingFormItem1.Id = 59;
            this.showLineSpacingFormItem1.Name = "showLineSpacingFormItem1";
            // 
            // addSpacingBeforeParagraphItem1
            // 
            this.addSpacingBeforeParagraphItem1.Caption = "Add Space &Before Paragraph";
            this.addSpacingBeforeParagraphItem1.Enabled = false;
            this.addSpacingBeforeParagraphItem1.Id = 60;
            this.addSpacingBeforeParagraphItem1.Name = "addSpacingBeforeParagraphItem1";
            // 
            // removeSpacingBeforeParagraphItem1
            // 
            this.removeSpacingBeforeParagraphItem1.Caption = "Remove Space &Before Paragraph";
            this.removeSpacingBeforeParagraphItem1.Enabled = false;
            this.removeSpacingBeforeParagraphItem1.Id = 61;
            this.removeSpacingBeforeParagraphItem1.Name = "removeSpacingBeforeParagraphItem1";
            // 
            // addSpacingAfterParagraphItem1
            // 
            this.addSpacingAfterParagraphItem1.Caption = "Add Space &After Paragraph";
            this.addSpacingAfterParagraphItem1.Enabled = false;
            this.addSpacingAfterParagraphItem1.Id = 62;
            this.addSpacingAfterParagraphItem1.Name = "addSpacingAfterParagraphItem1";
            // 
            // removeSpacingAfterParagraphItem1
            // 
            this.removeSpacingAfterParagraphItem1.Caption = "Remove Space &After Paragraph";
            this.removeSpacingAfterParagraphItem1.Enabled = false;
            this.removeSpacingAfterParagraphItem1.Id = 63;
            this.removeSpacingAfterParagraphItem1.Name = "removeSpacingAfterParagraphItem1";
            // 
            // changeParagraphBackColorItem1
            // 
            this.changeParagraphBackColorItem1.Caption = "Shading";
            this.changeParagraphBackColorItem1.Enabled = false;
            this.changeParagraphBackColorItem1.Id = 64;
            this.changeParagraphBackColorItem1.Name = "changeParagraphBackColorItem1";
            // 
            // galleryChangeStyleItem1
            // 
            this.galleryChangeStyleItem1.Caption = "Quick Styles";
            this.galleryChangeStyleItem1.Enabled = false;
            // 
            // 
            // 
            this.galleryChangeStyleItem1.Gallery.ColumnCount = 10;
            this.galleryChangeStyleItem1.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup1});
            this.galleryChangeStyleItem1.Gallery.ImageSize = new System.Drawing.Size(65, 46);
            this.galleryChangeStyleItem1.Id = 65;
            this.galleryChangeStyleItem1.Name = "galleryChangeStyleItem1";
            // 
            // findItem1
            // 
            this.findItem1.Caption = "Find";
            this.findItem1.Enabled = false;
            this.findItem1.Id = 66;
            this.findItem1.Name = "findItem1";
            // 
            // replaceItem1
            // 
            this.replaceItem1.Caption = "Replace";
            this.replaceItem1.Enabled = false;
            this.replaceItem1.Id = 67;
            this.replaceItem1.Name = "replaceItem1";
            // 
            // btnRewindAudio
            // 
            this.btnRewindAudio.Caption = "Rewind";
            this.btnRewindAudio.Id = 68;
            this.btnRewindAudio.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9);
            this.btnRewindAudio.LargeImageIndex = 3;
            this.btnRewindAudio.Name = "btnRewindAudio";
            this.btnRewindAudio.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRewindAudio_ItemClick);
            // 
            // btnPlayAudio
            // 
            this.btnPlayAudio.Caption = "Play";
            this.btnPlayAudio.Id = 69;
            this.btnPlayAudio.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F10);
            this.btnPlayAudio.LargeImageIndex = 0;
            this.btnPlayAudio.Name = "btnPlayAudio";
            this.btnPlayAudio.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPlayAudio_ItemClick);
            // 
            // btnStopAudio
            // 
            this.btnStopAudio.Caption = "Stop";
            this.btnStopAudio.Id = 70;
            this.btnStopAudio.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11);
            this.btnStopAudio.LargeImageIndex = 1;
            this.btnStopAudio.Name = "btnStopAudio";
            this.btnStopAudio.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStopAudio_ItemClick);
            // 
            // btnFastForwardAudio
            // 
            this.btnFastForwardAudio.Caption = "Fast Forward";
            this.btnFastForwardAudio.Id = 71;
            this.btnFastForwardAudio.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12);
            this.btnFastForwardAudio.LargeImageIndex = 2;
            this.btnFastForwardAudio.Name = "btnFastForwardAudio";
            this.btnFastForwardAudio.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFastForwardAudio_ItemClick);
            // 
            // btnIncreaseSpeed
            // 
            this.btnIncreaseSpeed.Caption = "Increase Speed";
            this.btnIncreaseSpeed.Id = 75;
            this.btnIncreaseSpeed.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus));
            this.btnIncreaseSpeed.LargeImageIndex = 4;
            this.btnIncreaseSpeed.Name = "btnIncreaseSpeed";
            this.btnIncreaseSpeed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnIncreaseSpeed_ItemClick);
            // 
            // btnDecreaseSpeed
            // 
            this.btnDecreaseSpeed.Caption = "Decrease Speed";
            this.btnDecreaseSpeed.Id = 76;
            this.btnDecreaseSpeed.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus));
            this.btnDecreaseSpeed.LargeImageIndex = 5;
            this.btnDecreaseSpeed.Name = "btnDecreaseSpeed";
            this.btnDecreaseSpeed.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDecreaseSpeed_ItemClick);
            // 
            // btnFinishDocument
            // 
            this.btnFinishDocument.Caption = "Finish Document";
            this.btnFinishDocument.Id = 78;
            this.btnFinishDocument.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F1);
            this.btnFinishDocument.LargeImageIndex = 6;
            this.btnFinishDocument.Name = "btnFinishDocument";
            toolTipTitleItem2.Text = "Finish Document (F1)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Sends the completed document back to the client.\r\n\r\nWill automatically open next " +
    "job in downloaded queue.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.btnFinishDocument.SuperTip = superToolTip2;
            this.btnFinishDocument.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFinishDocument_ItemClick);
            // 
            // btnSendToQA
            // 
            this.btnSendToQA.Caption = "Send to QA";
            this.btnSendToQA.Id = 79;
            this.btnSendToQA.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2);
            this.btnSendToQA.LargeImageIndex = 7;
            this.btnSendToQA.Name = "btnSendToQA";
            toolTipTitleItem3.Text = "Send to QA (F2)";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Sends the document to QA for further review.\r\n\r\nWill automatically open next job " +
    "in downloaded queue.";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.btnSendToQA.SuperTip = superToolTip3;
            this.btnSendToQA.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSendToQA_ItemClick);
            // 
            // btnSplitJobType
            // 
            this.btnSplitJobType.Caption = "Split by JobType";
            this.btnSplitJobType.Hint = "Creates a new job from the selected text with a different job type.";
            this.btnSplitJobType.Id = 80;
            this.btnSplitJobType.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F3);
            this.btnSplitJobType.LargeImageIndex = 9;
            this.btnSplitJobType.Name = "btnSplitJobType";
            this.btnSplitJobType.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSplitJobType_ItemClick);
            // 
            // btnSplitPatient
            // 
            this.btnSplitPatient.Caption = "Split by Patient";
            this.btnSplitPatient.Hint = "Creates a new job from the selected text with a different patient.";
            this.btnSplitPatient.Id = 81;
            this.btnSplitPatient.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4);
            this.btnSplitPatient.LargeImageIndex = 8;
            this.btnSplitPatient.Name = "btnSplitPatient";
            this.btnSplitPatient.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSplitPatient_ItemClick);
            // 
            // btnPatients
            // 
            this.btnPatients.Caption = "Patients";
            this.btnPatients.Hint = "Search for a patient in the client\'s database.";
            this.btnPatients.Id = 82;
            this.btnPatients.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.btnPatients.LargeImageIndex = 10;
            this.btnPatients.Name = "btnPatients";
            this.btnPatients.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPatients_ItemClick);
            // 
            // btnReferring
            // 
            this.btnReferring.Caption = "Referring Physicians";
            this.btnReferring.Hint = "Search for a referring physician in the client\'s database.";
            this.btnReferring.Id = 83;
            this.btnReferring.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6);
            this.btnReferring.LargeImageIndex = 11;
            this.btnReferring.Name = "btnReferring";
            this.btnReferring.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReferring_ItemClick);
            // 
            // btnInsertMacro
            // 
            this.btnInsertMacro.Caption = "Insert Macro";
            this.btnInsertMacro.Hint = "Inserts a dictator defined macro into the document.";
            this.btnInsertMacro.Id = 84;
            this.btnInsertMacro.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7);
            this.btnInsertMacro.LargeImageIndex = 12;
            this.btnInsertMacro.Name = "btnInsertMacro";
            this.btnInsertMacro.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInsertMacro_ItemClick);
            // 
            // btnManualDownload
            // 
            this.btnManualDownload.Caption = "Find Available Jobs";
            this.btnManualDownload.Id = 85;
            this.btnManualDownload.LargeImageIndex = 13;
            this.btnManualDownload.Name = "btnManualDownload";
            this.btnManualDownload.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnManualDownload_ItemClick);
            // 
            // barZoomSlider
            // 
            this.barZoomSlider.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barZoomSlider.Caption = "Zoom: ";
            this.barZoomSlider.Edit = this.repositoryItemZoomTrackBar2;
            this.barZoomSlider.EditValue = "100";
            this.barZoomSlider.Id = 87;
            this.barZoomSlider.Name = "barZoomSlider";
            this.barZoomSlider.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barZoomSlider.Width = 150;
            // 
            // repositoryItemZoomTrackBar2
            // 
            this.repositoryItemZoomTrackBar2.LargeChange = 25;
            this.repositoryItemZoomTrackBar2.Maximum = 200;
            this.repositoryItemZoomTrackBar2.Middle = 5;
            this.repositoryItemZoomTrackBar2.Minimum = 50;
            this.repositoryItemZoomTrackBar2.Name = "repositoryItemZoomTrackBar2";
            this.repositoryItemZoomTrackBar2.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.repositoryItemZoomTrackBar2.SmallChange = 10;
            // 
            // statusBarLabel
            // 
            this.statusBarLabel.Id = 88;
            this.statusBarLabel.Name = "statusBarLabel";
            this.statusBarLabel.TextAlignment = System.Drawing.StringAlignment.Near;
            this.statusBarLabel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btnPreferences
            // 
            this.btnPreferences.Caption = "Preferences";
            this.btnPreferences.Id = 91;
            this.btnPreferences.LargeImageIndex = 14;
            this.btnPreferences.Name = "btnPreferences";
            this.btnPreferences.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPreferences_ItemClick);
            // 
            // btnAutoSentence
            // 
            this.btnAutoSentence.Caption = "Auto Sentence";
            this.btnAutoSentence.Id = 92;
            this.btnAutoSentence.ImageIndex = 0;
            this.btnAutoSentence.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.OemPeriod));
            this.btnAutoSentence.Name = "btnAutoSentence";
            toolTipTitleItem4.Text = "Auto Sentence (Alt+.)";
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "End a sentence: Inserts a period, a space, and ensures the next word is capitaliz" +
    "ed.";
            superToolTip4.Items.Add(toolTipTitleItem4);
            superToolTip4.Items.Add(toolTipItem4);
            this.btnAutoSentence.SuperTip = superToolTip4;
            this.btnAutoSentence.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAutoSentence_ItemClick);
            // 
            // btnNextAnomaly
            // 
            this.btnNextAnomaly.Caption = "Next Anomaly";
            this.btnNextAnomaly.Hint = "Moves to the next anomaly in the document.";
            this.btnNextAnomaly.Id = 93;
            this.btnNextAnomaly.ImageIndex = 1;
            this.btnNextAnomaly.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R));
            this.btnNextAnomaly.Name = "btnNextAnomaly";
            this.btnNextAnomaly.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNextAnomaly_ItemClick);
            // 
            // btnPreviousAnomaly
            // 
            this.btnPreviousAnomaly.Caption = "Previous Anomaly";
            this.btnPreviousAnomaly.Hint = "Moves to the previous anomaly in the document.";
            this.btnPreviousAnomaly.Id = 94;
            this.btnPreviousAnomaly.ImageIndex = 1;
            this.btnPreviousAnomaly.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Alt) 
                | System.Windows.Forms.Keys.R));
            this.btnPreviousAnomaly.Name = "btnPreviousAnomaly";
            this.btnPreviousAnomaly.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPreviousAnomaly_ItemClick);
            // 
            // btnResetLayout
            // 
            this.btnResetLayout.Caption = "Reset Default Layout";
            this.btnResetLayout.Id = 96;
            this.btnResetLayout.LargeImageIndex = 16;
            this.btnResetLayout.Name = "btnResetLayout";
            this.btnResetLayout.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnResetLayout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnResetLayout_ItemClick);
            // 
            // barJobTemplate
            // 
            this.barJobTemplate.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barJobTemplate.Caption = "Template:  BNJ_Letter ";
            this.barJobTemplate.Id = 97;
            this.barJobTemplate.ImageIndex = 2;
            this.barJobTemplate.Name = "barJobTemplate";
            this.barJobTemplate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // btnLogFolder
            // 
            this.btnLogFolder.Caption = "Open Log Folder";
            this.btnLogFolder.Id = 104;
            this.btnLogFolder.LargeImageIndex = 17;
            this.btnLogFolder.Name = "btnLogFolder";
            this.btnLogFolder.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLogFolder_ItemClick);
            // 
            // btnAutoCorrect
            // 
            this.btnAutoCorrect.Caption = "AutoCorrect Options";
            this.btnAutoCorrect.Id = 105;
            this.btnAutoCorrect.LargeImageIndex = 18;
            this.btnAutoCorrect.Name = "btnAutoCorrect";
            this.btnAutoCorrect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAutoCorrect_ItemClick);
            // 
            // btnReleaseJob
            // 
            this.btnReleaseJob.Caption = "Release Job";
            this.btnReleaseJob.Enabled = false;
            this.btnReleaseJob.Id = 106;
            this.btnReleaseJob.LargeImageIndex = 19;
            this.btnReleaseJob.Name = "btnReleaseJob";
            this.btnReleaseJob.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnReleaseJob_ItemClick);
            // 
            // btnLaunchPortal
            // 
            this.btnLaunchPortal.AllowHtmlText = DevExpress.Utils.DefaultBoolean.False;
            this.btnLaunchPortal.Caption = "Portal";
            this.btnLaunchPortal.Hint = "Launches the Entrada Editor Portal.";
            this.btnLaunchPortal.Id = 109;
            this.btnLaunchPortal.ImageIndex = 3;
            this.btnLaunchPortal.Name = "btnLaunchPortal";
            this.btnLaunchPortal.TextAlignment = System.Drawing.StringAlignment.Near;
            this.btnLaunchPortal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLaunchPortal_ItemClick);
            // 
            // btnWorkSummary
            // 
            this.btnWorkSummary.Caption = "Work  Summary";
            this.btnWorkSummary.Id = 113;
            this.btnWorkSummary.LargeImageIndex = 20;
            this.btnWorkSummary.Name = "btnWorkSummary";
            this.btnWorkSummary.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnWorkSummary_ItemClick);
            // 
            // btnCC
            // 
            this.btnCC.Caption = "Add CC";
            this.btnCC.Hint = "Add a CC to the document.";
            this.btnCC.Id = 114;
            this.btnCC.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8);
            this.btnCC.LargeImageIndex = 21;
            this.btnCC.Name = "btnCC";
            this.btnCC.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCC_ItemClick);
            // 
            // btnSendLogs
            // 
            this.btnSendLogs.Caption = "Send Logs to Support";
            this.btnSendLogs.Hint = "Sends log files to Support if requested.";
            this.btnSendLogs.Id = 115;
            this.btnSendLogs.LargeImageIndex = 22;
            this.btnSendLogs.Name = "btnSendLogs";
            this.btnSendLogs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSendLogs_ItemClick);
            // 
            // btnSpellCheck
            // 
            this.btnSpellCheck.Caption = "Spell Check";
            this.btnSpellCheck.Id = 116;
            this.btnSpellCheck.ImageIndex = 5;
            this.btnSpellCheck.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S));
            this.btnSpellCheck.Name = "btnSpellCheck";
            this.btnSpellCheck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSpellCheck_ItemClick);
            // 
            // btnMakeUpperCase
            // 
            this.btnMakeUpperCase.Caption = "Upper Case";
            this.btnMakeUpperCase.Enabled = false;
            this.btnMakeUpperCase.Id = 117;
            this.btnMakeUpperCase.ImageIndex = 4;
            this.btnMakeUpperCase.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3));
            this.btnMakeUpperCase.Name = "btnMakeUpperCase";
            this.btnMakeUpperCase.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnMakeUpperCase_ItemClick);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Caption = "Change Password";
            this.btnChangePassword.Id = 117;
            this.btnChangePassword.LargeImageIndex = 24;
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChangePassword_ItemClick);
            // 
            // btnLaunchGuide
            // 
            this.btnLaunchGuide.Caption = "Guide";
            this.btnLaunchGuide.Hint = "Loads the Entrada Editor Guide";
            this.btnLaunchGuide.Id = 118;
            this.btnLaunchGuide.ImageIndex = 6;
            this.btnLaunchGuide.Name = "btnLaunchGuide";
            this.btnLaunchGuide.TextAlignment = System.Drawing.StringAlignment.Near;
            this.btnLaunchGuide.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnLaunchGuide_ItemClick);
            // 
            // btnSameQA
            // 
            this.btnSameQA.Caption = "Return to Same QA Level";
            this.btnSameQA.Enabled = false;
            this.btnSameQA.Id = 121;
            this.btnSameQA.LargeImageIndex = 25;
            this.btnSameQA.Name = "btnSameQA";
            this.btnSameQA.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSameQA_ItemClick);
            // 
            // btnNextQA
            // 
            this.btnNextQA.Caption = "Send to QA";
            this.btnNextQA.Enabled = false;
            this.btnNextQA.Id = 122;
            this.btnNextQA.LargeImageIndex = 26;
            this.btnNextQA.Name = "btnNextQA";
            this.btnNextQA.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNextQA_ItemClick);
            // 
            // btnEntradaQA
            // 
            this.btnEntradaQA.Caption = "Send to Entrada QA";
            this.btnEntradaQA.Enabled = false;
            this.btnEntradaQA.Id = 123;
            this.btnEntradaQA.LargeImageIndex = 27;
            this.btnEntradaQA.Name = "btnEntradaQA";
            this.btnEntradaQA.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEntradaQA_ItemClick);
            // 
            // btnSendtoCR
            // 
            this.btnSendtoCR.Caption = "Send to Client Review";
            this.btnSendtoCR.Enabled = false;
            this.btnSendtoCR.Id = 124;
            this.btnSendtoCR.LargeImageIndex = 28;
            this.btnSendtoCR.Name = "btnSendtoCR";
            this.btnSendtoCR.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSendtoCR_ItemClick);
            // 
            // barCategory
            // 
            this.barCategory.Edit = this.rptLookUpEditCategory;
            this.barCategory.EditValue = "Select";
            this.barCategory.Hint = "QA Category";
            this.barCategory.Id = 136;
            this.barCategory.Name = "barCategory";
            this.barCategory.Tag = "Select";
            this.barCategory.Width = 90;
            this.barCategory.EditValueChanged += new System.EventHandler(this.SubCategoryByParentId);
            // 
            // rptLookUpEditCategory
            // 
            this.rptLookUpEditCategory.AutoHeight = false;
            this.rptLookUpEditCategory.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rptLookUpEditCategory.Name = "rptLookUpEditCategory";
            // 
            // barSubCategory
            // 
            this.barSubCategory.Edit = this.rptLookUpEditSubCategory;
            this.barSubCategory.EditValue = "Select";
            this.barSubCategory.Hint = "QA SubCategory";
            this.barSubCategory.Id = 137;
            this.barSubCategory.Name = "barSubCategory";
            this.barSubCategory.Width = 90;
            // 
            // rptLookUpEditSubCategory
            // 
            this.rptLookUpEditSubCategory.AutoHeight = false;
            this.rptLookUpEditSubCategory.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rptLookUpEditSubCategory.Name = "rptLookUpEditSubCategory";
            // 
            // barCreateNote
            // 
            this.barCreateNote.Caption = "Create Note";
            this.barCreateNote.Enabled = false;
            this.barCreateNote.Id = 139;
            this.barCreateNote.LargeImageIndex = 29;
            this.barCreateNote.Name = "barCreateNote";
            this.barCreateNote.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCreateNote_ItemClick);
            // 
            // largeImages
            // 
            this.largeImages.ImageSize = new System.Drawing.Size(32, 32);
            this.largeImages.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("largeImages.ImageStream")));
            this.largeImages.Images.SetKeyName(0, "control_play_blue.png");
            this.largeImages.Images.SetKeyName(1, "control_stop_blue.png");
            this.largeImages.Images.SetKeyName(2, "control_fastforward_blue.png");
            this.largeImages.Images.SetKeyName(3, "control_rewind_blue.png");
            this.largeImages.Images.SetKeyName(4, "time_add.png");
            this.largeImages.Images.SetKeyName(5, "time_delete.png");
            this.largeImages.Images.SetKeyName(6, "page_white_check.png");
            this.largeImages.Images.SetKeyName(7, "page_white_qa.png");
            this.largeImages.Images.SetKeyName(8, "split_patient.png");
            this.largeImages.Images.SetKeyName(9, "document_insert.png");
            this.largeImages.Images.SetKeyName(10, "group_search.png");
            this.largeImages.Images.SetKeyName(11, "physicians_search.png");
            this.largeImages.Images.SetKeyName(12, "sitemap_color.png");
            this.largeImages.Images.SetKeyName(13, "google_custom_search.png");
            this.largeImages.Images.SetKeyName(14, "interface_preferences.png");
            this.largeImages.Images.SetKeyName(15, "page_white_star.png");
            this.largeImages.Images.SetKeyName(16, "application_side_boxes.png");
            this.largeImages.Images.SetKeyName(17, "folder_bug.png");
            this.largeImages.Images.SetKeyName(18, "wand.png");
            this.largeImages.Images.SetKeyName(19, "document_import.png");
            this.largeImages.Images.SetKeyName(20, "chart_line.png");
            this.largeImages.Images.SetKeyName(21, "cc.png");
            this.largeImages.Images.SetKeyName(22, "documents_email.png");
            this.largeImages.Images.SetKeyName(23, "spellcheck.png");
            this.largeImages.Images.SetKeyName(24, "change_password.png");
            this.largeImages.Images.SetKeyName(25, "SameQA.png");
            this.largeImages.Images.SetKeyName(26, "NextQA.png");
            this.largeImages.Images.SetKeyName(27, "EntradaQA.png");
            this.largeImages.Images.SetKeyName(28, "ClientReview.png");
            this.largeImages.Images.SetKeyName(29, "Create Notes.png");
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grpDownloadQueue,
            this.grpDocuments,
            this.grpClientData,
            this.grpQA,
            this.grpAudio});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Home";
            // 
            // grpDownloadQueue
            // 
            this.grpDownloadQueue.ItemLinks.Add(this.btnStartEditing);
            this.grpDownloadQueue.ItemLinks.Add(this.btnStopEditing);
            this.grpDownloadQueue.ItemLinks.Add(this.btnManualDownload);
            this.grpDownloadQueue.Name = "grpDownloadQueue";
            this.grpDownloadQueue.ShowCaptionButton = false;
            this.grpDownloadQueue.Text = "Download Queue";
            // 
            // grpDocuments
            // 
            this.grpDocuments.Enabled = false;
            this.grpDocuments.ItemLinks.Add(this.btnFinishDocument);
            this.grpDocuments.ItemLinks.Add(this.btnSendToQA);
            this.grpDocuments.ItemLinks.Add(this.btnSplitJobType, true);
            this.grpDocuments.ItemLinks.Add(this.btnSplitPatient);
            this.grpDocuments.Name = "grpDocuments";
            this.grpDocuments.ShowCaptionButton = false;
            this.grpDocuments.Text = "Documents";
            // 
            // grpClientData
            // 
            this.grpClientData.Enabled = false;
            this.grpClientData.ItemLinks.Add(this.btnPatients);
            this.grpClientData.ItemLinks.Add(this.btnReferring);
            this.grpClientData.ItemLinks.Add(this.btnInsertMacro);
            this.grpClientData.ItemLinks.Add(this.btnCC);
            this.grpClientData.Name = "grpClientData";
            this.grpClientData.ShowCaptionButton = false;
            this.grpClientData.Text = "Client Data";
            // 
            // grpQA
            // 
            this.grpQA.ItemLinks.Add(this.btnSameQA);
            this.grpQA.ItemLinks.Add(this.btnNextQA);
            this.grpQA.ItemLinks.Add(this.btnEntradaQA);
            this.grpQA.ItemLinks.Add(this.barCreateNote);
            this.grpQA.ItemLinks.Add(this.btnSendtoCR);
            this.grpQA.ItemLinks.Add(this.barCategory);
            this.grpQA.ItemLinks.Add(this.barSubCategory);
            this.grpQA.Name = "grpQA";
            this.grpQA.ShowCaptionButton = false;
            this.grpQA.Text = "Send";
            // 
            // grpAudio
            // 
            this.grpAudio.Enabled = false;
            this.grpAudio.ItemLinks.Add(this.btnRewindAudio);
            this.grpAudio.ItemLinks.Add(this.btnPlayAudio);
            this.grpAudio.ItemLinks.Add(this.btnStopAudio);
            this.grpAudio.ItemLinks.Add(this.btnFastForwardAudio);
            this.grpAudio.ItemLinks.Add(this.btnDecreaseSpeed, true);
            this.grpAudio.ItemLinks.Add(this.btnIncreaseSpeed);
            this.grpAudio.Name = "grpAudio";
            this.grpAudio.ShowCaptionButton = false;
            this.grpAudio.Text = "Audio";
            // 
            // homeRibbonPage1
            // 
            this.homeRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.clipboardRibbonPageGroup1,
            this.fontRibbonPageGroup1,
            this.paragraphRibbonPageGroup1,
            this.editingRibbonPageGroup1,
            this.grpQuickFix});
            this.homeRibbonPage1.Name = "homeRibbonPage1";
            this.homeRibbonPage1.Text = "Format";
            // 
            // clipboardRibbonPageGroup1
            // 
            this.clipboardRibbonPageGroup1.ItemLinks.Add(this.pasteItem1);
            this.clipboardRibbonPageGroup1.ItemLinks.Add(this.cutItem1);
            this.clipboardRibbonPageGroup1.ItemLinks.Add(this.copyItem1);
            this.clipboardRibbonPageGroup1.ItemLinks.Add(this.pasteSpecialItem1);
            this.clipboardRibbonPageGroup1.Name = "clipboardRibbonPageGroup1";
            // 
            // fontRibbonPageGroup1
            // 
            this.fontRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup2);
            this.fontRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup3);
            this.fontRibbonPageGroup1.Name = "fontRibbonPageGroup1";
            // 
            // paragraphRibbonPageGroup1
            // 
            this.paragraphRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup5);
            this.paragraphRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup6);
            this.paragraphRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup7);
            this.paragraphRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup8);
            this.paragraphRibbonPageGroup1.Name = "paragraphRibbonPageGroup1";
            // 
            // editingRibbonPageGroup1
            // 
            this.editingRibbonPageGroup1.ItemLinks.Add(this.findItem1);
            this.editingRibbonPageGroup1.ItemLinks.Add(this.replaceItem1);
            this.editingRibbonPageGroup1.Name = "editingRibbonPageGroup1";
            // 
            // grpQuickFix
            // 
            this.grpQuickFix.Enabled = false;
            this.grpQuickFix.ItemLinks.Add(this.btnAutoSentence);
            this.grpQuickFix.ItemLinks.Add(this.btnNextAnomaly);
            this.grpQuickFix.ItemLinks.Add(this.btnPreviousAnomaly);
            this.grpQuickFix.ItemLinks.Add(this.btnMakeUpperCase);
            this.grpQuickFix.ItemLinks.Add(this.btnSpellCheck);
            this.grpQuickFix.Name = "grpQuickFix";
            this.grpQuickFix.ShowCaptionButton = false;
            this.grpQuickFix.Text = "QuickFix";
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.grpAdvancedDocuments,
            this.ribbonPageGroup5,
            this.ribbonPageGroup1});
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "Advanced";
            // 
            // grpAdvancedDocuments
            // 
            this.grpAdvancedDocuments.ItemLinks.Add(this.btnReleaseJob);
            this.grpAdvancedDocuments.ItemLinks.Add(this.btnWorkSummary);
            this.grpAdvancedDocuments.Name = "grpAdvancedDocuments";
            this.grpAdvancedDocuments.ShowCaptionButton = false;
            this.grpAdvancedDocuments.Text = "Documents";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.btnPreferences);
            this.ribbonPageGroup5.ItemLinks.Add(this.btnAutoCorrect);
            this.ribbonPageGroup5.ItemLinks.Add(this.btnResetLayout);
            this.ribbonPageGroup5.ItemLinks.Add(this.btnChangePassword);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.ShowCaptionButton = false;
            this.ribbonPageGroup5.Text = "Preferences";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnLogFolder);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSendLogs);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.ShowCaptionButton = false;
            this.ribbonPageGroup1.Text = "Debug";
            // 
            // repositoryItemZoomTrackBar1
            // 
            this.repositoryItemZoomTrackBar1.Maximum = 30;
            this.repositoryItemZoomTrackBar1.Middle = 5;
            this.repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
            this.repositoryItemZoomTrackBar1.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", 100, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.repositoryItemComboBox2.Items.AddRange(new object[] {
            "Dictation Quality",
            "Missing Information",
            "TI",
            "Medical Knowledge",
            "Editor",
            "FR",
            "IT"});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // repositoryItemComboBox3
            // 
            this.repositoryItemComboBox3.AutoHeight = false;
            this.repositoryItemComboBox3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox3.Name = "repositoryItemComboBox3";
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // repositoryItemComboBox4
            // 
            this.repositoryItemComboBox4.AutoHeight = false;
            this.repositoryItemComboBox4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox4.Name = "repositoryItemComboBox4";
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // repositoryItemComboBox5
            // 
            this.repositoryItemComboBox5.AutoHeight = false;
            this.repositoryItemComboBox5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox5.Name = "repositoryItemComboBox5";
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            // 
            // repositoryItemComboBox6
            // 
            this.repositoryItemComboBox6.AutoHeight = false;
            this.repositoryItemComboBox6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox6.Name = "repositoryItemComboBox6";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.barJobTemplate);
            this.ribbonStatusBar1.ItemLinks.Add(this.barZoomSlider);
            this.ribbonStatusBar1.ItemLinks.Add(this.trkAudioPosition);
            this.ribbonStatusBar1.ItemLinks.Add(this.lblAudioPosition);
            this.ribbonStatusBar1.ItemLinks.Add(this.statusBarLabel);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 736);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1282, 31);
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel1.ID = new System.Guid("e4592448-7b97-4e48-a1f4-48d796b68c0b");
            this.dockPanel1.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel1.Size = new System.Drawing.Size(192, 200);
            this.dockPanel1.Text = "Tags";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(192, 200);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // panelContainer1
            // 
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.panelContainer1.ID = new System.Guid("3c79939a-e263-417a-b713-609b8325f2e8");
            this.panelContainer1.Location = new System.Drawing.Point(782, 142);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(200, 200);
            this.panelContainer1.Size = new System.Drawing.Size(200, 507);
            // 
            // panelContainer2
            // 
            this.panelContainer2.ActiveChild = this.dockPanel1;
            this.panelContainer2.Controls.Add(this.dockPanel1);
            this.panelContainer2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.panelContainer2.FloatVertical = true;
            this.panelContainer2.ID = new System.Guid("b8865c04-1bb3-4b2b-a1e6-19109381bd4a");
            this.panelContainer2.Location = new System.Drawing.Point(0, 253);
            this.panelContainer2.Name = "panelContainer2";
            this.panelContainer2.OriginalSize = new System.Drawing.Size(200, 200);
            this.panelContainer2.Size = new System.Drawing.Size(200, 254);
            this.panelContainer2.Tabbed = true;
            this.panelContainer2.Text = "panelContainer2";
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel3});
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer3,
            this.pnlQANotes});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel3
            // 
            this.dockPanel3.Controls.Add(this.dockPanel3_Container);
            this.dockPanel3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel3.ID = new System.Guid("83fcffe9-ccd9-47a3-b00b-6b5ce7ac4dda");
            this.dockPanel3.Location = new System.Drawing.Point(0, 414);
            this.dockPanel3.Name = "dockPanel3";
            this.dockPanel3.OriginalSize = new System.Drawing.Size(200, 198);
            this.dockPanel3.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel3.SavedIndex = 1;
            this.dockPanel3.SavedParent = this.panelContainer3;
            this.dockPanel3.Size = new System.Drawing.Size(250, 192);
            this.dockPanel3.Text = "dockPanel3";
            this.dockPanel3.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(242, 165);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // panelContainer3
            // 
            this.panelContainer3.Controls.Add(this.dockPanel4);
            this.panelContainer3.Controls.Add(this.dckTags);
            this.panelContainer3.Controls.Add(this.dockPanel2);
            this.panelContainer3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer3.ID = new System.Guid("117be936-c126-4ed2-a356-58ca550a1bc1");
            this.panelContainer3.Location = new System.Drawing.Point(1058, 144);
            this.panelContainer3.Name = "panelContainer3";
            this.panelContainer3.OriginalSize = new System.Drawing.Size(224, 200);
            this.panelContainer3.Size = new System.Drawing.Size(224, 592);
            this.panelContainer3.Text = "panelContainer3";
            // 
            // dockPanel4
            // 
            this.dockPanel4.Controls.Add(this.dockPanel4_Container);
            this.dockPanel4.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel4.FloatVertical = true;
            this.dockPanel4.ID = new System.Guid("625c8d4e-dc0a-42a1-bad5-6d7b7fe6fe8d");
            this.dockPanel4.Location = new System.Drawing.Point(0, 0);
            this.dockPanel4.Name = "dockPanel4";
            this.dockPanel4.Options.ShowCloseButton = false;
            this.dockPanel4.OriginalSize = new System.Drawing.Size(224, 124);
            this.dockPanel4.Size = new System.Drawing.Size(224, 124);
            this.dockPanel4.Text = "Downloaded Jobs";
            // 
            // dockPanel4_Container
            // 
            this.dockPanel4_Container.Controls.Add(this.lstDownloadedJobs);
            this.dockPanel4_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel4_Container.Name = "dockPanel4_Container";
            this.dockPanel4_Container.Size = new System.Drawing.Size(216, 97);
            this.dockPanel4_Container.TabIndex = 0;
            // 
            // lstDownloadedJobs
            // 
            this.lstDownloadedJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDownloadedJobs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstDownloadedJobs.HighlightColor = System.Drawing.Color.Empty;
            this.lstDownloadedJobs.IntegralHeight = false;
            this.lstDownloadedJobs.ItemHeight = 32;
            this.lstDownloadedJobs.Location = new System.Drawing.Point(0, 0);
            this.lstDownloadedJobs.Name = "lstDownloadedJobs";
            this.lstDownloadedJobs.Size = new System.Drawing.Size(216, 97);
            this.lstDownloadedJobs.TabIndex = 0;
            // 
            // dckTags
            // 
            this.dckTags.Controls.Add(this.controlContainer1);
            this.dckTags.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dckTags.ID = new System.Guid("c2b2515e-3ed7-4394-ba09-08a471ed8199");
            this.dckTags.Location = new System.Drawing.Point(0, 124);
            this.dckTags.Name = "dckTags";
            this.dckTags.Options.ShowCloseButton = false;
            this.dckTags.OriginalSize = new System.Drawing.Size(224, 117);
            this.dckTags.Size = new System.Drawing.Size(224, 117);
            this.dckTags.Text = "TDD Tags";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.lstTags);
            this.controlContainer1.Location = new System.Drawing.Point(4, 23);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(216, 90);
            this.controlContainer1.TabIndex = 0;
            // 
            // lstTags
            // 
            this.lstTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTags.Location = new System.Drawing.Point(0, 0);
            this.lstTags.Name = "lstTags";
            this.lstTags.Size = new System.Drawing.Size(216, 90);
            this.lstTags.TabIndex = 0;
            this.lstTags.DoubleClick += new System.EventHandler(this.lstTags_DoubleClick);
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel2.ID = new System.Guid("702065f5-2e88-421b-8fe2-dcb8fdec5ffc");
            this.dockPanel2.Location = new System.Drawing.Point(0, 241);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.Options.ShowCloseButton = false;
            this.dockPanel2.OriginalSize = new System.Drawing.Size(224, 351);
            this.dockPanel2.Size = new System.Drawing.Size(224, 351);
            this.dockPanel2.Text = "Demographics";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.grdDemographics);
            this.dockPanel2_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(216, 324);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // grdDemographics
            // 
            this.grdDemographics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDemographics.Location = new System.Drawing.Point(0, 0);
            this.grdDemographics.Name = "grdDemographics";
            this.grdDemographics.OptionsHint.ShowCellHints = false;
            this.grdDemographics.Size = new System.Drawing.Size(216, 324);
            this.grdDemographics.TabIndex = 0;
            // 
            // pnlQANotes
            // 
            this.pnlQANotes.Controls.Add(this.dockPanel5_Container);
            this.pnlQANotes.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.pnlQANotes.ID = new System.Guid("20758d89-59bf-4ebe-a358-8eee242ae54c");
            this.pnlQANotes.Location = new System.Drawing.Point(0, 631);
            this.pnlQANotes.Name = "pnlQANotes";
            this.pnlQANotes.Options.ShowCloseButton = false;
            this.pnlQANotes.OriginalSize = new System.Drawing.Size(200, 105);
            this.pnlQANotes.Size = new System.Drawing.Size(1058, 105);
            this.pnlQANotes.Text = "QA Note";
            // 
            // dockPanel5_Container
            // 
            this.dockPanel5_Container.Controls.Add(this.txtQANote);
            this.dockPanel5_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel5_Container.Name = "dockPanel5_Container";
            this.dockPanel5_Container.Size = new System.Drawing.Size(1050, 78);
            this.dockPanel5_Container.TabIndex = 0;
            // 
            // txtQANote
            // 
            this.txtQANote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQANote.Enabled = false;
            this.txtQANote.Location = new System.Drawing.Point(0, 0);
            this.txtQANote.MenuManager = this.ribbonControl1;
            this.txtQANote.Name = "txtQANote";
            this.spellChecker1.SetShowSpellCheckMenu(this.txtQANote, true);
            this.txtQANote.Size = new System.Drawing.Size(1050, 78);
            this.spellChecker1.SetSpellCheckerOptions(this.txtQANote, optionsSpelling1);
            this.txtQANote.TabIndex = 0;
            this.txtQANote.UseOptimizedRendering = true;
            // 
            // documentManager1
            // 
            this.documentManager1.ContainerControl = this;
            this.documentManager1.View = this.tabbedView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // richEditBarController1
            // 
            this.richEditBarController1.BarItems.Add(this.pasteItem1);
            this.richEditBarController1.BarItems.Add(this.cutItem1);
            this.richEditBarController1.BarItems.Add(this.copyItem1);
            this.richEditBarController1.BarItems.Add(this.pasteSpecialItem1);
            this.richEditBarController1.BarItems.Add(this.changeFontNameItem1);
            this.richEditBarController1.BarItems.Add(this.changeFontSizeItem1);
            this.richEditBarController1.BarItems.Add(this.fontSizeIncreaseItem1);
            this.richEditBarController1.BarItems.Add(this.fontSizeDecreaseItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontBoldItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontItalicItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontUnderlineItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontDoubleUnderlineItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontStrikeoutItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontDoubleStrikeoutItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontSuperscriptItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontSubscriptItem1);
            this.richEditBarController1.BarItems.Add(this.changeFontColorItem1);
            this.richEditBarController1.BarItems.Add(this.changeFontBackColorItem1);
            this.richEditBarController1.BarItems.Add(this.changeTextCaseItem1);
            this.richEditBarController1.BarItems.Add(this.makeTextUpperCaseItem1);
            this.richEditBarController1.BarItems.Add(this.makeTextLowerCaseItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTextCaseItem1);
            this.richEditBarController1.BarItems.Add(this.clearFormattingItem1);
            this.richEditBarController1.BarItems.Add(this.toggleBulletedListItem1);
            this.richEditBarController1.BarItems.Add(this.toggleNumberingListItem1);
            this.richEditBarController1.BarItems.Add(this.toggleMultiLevelListItem1);
            this.richEditBarController1.BarItems.Add(this.decreaseIndentItem1);
            this.richEditBarController1.BarItems.Add(this.increaseIndentItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentLeftItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentCenterItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentRightItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentJustifyItem1);
            this.richEditBarController1.BarItems.Add(this.toggleShowWhitespaceItem1);
            this.richEditBarController1.BarItems.Add(this.changeParagraphLineSpacingItem1);
            this.richEditBarController1.BarItems.Add(this.setSingleParagraphSpacingItem1);
            this.richEditBarController1.BarItems.Add(this.setSesquialteralParagraphSpacingItem1);
            this.richEditBarController1.BarItems.Add(this.setDoubleParagraphSpacingItem1);
            this.richEditBarController1.BarItems.Add(this.showLineSpacingFormItem1);
            this.richEditBarController1.BarItems.Add(this.addSpacingBeforeParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.removeSpacingBeforeParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.addSpacingAfterParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.removeSpacingAfterParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.changeParagraphBackColorItem1);
            this.richEditBarController1.BarItems.Add(this.galleryChangeStyleItem1);
            this.richEditBarController1.BarItems.Add(this.findItem1);
            this.richEditBarController1.BarItems.Add(this.replaceItem1);
            // 
            // audioDjStudio1
            // 
            this.audioDjStudio1.Fader = ((AudioDjStudio.FaderObject)(resources.GetObject("audioDjStudio1.Fader")));
            this.audioDjStudio1.LastError = AudioDjStudio.enumErrorCodes.ERR_NOERROR;
            this.audioDjStudio1.Location = new System.Drawing.Point(12, 150);
            this.audioDjStudio1.Name = "audioDjStudio1";
            this.audioDjStudio1.Size = new System.Drawing.Size(48, 48);
            this.audioDjStudio1.TabIndex = 5;
            // 
            // tmrAudioPosition
            // 
            this.tmrAudioPosition.Interval = 200;
            this.tmrAudioPosition.Tick += new System.EventHandler(this.tmrAudioPosition_Tick);
            // 
            // spellChecker1
            // 
            this.spellChecker1.Culture = new System.Globalization.CultureInfo("en-US");
            this.spellChecker1.LoadOnDemand = true;
            this.spellChecker1.ParentContainer = null;
            this.spellChecker1.SpellCheckMode = DevExpress.XtraSpellChecker.SpellCheckMode.AsYouType;
            // 
            // tmrDownloadQueue
            // 
            this.tmrDownloadQueue.Interval = 50000;
            this.tmrDownloadQueue.Tick += new System.EventHandler(this.tmrDownloadQueue_Tick);
            // 
            // tmrRewindForward
            // 
            this.tmrRewindForward.Tick += new System.EventHandler(this.tmrRewindForward_Tick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Next Anomaly";
            this.barButtonItem1.Hint = "Moves to the next anomaly in the document.";
            this.barButtonItem1.Id = 93;
            this.barButtonItem1.ImageIndex = 1;
            this.barButtonItem1.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // EditorForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1282, 767);
            this.Controls.Add(this.audioDjStudio1);
            this.Controls.Add(this.pnlQANotes);
            this.Controls.Add(this.panelContainer3);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "EditorForm";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "Entrada Editor";
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.Shown += new System.EventHandler(this.EditorForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smallImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptLookUpEditCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptLookUpEditSubCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.largeImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox6)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.panelContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel3.ResumeLayout(false);
            this.panelContainer3.ResumeLayout(false);
            this.dockPanel4.ResumeLayout(false);
            this.dockPanel4_Container.ResumeLayout(false);
            this.dckTags.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstTags)).EndInit();
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDemographics)).EndInit();
            this.pnlQANotes.ResumeLayout(false);
            this.dockPanel5_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtQANote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.richEditBarController1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
		private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpDownloadQueue;
		private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
		private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
		private DevExpress.XtraBars.Docking.DockPanel panelContainer2;
		private DevExpress.XtraBars.Docking.DockManager dockManager1;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel3;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraVerticalGrid.PropertyGridControl grdDemographics;
		private DevExpress.XtraBars.Docking.DockPanel panelContainer3;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel4;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel4_Container;
        private Entrada.Editor.DownloadedJobsListBox lstDownloadedJobs;
		private DevExpress.XtraBars.BarEditItem trkAudioPosition;
		private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar1;
		private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;
		private DevExpress.XtraBars.BarButtonGroup barButtonGroup1;
		private DevExpress.XtraBars.BarStaticItem lblAudioPosition;
		private DevExpress.XtraBars.BarButtonItem btnStartEditing;
		private DevExpress.XtraBars.BarButtonItem btnStopEditing;
		private DevExpress.Utils.ImageCollection largeImages;
		private DevExpress.XtraRichEdit.UI.PasteItem pasteItem1;
		private DevExpress.XtraRichEdit.UI.CutItem cutItem1;
		private DevExpress.XtraRichEdit.UI.CopyItem copyItem1;
		private DevExpress.XtraRichEdit.UI.PasteSpecialItem pasteSpecialItem1;
		private DevExpress.XtraBars.BarButtonGroup barButtonGroup2;
		private DevExpress.XtraRichEdit.UI.ChangeFontNameItem changeFontNameItem1;
		private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
		private DevExpress.XtraRichEdit.UI.ChangeFontSizeItem changeFontSizeItem1;
		private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit repositoryItemRichEditFontSizeEdit1;
		private DevExpress.XtraRichEdit.UI.FontSizeIncreaseItem fontSizeIncreaseItem1;
		private DevExpress.XtraRichEdit.UI.FontSizeDecreaseItem fontSizeDecreaseItem1;
		private DevExpress.XtraBars.BarButtonGroup barButtonGroup3;
		private DevExpress.XtraRichEdit.UI.ToggleFontBoldItem toggleFontBoldItem1;
		private DevExpress.XtraRichEdit.UI.ToggleFontItalicItem toggleFontItalicItem1;
		private DevExpress.XtraRichEdit.UI.ToggleFontUnderlineItem toggleFontUnderlineItem1;
		private DevExpress.XtraRichEdit.UI.ToggleFontDoubleUnderlineItem toggleFontDoubleUnderlineItem1;
		private DevExpress.XtraRichEdit.UI.ToggleFontStrikeoutItem toggleFontStrikeoutItem1;
		private DevExpress.XtraRichEdit.UI.ToggleFontDoubleStrikeoutItem toggleFontDoubleStrikeoutItem1;
		private DevExpress.XtraRichEdit.UI.ToggleFontSuperscriptItem toggleFontSuperscriptItem1;
		private DevExpress.XtraRichEdit.UI.ToggleFontSubscriptItem toggleFontSubscriptItem1;
		private DevExpress.XtraBars.BarButtonGroup barButtonGroup4;
		private DevExpress.XtraRichEdit.UI.ChangeFontColorItem changeFontColorItem1;
		private DevExpress.XtraRichEdit.UI.ChangeFontBackColorItem changeFontBackColorItem1;
		private DevExpress.XtraRichEdit.UI.ChangeTextCaseItem changeTextCaseItem1;
		private DevExpress.XtraRichEdit.UI.MakeTextUpperCaseItem makeTextUpperCaseItem1;
		private DevExpress.XtraRichEdit.UI.MakeTextLowerCaseItem makeTextLowerCaseItem1;
		private DevExpress.XtraRichEdit.UI.ToggleTextCaseItem toggleTextCaseItem1;
		private DevExpress.XtraRichEdit.UI.ClearFormattingItem clearFormattingItem1;
		private DevExpress.XtraBars.BarButtonGroup barButtonGroup5;
		private DevExpress.XtraRichEdit.UI.ToggleBulletedListItem toggleBulletedListItem1;
		private DevExpress.XtraRichEdit.UI.ToggleNumberingListItem toggleNumberingListItem1;
		private DevExpress.XtraRichEdit.UI.ToggleMultiLevelListItem toggleMultiLevelListItem1;
		private DevExpress.XtraBars.BarButtonGroup barButtonGroup6;
		private DevExpress.XtraRichEdit.UI.DecreaseIndentItem decreaseIndentItem1;
		private DevExpress.XtraRichEdit.UI.IncreaseIndentItem increaseIndentItem1;
		private DevExpress.XtraRichEdit.UI.ToggleShowWhitespaceItem toggleShowWhitespaceItem1;
		private DevExpress.XtraBars.BarButtonGroup barButtonGroup7;
		private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentLeftItem toggleParagraphAlignmentLeftItem1;
		private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentCenterItem toggleParagraphAlignmentCenterItem1;
		private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentRightItem toggleParagraphAlignmentRightItem1;
		private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentJustifyItem toggleParagraphAlignmentJustifyItem1;
		private DevExpress.XtraBars.BarButtonGroup barButtonGroup8;
		private DevExpress.XtraRichEdit.UI.ChangeParagraphLineSpacingItem changeParagraphLineSpacingItem1;
		private DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem setSingleParagraphSpacingItem1;
		private DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem setSesquialteralParagraphSpacingItem1;
		private DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem setDoubleParagraphSpacingItem1;
		private DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem showLineSpacingFormItem1;
		private DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem addSpacingBeforeParagraphItem1;
		private DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem removeSpacingBeforeParagraphItem1;
		private DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem addSpacingAfterParagraphItem1;
		private DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem removeSpacingAfterParagraphItem1;
		private DevExpress.XtraRichEdit.UI.ChangeParagraphBackColorItem changeParagraphBackColorItem1;
		private DevExpress.XtraRichEdit.UI.GalleryChangeStyleItem galleryChangeStyleItem1;
		private DevExpress.XtraRichEdit.UI.FindItem findItem1;
		private DevExpress.XtraRichEdit.UI.ReplaceItem replaceItem1;
		private DevExpress.XtraRichEdit.UI.HomeRibbonPage homeRibbonPage1;
		private DevExpress.XtraRichEdit.UI.ClipboardRibbonPageGroup clipboardRibbonPageGroup1;
		private DevExpress.XtraRichEdit.UI.FontRibbonPageGroup fontRibbonPageGroup1;
		private DevExpress.XtraRichEdit.UI.ParagraphRibbonPageGroup paragraphRibbonPageGroup1;
		private DevExpress.XtraRichEdit.UI.EditingRibbonPageGroup editingRibbonPageGroup1;
		private DevExpress.XtraRichEdit.UI.RichEditBarController richEditBarController1;
		private AudioDjStudio.AudioDjStudio audioDjStudio1;
		private System.Windows.Forms.Timer tmrAudioPosition;
		private DevExpress.XtraBars.BarButtonItem btnRewindAudio;
		private DevExpress.XtraBars.BarButtonItem btnPlayAudio;
		private DevExpress.XtraBars.BarButtonItem btnStopAudio;
		private DevExpress.XtraBars.BarButtonItem btnFastForwardAudio;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpAudio;
		private DevExpress.XtraBars.BarButtonItem btnIncreaseSpeed;
		private DevExpress.XtraBars.BarButtonItem btnDecreaseSpeed;
		private DevExpress.XtraSpellChecker.SpellChecker spellChecker1;
		private DevExpress.XtraBars.BarButtonItem btnFinishDocument;
		private DevExpress.XtraBars.BarButtonItem btnSendToQA;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpDocuments;
		private DevExpress.XtraBars.BarButtonItem btnSplitJobType;
		private DevExpress.XtraBars.BarButtonItem btnSplitPatient;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpClientData;
		private DevExpress.XtraBars.BarButtonItem btnPatients;
		private DevExpress.XtraBars.BarButtonItem btnReferring;
		private DevExpress.XtraBars.BarButtonItem btnInsertMacro;
        private DevExpress.XtraBars.BarButtonItem btnManualDownload;
        private DevExpress.XtraBars.BarEditItem barZoomSlider;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar2;
        private DevExpress.XtraBars.BarStaticItem statusBarLabel;
        private System.Windows.Forms.Timer tmrDownloadQueue;
        private System.Windows.Forms.Timer tmrRewindForward;
        private DevExpress.XtraBars.BarButtonItem btnPreferences;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.Utils.ImageCollection smallImages;
        private DevExpress.XtraBars.BarButtonItem btnAutoSentence;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpQuickFix;
        private DevExpress.XtraBars.BarButtonItem btnNextAnomaly;
        private DevExpress.XtraBars.BarButtonItem btnPreviousAnomaly;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        internal DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.BarButtonItem btnResetLayout;
        private DevExpress.XtraBars.BarSubItem barJobTemplate;
        private DevExpress.XtraBars.BarButtonItem btnLogFolder;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnAutoCorrect;
        private DevExpress.XtraBars.BarButtonItem btnReleaseJob;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpAdvancedDocuments;
        private DevExpress.XtraBars.BarStaticItem btnLaunchPortal;
        private DevExpress.XtraBars.BarButtonItem btnWorkSummary;
        private DevExpress.XtraBars.Docking.DockPanel pnlQANotes;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel5_Container;
        private DevExpress.XtraEditors.MemoEdit txtQANote;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.BarButtonItem btnCC;
        private DevExpress.XtraBars.BarButtonItem btnSendLogs;
        private DevExpress.XtraBars.Docking.DockPanel dckTags;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraEditors.ListBoxControl lstTags;
        private DevExpress.XtraBars.BarButtonItem btnSpellCheck;
        private DevExpress.XtraBars.BarButtonItem btnMakeUpperCase;
        private DevExpress.XtraBars.BarButtonItem btnChangePassword;
        private DevExpress.XtraBars.BarStaticItem btnLaunchGuide;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup grpQA;
        private DevExpress.XtraBars.BarButtonItem btnSameQA;
        private DevExpress.XtraBars.BarButtonItem btnNextQA;
        private DevExpress.XtraBars.BarButtonItem btnEntradaQA;
        private DevExpress.XtraBars.BarButtonItem btnSendtoCR;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox3;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox4;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox5;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox6;
        private DevExpress.XtraBars.BarEditItem barCategory;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rptLookUpEditCategory;
        private DevExpress.XtraBars.BarEditItem barSubCategory;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rptLookUpEditSubCategory;
        private DevExpress.XtraBars.BarButtonItem barCreateNote;
	}
}