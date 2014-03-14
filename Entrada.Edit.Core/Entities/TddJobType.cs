using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Core
{
    public class TddJobType
    {
        public string Name { get; set; }
        public bool AllTagged { get; set; }
        public List<TddTag> Tags { get; private set; }

        public TddJobType ()
        {
            Tags = new List<TddTag> ();
        }

        public bool IsValidTag (string name)
        {
            return Tags.Any (p => p.Name == name);
        }

        public List<string> GetTagShortcuts ()
        {
            var list = new List<string> ();

            for (var i = 0; i < Tags.Count; i++) {
                if (i < 9)
                    list.Add (string.Format ("Alt-{0}: {1}", i + 1, Tags[i]));
                else if (i == 9)
                    list.Add (string.Format ("Alt-0: {0}", Tags[i]));
                else
                    list.Add (Tags[i].ToString ());
            }

            return list;
        }
    }
}
