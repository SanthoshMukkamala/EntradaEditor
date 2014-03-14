using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entrada.Entities;

namespace Entrada.Editor.Core
{
    public class CCEntity : Entrada.Editor.Data.FixedPhysicianEntity
    {
        public string Unique { get; set; }

        public CCEntity ()
        {
            Unique = Guid.NewGuid ().ToString ().Replace ("-", "");
        }

        public CCEntity (Entrada.Editor.Data.FixedPhysicianEntity phys) : this ()
        {
            Address1 = phys.Address1;
            Address2 = phys.Address2;
            City = phys.City;
            ClinicName = phys.ClinicName;
            DOB = phys.DOB;
            Email = phys.Email;
            Fax = phys.Fax;
            FirstName = phys.FirstName;
            Initials = phys.Initials;
            LastName = phys.LastName;
            MI = phys.MI;
            Phone = phys.Phone;
            PhysicianID = phys.PhysicianID;
            Sex = phys.Sex;
            SSN = phys.SSN;
            State = phys.State;
            Suffix = phys.Suffix;
            Zip = phys.Zip;
        }
    }
}
