using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Core
{
	public class AudioEntity
	{
		public string AudioFile { get; set; }
		public int Duration { get; set; }
		public uint Position { get; set; }
		public float Speed { get; set; }

		public AudioEntity (string filename, int duration)
		{
			AudioFile = filename;
			Duration = duration;
			Position = 0;
			Speed = 0;
		}

		public bool CanSpeedUp { get { return Speed < 40; } }
		public bool CanSlowDown { get { return Speed > -40; } }

		public void SpeedUp ()
		{
			if (CanSpeedUp)
				Speed += 10;
		}

		public void SlowDown ()
		{
			if (CanSlowDown)
				Speed -= 10;
		}
	}
}
