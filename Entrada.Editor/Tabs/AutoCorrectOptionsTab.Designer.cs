namespace Entrada.Editor
{
    partial class AutoCorrectOptionsTab
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.chkTwoCaps = new DevExpress.XtraEditors.CheckEdit();
            this.chkSpellCheck = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkTwoCaps.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSpellCheck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.LineLocation = DevExpress.XtraEditors.LineLocation.Bottom;
            this.labelControl2.LineVisible = true;
            this.labelControl2.Location = new System.Drawing.Point(9, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.ShowLineShadow = false;
            this.labelControl2.Size = new System.Drawing.Size(452, 29);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "AutoCorrect Options";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(426, 387);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkTwoCaps
            // 
            this.chkTwoCaps.Location = new System.Drawing.Point(24, 38);
            this.chkTwoCaps.Name = "chkTwoCaps";
            this.chkTwoCaps.Properties.Caption = "Correct TWo INitial CApitals";
            this.chkTwoCaps.Size = new System.Drawing.Size(183, 19);
            this.chkTwoCaps.TabIndex = 11;
            this.chkTwoCaps.CheckedChanged += new System.EventHandler(this.chkTwoCaps_CheckedChanged);
            // 
            // chkSpellCheck
            // 
            this.chkSpellCheck.Location = new System.Drawing.Point(24, 58);
            this.chkSpellCheck.Name = "chkSpellCheck";
            this.chkSpellCheck.Properties.Caption = "Automatically use suggestions from spell check";
            this.chkSpellCheck.Size = new System.Drawing.Size(264, 19);
            this.chkSpellCheck.TabIndex = 12;
            this.chkSpellCheck.CheckedChanged += new System.EventHandler(this.chkSpellCheck_CheckedChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(26, 99);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(125, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Replace text as you type:";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(26, 118);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(379, 167);
            this.gridControl1.TabIndex = 18;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(330, 291);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // AutoCorrectOptionsTab
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.chkSpellCheck);
            this.Controls.Add(this.chkTwoCaps);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl2);
            this.Name = "AutoCorrectOptionsTab";
            this.Size = new System.Drawing.Size(537, 427);
            this.Load += new System.EventHandler(this.PreferencesTab_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkTwoCaps.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSpellCheck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.CheckEdit chkTwoCaps;
        private DevExpress.XtraEditors.CheckEdit chkSpellCheck;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
    }
}
