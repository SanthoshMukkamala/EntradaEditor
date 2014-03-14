using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Data
{
    // The PhysicianEntity has a DateTime DOB, but the JSON can send an empty string
    public class FixedPhysicianEntity
    {
        public DateTime? DOB { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ClinicName { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string Initials { get; set; }
        public string LastName { get; set; }
        public string MI { get; set; }
        public string Phone { get; set; }
        public object PhysicianID { get; set; }
        public object Sex { get; set; }
        public object SSN { get; set; }
        public string State { get; set; }
        public string Suffix { get; set; }
        public string Zip { get; set; }
        
        public string Format ()
        {
            return string.Join (" ", new string[] { FirstName, MI, LastName, Suffix }.Where (p => !string.IsNullOrWhiteSpace (p)));
        }

        public string ToFullFormat ()
        {
            var sb = new StringBuilder ();
            sb.AppendLine (this.Format ());

            if (!string.IsNullOrWhiteSpace (Address1))
                sb.AppendLine (Address1);

            if (!string.IsNullOrWhiteSpace (Address2))
                sb.AppendLine (Address2);

            if (!string.IsNullOrWhiteSpace (City + State + Zip)) {
                sb.AppendFormat ("{0}, {1}, {2}", City, State, Zip);
                sb.AppendLine ();
            }

            if (!string.IsNullOrWhiteSpace (Fax))
                sb.AppendLine (Fax);

            return sb.ToString ();
        }
    }
}
