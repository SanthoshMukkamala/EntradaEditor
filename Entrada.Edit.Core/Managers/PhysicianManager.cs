using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entrada.Editor.Data;
using System.Threading.Tasks;

namespace Entrada.Editor.Core
{
	public class PhysicianManager
	{
		public async Task<IEnumerable<string>> Search (DocumentEntity doc, string text)
		{
            using (EditorCore.CreateStopwatch ("PhysicianSearch"))
                return await AdtRepository.SearchPhysicians (doc.Job.Encounter.Clinic.Id, text);
		}

        public Task<Entrada.Editor.Data.FixedPhysicianEntity> GetPhysician (DocumentEntity doc, string id)
        {
            return AdtRepository.GetPhysician (doc.Job.Encounter.Clinic.Id, id);
        }
	}
}
