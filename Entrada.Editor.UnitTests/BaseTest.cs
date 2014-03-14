using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Entrada.Editor.UnitTests
{
    public abstract class BaseTest
    {
        protected string GetText (string file)
        {
            // This doesn't work for CI
            //var dir = Path.GetDirectoryName (typeof(BaseTest).Assembly.Location);
            //var path = Path.Combine (dir, "TestData", file);
            //return File.ReadAllText (path);

            var asm = Assembly.GetExecutingAssembly ();

            using (var sr = new StreamReader (asm.GetManifestResourceStream ("Entrada.Editor.UnitTests.TestData." + file)))
                return sr.ReadToEnd ();
        }
    }
}
