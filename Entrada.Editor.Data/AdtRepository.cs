using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entrada.Entities;
using Newtonsoft.Json;

namespace Entrada.Editor.Data
{
    public static class AdtRepository
    {
        public static Task<string[]> GetAvailableJobTypes (int clinicID)
        {
            return Task.Factory.StartNew (() => {
                var ws = Settings.CreateAdtService ();

                return ws.SearchJobTypes (clinicID);
            });
        }

        public static Task<List<string>> SearchPatients (int clinicID, string term)
        {
            return Task.Factory.StartNew (() => {
                var ws = Settings.CreateAdtService ();
                var search = BuildSearch (term, true);

                var pats = ws.SearchPatientsCompact (clinicID, search);
                var list = JsonConvert.DeserializeObject<PatientJson[]> (pats);

                if (list == null)
                    return new List<string> ();

                // This is the updated version that uses the pipe separator [T716]
                if (list.Length > 0 && list[0].PatName.Contains ('|'))
                    return list.Where (p => p.PatName != null).Select (p => p.PatName.Replace ("|", " - ")).ToList ();

                // This is the old web service that uses a hyphen
                return list.Where (p => p.PatName != null).Select (p => p.PatName.Replace ("-", " - ")).ToList ();
            });
        }

        private static string BuildSearch (string term, bool isPatient)
        {
            var phrases = new List<string> ();

            foreach (var t in term.Split (new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)) {
                if (isPatient)
                    phrases.Add (string.Format ("(FIRSTNAME.Contains(\"{0}\") OR LASTNAME.Contains(\"{0}\") OR MRN.Contains(\"{0}\"))", t));
                else
                    phrases.Add (string.Format ("(FIRSTNAME.Contains(\"{0}\") OR LASTNAME.Contains(\"{0}\") OR PhysicianID.Contains(\"{0}\"))", t));
            }

            return string.Join (" AND ", phrases);
        }

        public static Task<PatientEntity> GetPatient (int clinicID, string mrn)
        {
            return Task.Factory.StartNew (() => {
                var ws = Settings.CreateAdtService ();
                var search = string.Format ("MRN = \"{0}\"", mrn);

                var pat = ws.SearchPatients (clinicID, search);
                var pat_ent = JsonConvert.DeserializeObject<PatientEntity[]> (pat);

                return pat_ent.FirstOrDefault ();
            });
        }

        public static Task<List<PatientEncounterView>> GetPatientEncounters (int clinicID, string mrn)
        {
            return Task.Factory.StartNew (() => {
                var ws = Settings.CreateAdtService ();
                var search = string.Format ("MRN = \"{0}\"", mrn);

                var pat = ws.SearchSchedule (clinicID, DateTime.MinValue, DateTime.MinValue, search);
                var pat_ent = JsonConvert.DeserializeObject<DataTable> (pat);

                return TableToView (pat_ent);
            });
        }

        private class PatientJson
        {
            public string PatName { get; set; }
        }

        private class PhysicianJson
        {
            public string PhysName { get; set; }
        }

        public static Task<List<string>> SearchPhysicians (int clinicID, string term)
        {
            return Task.Factory.StartNew (() => {
                var ws = Settings.CreateAdtService ();
                var search = BuildSearch (term, false);

                var pats = ws.SearchPhysiciansCompact (clinicID, search);
                var list = JsonConvert.DeserializeObject<PhysicianJson[]> (pats).Where (p => !string.IsNullOrWhiteSpace (p.PhysName)).ToArray ();

                // This is the updated version that uses the pipe separator [T716]
                if (list.Length > 0 && list[0].PhysName.Contains ('|'))
                    return list.Select (p => p.PhysName.Replace ("|", " - ")).ToList ();

                // This is the old web service that uses a hyphen
                return list.Select (p => p.PhysName.Replace ("-", " - ")).ToList ();
            });
        }

        public static Task<FixedPhysicianEntity> GetPhysician (int clinicID, string id)
        {
            return Task.Factory.StartNew (() => {
                var ws = Settings.CreateAdtService ();
                var search = string.Format ("PhysicianID = \"{0}\"", id);

                var phys = ws.SearchPhysicians (clinicID, search);
                var phys_ent = JsonConvert.DeserializeObject<FixedPhysicianEntity[]> (phys);

                return phys_ent.FirstOrDefault ();
            });
        }

        private static List<PatientEncounterView> TableToView (DataTable dt)
        {
            var list = new List<PatientEncounterView> ();

            foreach (DataRow row in dt.Rows) {
                list.Add (new PatientEncounterView () {
                    PhysicianFirstName = IsNull (row["PhysicianFirstName"]),
                    PhysicianLastName = IsNull (row["PhysicianLastName"]),
                    AppointmentDate = RowToDate (row["ApptDate"]),
                    AppointmentTime = RowToSpan (row["ApptTime"])
                });
            }

            return list;
        }

        private static TimeSpan? RowToSpan (object time)
        {
            if (time == DBNull.Value)
                return null;

            if (time is DateTime)
                return ((DateTime)time).TimeOfDay;

            if (time is string) {
                DateTime result;

                if (DateTime.TryParse ((string)time, out result))
                    return result.TimeOfDay;
            }

            return (TimeSpan)time;
        }

        private static DateTime? RowToDate (object date)
        {
            if (date == DBNull.Value)
                return null;

            if (date is DateTime?)
                return (DateTime)date;

            if (date is DateTime)
                return (DateTime)date;

            if (date is string) {
                DateTime result;

                if (DateTime.TryParse ((string)date, out result))
                    return result;
            }

            return null;
        }

        private static string IsNull (object o)
        {
            if (o == null)
                return string.Empty;

            if (o == DBNull.Value)
                return string.Empty;

            return o.ToString ();
        }

        public class PatientEncounterView
        {
            public string PhysicianFirstName { get; set; }
            public string PhysicianLastName { get; set; }
            public DateTime? AppointmentDate { get; set; }
            public TimeSpan? AppointmentTime { get; set; }
        }
    }
}
