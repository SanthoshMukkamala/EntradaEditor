using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Core
{
    public class AvailableJobEntity
    {
        public bool Stat { get; set; }
        public DateTime TurnaroundTime { get {
                if (Stat)
                    return ReceivedOn.AddHours (2);

                return ReceivedOn.AddHours (24);
            }
        }
        public string JobNumber { get; set; }
        public string ClinicName { get; set; }
        public string DictatorID { get; set; }
        public string JobType { get; set; }
        public string MRN { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public int Duration { get; set; }

        public string LastQANote { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }                

        [Browsable (false)]
        public int ClinicID { get; set; }
        public DateTime DictationDate { get; set; }
        [Browsable (false)]
        public string CurrentQAStage { get; set; }
        [Browsable (false)]
        public int Location { get; set; }
        [Browsable (false)]
        public string MI { get; set; }
        [Browsable (false)]
        public DateTime ReceivedOn { get; set; }
        [Browsable (false)]
        public bool VREnabled { get; set; }
        [Browsable (false)]
        public string EditorID { get; set; }        
        [Browsable (false)]
        public string PayType { get; set; }
    }
}
