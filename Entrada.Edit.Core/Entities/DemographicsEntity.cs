using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entrada.Entities;

namespace Entrada.Editor.Core
{
    public class DemographicsEntity
    {
        [Browsable (false)]
        public Dictionary<string, string> Raw { get; private set; }
        [Browsable (false)]
        public Dictionary<string, string> CustomFieldDescriptions { get; private set; }
        [Browsable (false)]
        public string TEXT_BODY_VR { get; set; }

        [Category ("Job")]
        [DisplayName ("Dictator ID")]
        public string JOBS_DICTATORID { get { return Raw["JOBS_DICTATORID"]; } private set { Raw["JOBS_DICTATORID"] = value; } }
        [Category ("Job")]
        [DisplayName ("Clinic ID")]
        public string JOBS_CLINICID { get { return Raw["JOBS_CLINICID"]; } private set { Raw["JOBS_CLINICID"] = value; } }
        [Category ("Job")]
        [DisplayName ("Clinic Name")]
        public string JOBS_CLINICNAME { get { return Raw["JOBS_CLINICNAME"]; } private set { Raw["JOBS_CLINICNAME"] = value; } }
        [Category ("Job")]
        [DisplayName ("Location")]
        public string JOBS_LOCATION { get { return Raw["JOBS_LOCATION"]; } private set { Raw["JOBS_LOCATION"] = value; } }
        [Category ("Job")]
        [DisplayName ("Location Name")]
        public string JOBS_LOCATIONNAME { get { return Raw["JOBS_LOCATIONNAME"]; } private set { Raw["JOBS_LOCATIONNAME"] = value; } }
        [Category ("Job")]
        [DisplayName ("Appt Date")]
        public string JOBS_APPOINTMENTDATE { get { return Raw["JOBS_APPOINTMENTDATE"]; } set { var date = DateTime.Parse (value); Raw["JOBS_APPOINTMENTDATE"] = date.ToShortDateString (); } }
        [Category ("Job")]
        [DisplayName ("Appt Time")]
        public string JOBS_APPOINTMENTTIME { get { return Raw["JOBS_APPOINTMENTTIME"]; } set { if (string.IsNullOrEmpty (value)) { Raw["JOBS_APPOINTMENTTIME"] = value; return; } var time = DateTime.Parse (value); Raw["JOBS_APPOINTMENTTIME"] = time.ToShortTimeString (); } }
        [Category ("Job")]
        [DisplayName ("Job Type")]
        public string JOBS_JOBTYPE { get { return Raw["JOBS_JOBTYPE"]; } set { Raw["JOBS_JOBTYPE"] = value; } }
        [Category ("Job")]
        [DisplayName ("Stat")]
        public bool JOBS_STAT { get { return bool.Parse (Raw["JOBS_STAT"]); } private set { Raw["JOBS_STAT"] = value.ToString (); } }
        [Category ("Job")]
        [DisplayName ("CC")]
        public bool JOBS_CC { get { return bool.Parse (Raw["JOBS_CC"]); } set { Raw["JOBS_CC"] = value.ToString (); } }
        [Category ("Job")]
        [DisplayName ("Duration")]
        public string JOBS_DURATION { get { return Raw["JOBS_DURATION"]; } private set { Raw["JOBS_DURATION"] = value; } }
        [Category ("Job")]
        [DisplayName ("Dictation Date")]
        public string JOBS_DICTATIONDATE { get { return Raw["JOBS_DICTATIONDATE"]; } private set { Raw["JOBS_DICTATIONDATE"] = value; } }
        [Category ("Job")]
        [DisplayName ("Dictation Time")]
        public string JOBS_DICTATIONTIME { get { return Raw["JOBS_DICTATIONTIME"]; } private set { Raw["JOBS_DICTATIONTIME"] = value; } }
        [Category ("Dictator")]
        [DisplayName ("First Name")]
        public string DICTATOR_FIRSTNAME { get { return Raw["DICTATOR_FIRSTNAME"]; } private set { Raw["DICTATOR_FIRSTNAME"] = value; } }
        [Category ("Dictator")]
        [DisplayName ("Middle Initial")]
        public string DICTATOR_MI { get { return Raw["DICTATOR_MI"]; } private set { Raw["DICTATOR_MI"] = value; } }
        [Category ("Dictator")]
        [DisplayName ("Last Name")]
        public string DICTATOR_LASTNAME { get { return Raw["DICTATOR_LASTNAME"]; } private set { Raw["DICTATOR_LASTNAME"] = value; } }
        [Category ("Dictator")]
        [DisplayName ("Suffix")]
        public string DICTATOR_SUFFIX { get { return Raw["DICTATOR_SUFFIX"]; } private set { Raw["DICTATOR_SUFFIX"] = value; } }
        [Category ("Dictator")]
        [DisplayName ("Initials")]
        public string DICTATOR_INITIALS { get { return Raw["DICTATOR_INITIALS"]; } private set { Raw["DICTATOR_INITIALS"] = value; } }
        [Category ("Dictator")]
        [DisplayName ("Signature")]
        public string DICTATOR_SIGNATURE { get { return Raw["DICTATOR_SIGNATURE"]; } private set { Raw["DICTATOR_SIGNATURE"] = value; } }
        [Category ("Dictator")]
        [DisplayName ("User Code")]
        public string DICTATOR_USER_CODE { get { return Raw["DICTATOR_USER_CODE"]; } private set { Raw["DICTATOR_USER_CODE"] = value; } }
        [Category ("Editor")]
        [DisplayName ("Signoff 1")]
        [Browsable (false)]
        public string EDITOR_SIGNOFF1 { get { return Raw["EDITOR_SIGNOFF1"]; } set { Raw["EDITOR_SIGNOFF1"] = value; } }
        [Category ("Editor")]
        [DisplayName ("Signoff 2")]
        [Browsable (false)]
        public string EDITOR_SIGNOFF2 { get { return Raw["EDITOR_SIGNOFF2"]; } set { Raw["EDITOR_SIGNOFF2"] = value; } }
        [Category ("Editor")]
        [DisplayName ("Signoff 3")]
        [Browsable (false)]
        public string EDITOR_SIGNOFF3 { get { return Raw["EDITOR_SIGNOFF3"]; } set { Raw["EDITOR_SIGNOFF3"] = value; } }
        [Category ("Patient")]
        [DisplayName ("Alternate ID")]
        public string PATIENT_ALTERNATEID { get { return Raw["PATIENT_ALTERNATEID"]; } set { Raw["PATIENT_ALTERNATEID"] = value; } }
        [Category ("Patient")]
        [DisplayName ("MRN")]
        public string PATIENT_MRN { get { return Raw["PATIENT_MRN"]; } set { Raw["PATIENT_MRN"] = value; } }
        [Category ("Patient")]
        [DisplayName ("First Name")]
        public string PATIENT_FIRSTNAME { get { return Raw["PATIENT_FIRSTNAME"]; } set { Raw["PATIENT_FIRSTNAME"] = value; } }
        [Category ("Patient")]
        [DisplayName ("Middle Initial")]
        public string PATIENT_MI { get { return Raw["PATIENT_MI"]; } set { Raw["PATIENT_MI"] = value; } }
        [Category ("Patient")]
        [DisplayName ("Last Name")]
        public string PATIENT_LASTNAME { get { return Raw["PATIENT_LASTNAME"]; } set { Raw["PATIENT_LASTNAME"] = value; } }
        [Category ("Patient")]
        [DisplayName ("Suffix")]
        public string PATIENT_SUFFIX { get { return Raw["PATIENT_SUFFIX"]; } set { Raw["PATIENT_SUFFIX"] = value; } }
        [Category ("Patient")]
        [DisplayName ("DOB")]
        public string PATIENT_DOB { get { return Raw["PATIENT_DOB"]; } set { var date = DateTime.Parse (value); Raw["PATIENT_DOB"] = date.ToShortDateString (); } }
        [Category ("Patient")]
        [DisplayName ("SSN")]
        [Browsable (false)]
        public string PATIENT_SSN { get { return Raw["PATIENT_SSN"]; } set { Raw["PATIENT_SSN"] = value; } }
        [Category ("Patient")]
        [DisplayName ("Address 1")]
        public string PATIENT_ADDRESS1 { get { return Raw["PATIENT_ADDRESS1"]; } set { Raw["PATIENT_ADDRESS1"] = value; } }
        [Category ("Patient")]
        [DisplayName ("Address 2")]
        public string PATIENT_ADDRESS2 { get { return Raw["PATIENT_ADDRESS2"]; } set { Raw["PATIENT_ADDRESS2"] = value; } }
        [Category ("Patient")]
        [DisplayName ("City")]
        public string PATIENT_CITY { get { return Raw["PATIENT_CITY"]; } set { Raw["PATIENT_CITY"] = value; } }
        [Category ("Patient")]
        [DisplayName ("State")]
        public string PATIENT_STATE { get { return Raw["PATIENT_STATE"]; } set { Raw["PATIENT_STATE"] = value; } }
        [Category ("Patient")]
        [DisplayName ("Zip")]
        public string PATIENT_ZIP { get { return Raw["PATIENT_ZIP"]; } set { Raw["PATIENT_ZIP"] = value; } }
        [Category ("Patient")]
        [DisplayName ("Phone")]
        public string PATIENT_PHONE { get { return Raw["PATIENT_PHONE"]; } set { Raw["PATIENT_PHONE"] = value; } }
        [Category ("Patient")]
        [DisplayName ("Gender")]
        public string PATIENT_SEX { get { return Raw["PATIENT_SEX"]; } set { Raw["PATIENT_SEX"] = value; } }
        [Category ("Patient")]
        [DisplayName ("Patient ID")]
        [Browsable (false)]
        public string PATIENT_PATIENTID { get { return Raw["PATIENT_PATIENTID"]; } set { Raw["PATIENT_PATIENTID"] = value; } }
        [Category ("Patient")]
        [DisplayName ("Appointment ID")]
        [Browsable (false)]
        public string PATIENT_APPOINTMENTID { get { return Raw["PATIENT_APPOINTMENTID"]; } set { Raw["PATIENT_APPOINTMENTID"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("Physician ID")]
        public string REFERRING_PHYSICIANID { get { return Raw["REFERRING_PHYSICIANID"]; } set { Raw["REFERRING_PHYSICIANID"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("First Name")]
        public string REFERRING_FIRSTNAME { get { return Raw["REFERRING_FIRSTNAME"]; } set { Raw["REFERRING_FIRSTNAME"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("Middle Initial")]
        public string REFERRING_MI { get { return Raw["REFERRING_MI"]; } set { Raw["REFERRING_MI"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("Last Name")]
        public string REFERRING_LASTNAME { get { return Raw["REFERRING_LASTNAME"]; } set { Raw["REFERRING_LASTNAME"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("Suffix")]
        public string REFERRING_SUFFIX { get { return Raw["REFERRING_SUFFIX"]; } set { Raw["REFERRING_SUFFIX"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("DOB")]
        public string REFERRING_DOB { get { return Raw["REFERRING_DOB"]; }
            set {
                if (string.IsNullOrWhiteSpace (value)) {
                    Raw["REFERRING_DOB"] = string.Empty;
                    return;
                }

                var date = DateTime.Parse (value);
                Raw["REFERRING_DOB"] = date.ToShortDateString ();
            }
        }
        [Category ("Referring Physician")]
        [DisplayName ("SSN")]
        [Browsable (false)]
        public string REFERRING_SSN { get { return Raw["REFERRING_SSN"]; } set { Raw["REFERRING_SSN"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("Address 1")]
        public string REFERRING_ADDRESS1 { get { return Raw["REFERRING_ADDRESS1"]; } set { Raw["REFERRING_ADDRESS1"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("Address 2")]
        public string REFERRING_ADDRESS2 { get { return Raw["REFERRING_ADDRESS2"]; } set { Raw["REFERRING_ADDRESS2"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("City")]
        public string REFERRING_CITY { get { return Raw["REFERRING_CITY"]; } set { Raw["REFERRING_CITY"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("State")]
        public string REFERRING_STATE { get { return Raw["REFERRING_STATE"]; } set { Raw["REFERRING_STATE"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("Zip")]
        public string REFERRING_ZIP { get { return Raw["REFERRING_ZIP"]; } set { Raw["REFERRING_ZIP"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("Phone")]
        public string REFERRING_PHONE { get { return Raw["REFERRING_PHONE"]; } set { Raw["REFERRING_PHONE"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("Gender")]
        public string REFERRING_SEX { get { return Raw["REFERRING_SEX"]; } set { Raw["REFERRING_SEX"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("Fax")]
        public string REFERRING_FAX { get { return Raw["REFERRING_FAX"]; } set { Raw["REFERRING_FAX"] = value; } }
        [Category ("Referring Physician")]
        [DisplayName ("Clinic Name")]
        public string REFERRING_CLINICNAME { get { return Raw["REFERRING_CLINICNAME"]; } set { Raw["REFERRING_CLINICNAME"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom1")]
        public string CUSTOM_CUSTOM1 { get { return Raw["CUSTOM_CUSTOM1"]; } set { Raw["CUSTOM_CUSTOM1"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom2")]
        public string CUSTOM_CUSTOM2 { get { return Raw["CUSTOM_CUSTOM2"]; } set { Raw["CUSTOM_CUSTOM2"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom3")]
        public string CUSTOM_CUSTOM3 { get { return Raw["CUSTOM_CUSTOM3"]; } set { Raw["CUSTOM_CUSTOM3"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom4")]
        public string CUSTOM_CUSTOM4 { get { return Raw["CUSTOM_CUSTOM4"]; } set { Raw["CUSTOM_CUSTOM4"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom5")]
        public string CUSTOM_CUSTOM5 { get { return Raw["CUSTOM_CUSTOM5"]; } set { Raw["CUSTOM_CUSTOM5"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom6")]
        public string CUSTOM_CUSTOM6 { get { return Raw["CUSTOM_CUSTOM6"]; } set { Raw["CUSTOM_CUSTOM6"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom7")]
        public string CUSTOM_CUSTOM7 { get { return Raw["CUSTOM_CUSTOM7"]; } set { Raw["CUSTOM_CUSTOM7"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom8")]
        public string CUSTOM_CUSTOM8 { get { return Raw["CUSTOM_CUSTOM8"]; } set { Raw["CUSTOM_CUSTOM8"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom9")]
        public string CUSTOM_CUSTOM9 { get { return Raw["CUSTOM_CUSTOM9"]; } set { Raw["CUSTOM_CUSTOM9"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom10")]
        public string CUSTOM_CUSTOM10 { get { return Raw["CUSTOM_CUSTOM10"]; } set { Raw["CUSTOM_CUSTOM10"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom11")]
        public string CUSTOM_CUSTOM11 { get { return Raw["CUSTOM_CUSTOM11"]; } set { Raw["CUSTOM_CUSTOM11"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom12")]
        public string CUSTOM_CUSTOM12 { get { return Raw["CUSTOM_CUSTOM12"]; } set { Raw["CUSTOM_CUSTOM12"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom13")]
        public string CUSTOM_CUSTOM13 { get { return Raw["CUSTOM_CUSTOM13"]; } set { Raw["CUSTOM_CUSTOM13"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom14")]
        public string CUSTOM_CUSTOM14 { get { return Raw["CUSTOM_CUSTOM14"]; } set { Raw["CUSTOM_CUSTOM14"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom15")]
        public string CUSTOM_CUSTOM15 { get { return Raw["CUSTOM_CUSTOM15"]; } set { Raw["CUSTOM_CUSTOM15"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom16")]
        public string CUSTOM_CUSTOM16 { get { return Raw["CUSTOM_CUSTOM16"]; } set { Raw["CUSTOM_CUSTOM16"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom17")]
        public string CUSTOM_CUSTOM17 { get { return Raw["CUSTOM_CUSTOM17"]; } set { Raw["CUSTOM_CUSTOM17"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom18")]
        public string CUSTOM_CUSTOM18 { get { return Raw["CUSTOM_CUSTOM18"]; } set { Raw["CUSTOM_CUSTOM18"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom19")]
        public string CUSTOM_CUSTOM19 { get { return Raw["CUSTOM_CUSTOM19"]; } set { Raw["CUSTOM_CUSTOM19"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom20")]
        public string CUSTOM_CUSTOM20 { get { return Raw["CUSTOM_CUSTOM20"]; } set { Raw["CUSTOM_CUSTOM20"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom21")]
        public string CUSTOM_CUSTOM21 { get { return Raw["CUSTOM_CUSTOM21"]; } set { Raw["CUSTOM_CUSTOM21"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom22")]
        public string CUSTOM_CUSTOM22 { get { return Raw["CUSTOM_CUSTOM22"]; } set { Raw["CUSTOM_CUSTOM22"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom23")]
        public string CUSTOM_CUSTOM23 { get { return Raw["CUSTOM_CUSTOM23"]; } set { Raw["CUSTOM_CUSTOM23"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom24")]
        public string CUSTOM_CUSTOM24 { get { return Raw["CUSTOM_CUSTOM24"]; } set { Raw["CUSTOM_CUSTOM24"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom25")]
        public string CUSTOM_CUSTOM25 { get { return Raw["CUSTOM_CUSTOM25"]; } set { Raw["CUSTOM_CUSTOM25"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom26")]
        public string CUSTOM_CUSTOM26 { get { return Raw["CUSTOM_CUSTOM26"]; } set { Raw["CUSTOM_CUSTOM26"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom27")]
        public string CUSTOM_CUSTOM27 { get { return Raw["CUSTOM_CUSTOM27"]; } set { Raw["CUSTOM_CUSTOM27"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom28")]
        public string CUSTOM_CUSTOM28 { get { return Raw["CUSTOM_CUSTOM28"]; } set { Raw["CUSTOM_CUSTOM28"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom29")]
        public string CUSTOM_CUSTOM29 { get { return Raw["CUSTOM_CUSTOM29"]; } set { Raw["CUSTOM_CUSTOM29"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom30")]
        public string CUSTOM_CUSTOM30 { get { return Raw["CUSTOM_CUSTOM30"]; } set { Raw["CUSTOM_CUSTOM30"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom31")]
        public string CUSTOM_CUSTOM31 { get { return Raw["CUSTOM_CUSTOM31"]; } set { Raw["CUSTOM_CUSTOM31"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom32")]
        public string CUSTOM_CUSTOM32 { get { return Raw["CUSTOM_CUSTOM32"]; } set { Raw["CUSTOM_CUSTOM32"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom33")]
        public string CUSTOM_CUSTOM33 { get { return Raw["CUSTOM_CUSTOM33"]; } set { Raw["CUSTOM_CUSTOM33"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom34")]
        public string CUSTOM_CUSTOM34 { get { return Raw["CUSTOM_CUSTOM34"]; } set { Raw["CUSTOM_CUSTOM34"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom35")]
        public string CUSTOM_CUSTOM35 { get { return Raw["CUSTOM_CUSTOM35"]; } set { Raw["CUSTOM_CUSTOM35"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom36")]
        public string CUSTOM_CUSTOM36 { get { return Raw["CUSTOM_CUSTOM36"]; } set { Raw["CUSTOM_CUSTOM36"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom37")]
        public string CUSTOM_CUSTOM37 { get { return Raw["CUSTOM_CUSTOM37"]; } set { Raw["CUSTOM_CUSTOM37"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom38")]
        public string CUSTOM_CUSTOM38 { get { return Raw["CUSTOM_CUSTOM38"]; } set { Raw["CUSTOM_CUSTOM38"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom39")]
        public string CUSTOM_CUSTOM39 { get { return Raw["CUSTOM_CUSTOM39"]; } set { Raw["CUSTOM_CUSTOM39"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom40")]
        public string CUSTOM_CUSTOM40 { get { return Raw["CUSTOM_CUSTOM40"]; } set { Raw["CUSTOM_CUSTOM40"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom41")]
        public string CUSTOM_CUSTOM41 { get { return Raw["CUSTOM_CUSTOM41"]; } set { Raw["CUSTOM_CUSTOM41"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom42")]
        public string CUSTOM_CUSTOM42 { get { return Raw["CUSTOM_CUSTOM42"]; } set { Raw["CUSTOM_CUSTOM42"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom43")]
        public string CUSTOM_CUSTOM43 { get { return Raw["CUSTOM_CUSTOM43"]; } set { Raw["CUSTOM_CUSTOM43"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom44")]
        public string CUSTOM_CUSTOM44 { get { return Raw["CUSTOM_CUSTOM44"]; } set { Raw["CUSTOM_CUSTOM44"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom45")]
        public string CUSTOM_CUSTOM45 { get { return Raw["CUSTOM_CUSTOM45"]; } set { Raw["CUSTOM_CUSTOM45"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom46")]
        public string CUSTOM_CUSTOM46 { get { return Raw["CUSTOM_CUSTOM46"]; } set { Raw["CUSTOM_CUSTOM46"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom47")]
        public string CUSTOM_CUSTOM47 { get { return Raw["CUSTOM_CUSTOM47"]; } set { Raw["CUSTOM_CUSTOM47"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom48")]
        public string CUSTOM_CUSTOM48 { get { return Raw["CUSTOM_CUSTOM48"]; } set { Raw["CUSTOM_CUSTOM48"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom49")]
        public string CUSTOM_CUSTOM49 { get { return Raw["CUSTOM_CUSTOM49"]; } set { Raw["CUSTOM_CUSTOM49"] = value; } }
        [Category ("Custom Fields")]
        [DisplayName ("Custom50")]
        public string CUSTOM_CUSTOM50 { get { return Raw["CUSTOM_CUSTOM50"]; } set { Raw["CUSTOM_CUSTOM50"] = value; } }

        public DemographicsEntity ()
        {
            CustomFieldDescriptions = new Dictionary<string,string> ();
        }

        public DemographicsEntity (Dictionary<string, string> values) : this ()
        {
            Raw = values;
        }

        public IEnumerable<string> GetRequiredDemographics ()
        {
            var required = new string[] { "PATIENT_FIRSTNAME", "PATIENT_LASTNAME", "PATIENT_MRN", "JOBS_APPOINTMENTDATE" };

            return required.Where (p => string.IsNullOrWhiteSpace (Raw[p]));
        }

        public PatientEntity ToPatient ()
        {
            var pat = new PatientEntity ();

            pat.Address1 = PATIENT_ADDRESS1;
            pat.Address2 = PATIENT_ADDRESS2;
            pat.AlternateID = PATIENT_ALTERNATEID;
            pat.City = PATIENT_CITY;
            pat.DOB = PATIENT_DOB;
            pat.FirstName = PATIENT_FIRSTNAME;
            pat.LastName = PATIENT_LASTNAME;
            pat.MiddleName = PATIENT_MI;
            pat.MRN = PATIENT_MRN;
            pat.Id = int.Parse (PATIENT_PATIENTID);
            pat.Phone = PATIENT_PHONE;
            pat.Sex = PATIENT_SEX;
            pat.SSN = PATIENT_SSN;
            pat.State = PATIENT_STATE;
            pat.Suffix = PATIENT_SUFFIX;
            pat.Zip = PATIENT_ZIP;

            return pat;
        }

        public void FromPatient (PatientEntity pat)
        {
            PATIENT_ADDRESS1 = pat.Address1;
            PATIENT_ADDRESS2 = pat.Address2;
            PATIENT_DOB = pat.DOB;
            PATIENT_FIRSTNAME = pat.FirstName;
            PATIENT_LASTNAME = pat.LastName;
            PATIENT_MRN = pat.MRN;
            PATIENT_ALTERNATEID = pat.AlternateID;
            PATIENT_CITY = pat.City;
            PATIENT_MI = pat.MiddleName;
            PATIENT_PATIENTID = pat.Id.ToString ();
            PATIENT_PHONE = pat.Phone;
            PATIENT_SEX = pat.Sex;
            PATIENT_STATE = pat.State;
            PATIENT_SUFFIX = pat.Suffix;
            PATIENT_ZIP = pat.Zip;
        }

        public void FromPhysician (Entrada.Editor.Data.FixedPhysicianEntity phys)
        {
            REFERRING_ADDRESS1 = phys.Address1;
            REFERRING_ADDRESS2 = phys.Address2;
            REFERRING_CITY = phys.City;
            REFERRING_CLINICNAME = phys.ClinicName;
            REFERRING_DOB = phys.DOB.HasValue ? phys.DOB.Value.ToShortDateString (): "";
            REFERRING_FAX = phys.Fax;
            REFERRING_FIRSTNAME = phys.FirstName;
            REFERRING_LASTNAME = phys.LastName;
            REFERRING_MI = phys.MI;
            REFERRING_PHONE = phys.Phone;
            REFERRING_PHYSICIANID = phys.PhysicianID.ToString ();
            REFERRING_SEX = (string)phys.Sex;
            REFERRING_SSN = (string)phys.SSN;
            REFERRING_STATE = phys.State;
            REFERRING_SUFFIX = phys.Suffix;
            REFERRING_ZIP = phys.Zip;
        }
    }
}
