using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Entrada.Editor.Core;

namespace Entrada.Editor
{
    public partial class QANote : DevExpress.XtraEditors.XtraForm
    {
        private DocumentEntity document;

        public QANote(DocumentEntity doc)
            : this()
        {
            this.document = doc;
            memoeditNote.Focus();
            memoeditNote.Text = document.QANote;            
        }

        public QANote()
        {
            InitializeComponent();
            //memoeditNote.Text = document.QANote;
        }

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            if (memoeditNote.Text.TrimStart().TrimEnd().Trim() != String.Empty)
            {
                DialogResult = DialogResult.OK;                
                document.QANote = memoeditNote.Text;
                //document.Job.QAData.LastQANote = memoeditNote.Text;
                Close();
            }
            else
            {
                MessageBox.Show("Note can't be empty. Please type the note", "QA Note", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                memoeditNote.Focus();
                DialogResult = DialogResult.None;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (memoeditNote.Text != "")
            {
                string sMsg = "Are you sure you want to replace the existing note with the incoming note from the Job?";
                if (MessageBox.Show(sMsg, "QA Note", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    memoeditNote.Text = document.Job.QAData.LastQANote;
            }
            else
            {
                memoeditNote.Text = document.Job.QAData.LastQANote;
            }
            DialogResult = DialogResult.None;
        }
    }
}