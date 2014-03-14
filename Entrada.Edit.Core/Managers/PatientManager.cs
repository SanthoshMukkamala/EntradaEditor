using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entrada.Editor.Data;
using System.Threading.Tasks;

namespace Entrada.Editor.Core
{
	public class PatientManager
	{
		public async Task<IEnumerable<string>> Search (DocumentEntity doc, string text)
		{
            using (EditorCore.CreateStopwatch ("PatientSearch"))
                return await AdtRepository.SearchPatients (doc.Job.Encounter.Clinic.Id, text);
		}

		public Task<List<AdtRepository.PatientEncounterView>> GetAppointments (string mrn, Entrada.Entities.MedicalJobEntity job)
		{
            return AdtRepository.GetPatientEncounters (job.Encounter.Clinic.Id, mrn);
		}

        public Task<Entrada.Entities.PatientEntity> GetPatient (DocumentEntity doc, string mrn)
        {
            return AdtRepository.GetPatient (doc.Job.Encounter.Clinic.Id, mrn);
        }
	}
}
