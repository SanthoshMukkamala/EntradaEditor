using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading.Tasks;
using Entrada.Editor.Core;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Entrada.Editor
{
	public partial class PatientDialog : DevExpress.XtraEditors.XtraForm
	{
		private Entrada.Entities.MedicalJobEntity job;
        private DocumentEntity document;
		private string initial_term;

        public Entrada.Entities.PatientEntity SelectedPatient { get; private set; }
        public bool HasSelectedAppointment { get; private set; }
        public DateTime SelectedDate { get; private set; }
        public TimeSpan? SelectedTime { get; private set; }

		public PatientDialog ()
		{
			InitializeComponent ();

			textEdit1.SetSuggestionFunction (GetSuggestions);
            textEdit1.SetNotFoundLabel (lblNotFound);
			textEdit1.ItemSelected += textEdit1_ItemSelected;

            document = EditorCore.Documents.ActiveDocument;
            job = document.Job;
		}

		public PatientDialog (string mrn) : this ()
		{
			if (string.IsNullOrWhiteSpace (mrn) || mrn.Length < 3)
				return;

            initial_term = mrn;
		}

        public string AcceptButtonText {
            get { return btnAccept.Text; }
            set { btnAccept.Text = value; }
        }

        public bool AllowGenericSplit {
            get { return btnGenericPatient.Visible; }
            set { btnGenericPatient.Visible = value; }
        }

		async void textEdit1_ItemSelected (object sender, EventArgs e)
		{
			var mrn = textEdit1.Text.Substring (0, textEdit1.Text.IndexOf (' '));
			var pat = await EditorCore.Patients.GetPatient (document, mrn);
			var appts = await EditorCore.Patients.GetAppointments (mrn, job);
            
			SelectedPatient = pat;
            btnAccept.Enabled = true;

            if (appts == null || appts.Count == 0) {
                gridControl1.Enabled = false;
                lblAppointmentsFound.Visible = true;
                gridControl1.DataSource = null;
                return;
            }

            var list = appts.OrderByDescending (p => p.AppointmentDate).ToList ();

            // Insert a generic appointment in case none match
            if (list.Count > 0) {
                list.Insert (0, new Data.AdtRepository.PatientEncounterView () {
                    PhysicianFirstName = "New Appointment"
                });
            }

            gridControl1.DataSource = list;
            gridControl1.Enabled = list.Count > 0;
            lblAppointmentsFound.Visible = list.Count == 0;

			var index = list.IndexOf (appts.Where (p => p.AppointmentDate == job.Encounter.Date).FirstOrDefault ());

            if (gridControl1.Views.Count > 0) {
                var col = gridControl1.Views[0] as ColumnView;
                col.FocusedRowHandle = index;
            }

			gridControl1.Focus ();
			gridControl1.Select ();
		}

		private Task<IEnumerable<string>> GetSuggestions (string text)
		{
			return EditorCore.Patients.Search (document, text);
		}

		private void btnCancel_Click (object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void btnAccept_Click (object sender, EventArgs e)
		{
            var view = gridControl1.DefaultView as GridView;

			if (view.SelectedRowsCount > 0) {
                var appt = (Data.AdtRepository.PatientEncounterView)view.GetRow (view.GetSelectedRows ()[0]);

                // If they chose a generic appointment, don't use it
                if (appt.PhysicianFirstName != "New Appointment") {
                    HasSelectedAppointment = true;

                    SelectedDate = appt.AppointmentDate.Value;
                    SelectedTime = appt.AppointmentTime;
                }
            }

			DialogResult = DialogResult.OK;
		}

		private async void PatientDialog_Load (object sender, EventArgs e)
		{
			textEdit1.Focus ();
			textEdit1.Select ();

			if (initial_term != null) {
                var results = await EditorCore.Patients.Search (document, initial_term);

                if (results.Count () == 0)
                    return;

                var initial_pat = results.First ();
                
                textEdit1.Text = initial_pat.ToString ();
				textEdit1_ItemSelected (null, EventArgs.Empty);
			}
		}

        private void btnGenericPatient_Click (object sender, EventArgs e)
        {
            SelectedPatient = new Entities.PatientEntity () {
                MRN = "999999",
                FirstName = "GENERIC",
                LastName = "PATIENT",
                IsGeneric = true
            };

            DialogResult = DialogResult.OK;
        }
	}
}