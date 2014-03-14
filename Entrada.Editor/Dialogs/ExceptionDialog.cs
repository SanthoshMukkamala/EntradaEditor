using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Entrada.Editor.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Entrada.Editor
{
	public partial class ExceptionDialog : XtraForm
	{
        private string text;
        private Exception exception;
        private bool can_retry;

		public ExceptionDialog ()
		{
			InitializeComponent ();
		}

		public ExceptionDialog (string text, Exception ex, bool canRetry) : this ()
		{
            this.text = text;
            exception = ex;
            can_retry = canRetry;

            if (!string.IsNullOrWhiteSpace (text))
                labelControl1.Text = text;

            if (!can_retry) {
                btnCancel.Visible = false;
                btnRetry.Text = "OK";
            }

            memoEdit1.Text = exception.ToString ();
		}

		private void btnCancel_Click (object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void btnMoveText_Click (object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.OK;
		}

        private void ExceptionDialog_Load (object sender, EventArgs e)
		{
		}
	}
}
