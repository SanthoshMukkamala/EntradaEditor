using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entrada.Entities;

namespace Entrada.Editor.Core
{
	public class CloseSoundEventArgs : EventArgs
	{
        public string SoundFile { get; private set; }

        public CloseSoundEventArgs (string soundFile)
		{
			SoundFile = soundFile;
		}
	}
}
