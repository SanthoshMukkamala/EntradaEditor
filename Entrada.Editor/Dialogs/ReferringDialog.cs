using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entrada.Editor.Core;

namespace Entrada.Editor
{
	public partial class ReferringDialog : DevExpress.XtraEditors.XtraForm
	{
		private Entrada.Entities.MedicalJobEntity job;
        private DocumentEntity document;
		private string initial_term;

        public Entrada.Editor.Data.FixedPhysicianEntity SelectedPhysician { get; private set; }

		public ReferringDialog ()
		{
			InitializeComponent ();

			textEdit1.SetSuggestionFunction (GetSuggestions);
            textEdit1.SetNotFoundLabel (lblNotFound);
			textEdit1.ItemSelected += textEdit1_ItemSelected;

            document = EditorCore.Documents.ActiveDocument;
            job = document.Job;
		}

        public ReferringDialog (string mrn) : this ()
		{
			if (string.IsNullOrWhiteSpace (mrn))
				return;

            initial_term = mrn;
		}

        public string AcceptButtonText {
            get { return btnAccept.Text; }
            set { btnAccept.Text = value; }
        }

		async void textEdit1_ItemSelected (object sender, EventArgs e)
		{
			var id = textEdit1.Text.Substring (0, textEdit1.Text.IndexOf (' '));
			var phys = await EditorCore.Physicians.GetPhysician (document, id);
            
            if (phys == null)
                return;

			SelectedPhysician = phys;
            btnAccept.Enabled = true;

            memoEdit1.Text = phys.ToFullFormat ();
		}

		private Task<IEnumerable<string>> GetSuggestions (string text)
		{
			return EditorCore.Physicians.Search (document, text);
		}

		private void btnCancel_Click (object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void btnAccept_Click (object sender, EventArgs e)
		{
            Clipboard.Clear ();

            if (!string.IsNullOrWhiteSpace (memoEdit1.Text))
                Clipboard.SetText (memoEdit1.Text);

            DialogResult = DialogResult.OK;
		}

		private async void PatientDialog_Load (object sender, EventArgs e)
		{
			textEdit1.Focus ();
			textEdit1.Select ();

			if (initial_term != null) {
                var results = await EditorCore.Physicians.Search (document, initial_term);

                if (results.Count () == 0)
                    return;

                var initial_phys = results.First ();
                
                textEdit1.Text = initial_phys;
				textEdit1_ItemSelected (null, EventArgs.Empty);
			}
		}
	}
}