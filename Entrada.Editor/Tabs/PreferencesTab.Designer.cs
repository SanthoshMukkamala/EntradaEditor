namespace Entrada.Editor
{
    partial class PreferencesTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.cmbAudioSpeed = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtFootPedalBounce = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtKeyboardBounce = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.cmbDefaultZoom = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.cmbAudioDevice = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAudioSpeed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFootPedalBounce.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyboardBounce.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDefaultZoom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAudioDevice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbAudioSpeed
            // 
            this.cmbAudioSpeed.Location = new System.Drawing.Point(180, 123);
            this.cmbAudioSpeed.Name = "cmbAudioSpeed";
            this.cmbAudioSpeed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAudioSpeed.Properties.Items.AddRange(new object[] {
            "200%",
            "400%",
            "800%",
            "1600%"});
            this.cmbAudioSpeed.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbAudioSpeed.Size = new System.Drawing.Size(100, 20);
            this.cmbAudioSpeed.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(3, 126);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(171, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Rewind / Fast Forward Speed:";
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.LineLocation = DevExpress.XtraEditors.LineLocation.Bottom;
            this.labelControl2.LineVisible = true;
            this.labelControl2.Location = new System.Drawing.Point(9, 89);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.ShowLineShadow = false;
            this.labelControl2.Size = new System.Drawing.Size(452, 29);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Audio Preferences";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(3, 157);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(171, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Foot Pedal Bounce-back:";
            // 
            // txtFootPedalBounce
            // 
            this.txtFootPedalBounce.Location = new System.Drawing.Point(180, 154);
            this.txtFootPedalBounce.Name = "txtFootPedalBounce";
            this.txtFootPedalBounce.Properties.Mask.EditMask = "N";
            this.txtFootPedalBounce.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtFootPedalBounce.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtFootPedalBounce.Size = new System.Drawing.Size(100, 20);
            this.txtFootPedalBounce.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.Location = new System.Drawing.Point(286, 157);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(67, 13);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "seconds";
            // 
            // labelControl5
            // 
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.Location = new System.Drawing.Point(286, 185);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(67, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "seconds";
            // 
            // txtKeyboardBounce
            // 
            this.txtKeyboardBounce.Location = new System.Drawing.Point(180, 182);
            this.txtKeyboardBounce.Name = "txtKeyboardBounce";
            this.txtKeyboardBounce.Properties.Mask.EditMask = "N";
            this.txtKeyboardBounce.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtKeyboardBounce.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtKeyboardBounce.Size = new System.Drawing.Size(100, 20);
            this.txtKeyboardBounce.TabIndex = 8;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.Location = new System.Drawing.Point(3, 185);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(171, 13);
            this.labelControl6.TabIndex = 7;
            this.labelControl6.Text = "Keyboard Bounce-back:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(326, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelControl7
            // 
            this.labelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl7.LineLocation = DevExpress.XtraEditors.LineLocation.Bottom;
            this.labelControl7.LineVisible = true;
            this.labelControl7.Location = new System.Drawing.Point(9, 3);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.ShowLineShadow = false;
            this.labelControl7.Size = new System.Drawing.Size(452, 29);
            this.labelControl7.TabIndex = 13;
            this.labelControl7.Text = "Document Preferences";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl8.Location = new System.Drawing.Point(3, 40);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(171, 13);
            this.labelControl8.TabIndex = 12;
            this.labelControl8.Text = "Default Zoom:";
            // 
            // cmbDefaultZoom
            // 
            this.cmbDefaultZoom.Location = new System.Drawing.Point(180, 37);
            this.cmbDefaultZoom.Name = "cmbDefaultZoom";
            this.cmbDefaultZoom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDefaultZoom.Properties.Items.AddRange(new object[] {
            "50%",
            "60%",
            "70%",
            "80%",
            "90%",
            "100%",
            "110%",
            "120%",
            "130%",
            "140%",
            "150%",
            "160%",
            "170%",
            "180%",
            "190%",
            "200%"});
            this.cmbDefaultZoom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDefaultZoom.Size = new System.Drawing.Size(100, 20);
            this.cmbDefaultZoom.TabIndex = 11;
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl9.Location = new System.Drawing.Point(3, 214);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(171, 13);
            this.labelControl9.TabIndex = 14;
            this.labelControl9.Text = "Preferred Audio Device:";
            // 
            // cmbAudioDevice
            // 
            this.cmbAudioDevice.Location = new System.Drawing.Point(180, 211);
            this.cmbAudioDevice.Name = "cmbAudioDevice";
            this.cmbAudioDevice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAudioDevice.Properties.Items.AddRange(new object[] {
            "200%",
            "400%",
            "800%",
            "1600%"});
            this.cmbAudioDevice.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbAudioDevice.Size = new System.Drawing.Size(221, 20);
            this.cmbAudioDevice.TabIndex = 15;
            // 
            // PreferencesTab
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cmbAudioDevice);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.cmbDefaultZoom);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtKeyboardBounce);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtFootPedalBounce);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmbAudioSpeed);
            this.Name = "PreferencesTab";
            this.Size = new System.Drawing.Size(537, 352);
            this.Load += new System.EventHandler(this.PreferencesTab_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbAudioSpeed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFootPedalBounce.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKeyboardBounce.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDefaultZoom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAudioDevice.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cmbAudioSpeed;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtFootPedalBounce;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtKeyboardBounce;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDefaultZoom;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.ComboBoxEdit cmbAudioDevice;
    }
}
