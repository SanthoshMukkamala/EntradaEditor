using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Entrada.Editor
{
    public partial class JobCleanupDialog : DevExpress.XtraEditors.XtraForm
    {
        public List<ConfirmationJob> JobList { get; private set; }
        public Func<string, Task> Option1Action { get; set; }
        public Func<string, Task> Option2Action { get; set; }

        public JobCleanupDialog ()
        {
            InitializeComponent ();

            labelControl1.Text = "The following unfinished jobs are currently assigned to you from the job pool.  Would you like to Hold them or Release them?";
        }

        public JobCleanupDialog (IEnumerable<string> jobs) : this ()
        {
            JobList = jobs.Select (p => new ConfirmationJob () { JobNumber = p }).ToList ();
            gridControl1.DataSource = JobList;
        }

        private void CloseConfirmationDialog_Load (object sender, EventArgs e)
        {
            // We want our options to commit on click, not on cell leave
            repositoryItemCheckEdit1.EditValueChanged += (o, _) => { gridControl1.DefaultView.PostEditor (); };
        }

        public string LabelText {
            get { return labelControl1.Text; }
            set { labelControl1.Text = value; }
        }

        public string Option1Text {
            get { return gridColumn2.Caption; }
            set { gridColumn2.Caption = value; }
        }

        public string Option2Text {
            get { return gridColumn3.Caption; }
            set { gridColumn3.Caption = value; }
        }

        public bool AllowCancel {
            get { return btnCancel.Enabled; }
            set { btnCancel.Enabled = btnCancel.Visible = value; }
        }

        public string ContinueButtonText {
            get { return btnAccept.Text; }
            set { btnAccept.Text = value; }
        }

        public class ConfirmationJob
        {
            private bool option;

            public string JobNumber { get; set; }

            public bool Option1 { get { return option; } set { option = true; } }
            public bool Option2 { get { return !option; } set { option = !true; } }
        }

        private async void btnAccept_Click (object sender, EventArgs e)
        {
            foreach (var job in JobList) {
                if (job.Option1)
                    await Option1Action (job.JobNumber.Split (' ')[0]);
                else
                    await Option2Action (job.JobNumber.Split (' ')[0]);
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}