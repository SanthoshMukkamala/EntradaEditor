namespace Entrada.Editor
{
    partial class AutoCompleteTextBox
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
			this.listBox1 = new DevExpress.XtraEditors.ListBoxControl();
			this.textBox2 = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.listBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textBox2.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.ItemHeight = 13;
			this.listBox1.Location = new System.Drawing.Point(0, 19);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(391, 4);
			this.listBox1.TabIndex = 26;
			this.listBox1.Visible = false;
			// 
			// textBox2
			// 
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBox2.Location = new System.Drawing.Point(0, 0);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(391, 20);
			this.textBox2.TabIndex = 25;
			// 
			// AutoCompleteTextBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.textBox2);
			this.Name = "AutoCompleteTextBox";
			this.Size = new System.Drawing.Size(391, 20);
			((System.ComponentModel.ISupportInitialize)(this.listBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textBox2.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl listBox1;
        private DevExpress.XtraEditors.TextEdit textBox2;
    }
}
