using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Core
{
    public class InsertedMacroEntity
    {
        public string Name { get; set; }
        public int UneditedCharacterCount { get; set; }

        public InsertedMacroEntity ()
        {

        }

        public InsertedMacroEntity (string name, int characterCount)
        {
            Name = name;
            UneditedCharacterCount = characterCount;
        }
    }
}
