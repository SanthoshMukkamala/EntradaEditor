using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Entrada.Editor.Core;

namespace Entrada.Editor
{
	public partial class SplitByJobTypeDialog : XtraForm
	{
        private bool has_adt;

        public CopyMove CopyOrMove { get; private set; }

		public SplitByJobTypeDialog ()
		{
			InitializeComponent ();
		}

		public SplitByJobTypeDialog (DocumentEntity doc) : this ()
		{
            has_adt = doc.Job.Encounter.Clinic.HasADT;

            if (has_adt) {
                listBoxControl1.Items.AddRange (doc.AvailableJobTypes.OrderBy (p => p).ToArray ());
            } else {
                listBoxControl1.Visible = false;
                txtJobType.Visible = true;

                txtJobType.Text = doc.Job.JobType;
            }
		}

        public string SelectedJobType {
            get {
                if (has_adt) {
                    if (listBoxControl1.SelectedIndex < 0)
                        return null;

                    return listBoxControl1.SelectedItem.ToString ();
                }

                return txtJobType.Text;
            }
        }

		private void btnCancel_Click (object sender, EventArgs e)
		{
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void btnMoveText_Click (object sender, EventArgs e)
		{
            if (!has_adt && string.IsNullOrWhiteSpace (txtJobType.Text)) {
                MessageBox.Show ("Please specify a job type.");
                return;
            }

            CopyOrMove = SplitByJobTypeDialog.CopyMove.Move;
			DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void btnCopyText_Click (object sender, EventArgs e)
		{
            if (!has_adt && string.IsNullOrWhiteSpace (txtJobType.Text)) {
                MessageBox.Show ("Please specify a job type.");
                return;
            }

            CopyOrMove = SplitByJobTypeDialog.CopyMove.Copy;
            DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void SplitByJobTypeDialog_Load (object sender, EventArgs e)
		{
            if (has_adt) {
			    if (listBoxControl1.Items.Count > 0)
				    listBoxControl1.SelectedIndex = 0;

			    listBoxControl1.Focus ();
			    listBoxControl1.Select ();
            } else {
                txtJobType.Focus ();
                txtJobType.Select ();
                txtJobType.SelectAll ();
            }
		}

        public enum CopyMove
        {
            Copy,
            Move
        }
	}
}
