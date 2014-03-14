using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrada.Editor.Core
{
	public class AudioSpeedChangedEventArgs : EventArgs
	{
		public float Speed { get; private set; }

		public AudioSpeedChangedEventArgs (float speed)
		{
			Speed = speed;
		}
	}
}
