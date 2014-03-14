using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Core
{
    public class TddTag
    {
        public string Name { get; set; }
        public bool Required { get; set; }

        public TddTag ()
        {
        }

        public TddTag (string name, bool required = false)
        {
            Name = name;
            Required = required;
        }

        public override string ToString ()
        {
            if (!Required)
                return Name;

            return string.Format ("{0} [Required]", Name);
        }
    }
}
