using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entrada.Entities;

namespace Entrada.Editor.Core
{
	public class JobEventArgs : EventArgs
	{
        public MedicalJobEntity Job { get; private set; }

        public JobEventArgs (MedicalJobEntity job)
		{
			Job = job;
		}
	}
}
