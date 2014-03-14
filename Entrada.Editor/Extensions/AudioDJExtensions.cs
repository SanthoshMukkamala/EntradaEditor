using AudioDjStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entrada.Editor.Core;
using System.IO;

namespace Entrada.Editor
{
	// The AudioDjStudio API is not very .NET friendly, returning error codes
	// rather than throwing exceptions.  Also, we only use 1 player, so we don't
	// need to specify the index on every call.
	public static class AudioDJExtensions
	{
        public static void CloseSound (this AudioDjStudio.AudioDjStudio dj)
        {
            var result = dj.CloseSound (0);

            // If the sound was already closed, ignore the error
            if (result == enumErrorCodes.ERR_SOUND_NOT_LOADED)
                return;

            // If the player wasn't created yet, ignore the error
            if (result == enumErrorCodes.ERR_INVALID_PLAYER)
                return;

            ThrowIfExceptional (result);
        }
        
        public static int GetSoundDuration (this AudioDjStudio.AudioDjStudio dj)
		{
			return dj.GetSoundOriginalDuration (0);
		}

		public static double GetSoundPosition (this AudioDjStudio.AudioDjStudio dj)
		{
			double pos = 0;

			var result = dj.SoundPositionGet (0, ref pos, false);

			ThrowIfExceptional (result);

			return pos;
		}

		public static string GetSoundPositionString (this AudioDjStudio.AudioDjStudio dj)
		{
			return dj.SoundPositionStringGet (0, true, false, ":", null, 0, false);
		}

        public static enumPlayerStatus GetStatus (this AudioDjStudio.AudioDjStudio dj)
        {
            return dj.GetPlayerStatus (0);
        }

        public static bool IsSoundLoaded (this AudioDjStudio.AudioDjStudio dj)
        {
            var result = dj.GetLoadedSoundFile (0);

            if (!string.IsNullOrWhiteSpace (result))
                return true;

            return false;
        }

		public static void LoadSound (this AudioDjStudio.AudioDjStudio dj, string filename)
		{
            using (var stream = EncryptedFileSystem.GetDecryptedStream (filename)) {
                var bytes = new byte[stream.Length];

                stream.Read (bytes, 0, bytes.Length);
                var result = dj.LoadSoundFromMemory (0, bytes, bytes.Length);

                ThrowIfExceptional (result);
            }
		}

		public static void Pause (this AudioDjStudio.AudioDjStudio dj)
		{
			if (dj.GetStatus () != enumPlayerStatus.SOUND_PLAYING)
				return;

			var result = dj.PauseSound (0);

			ThrowIfExceptional (result);
		}

		public static void Play (this AudioDjStudio.AudioDjStudio dj)
		{
			var result = dj.PlaySound (0);

			ThrowIfExceptional (result);
		}

		public static void Seek (this AudioDjStudio.AudioDjStudio dj, uint pos)
		{
            // Seek scales based on tempo, but all our calculations expect
            // a constant duration.  Temporarily reset tempo to 0 and make change.
            var tempo = dj.GetTempoPerc (0);
            
            if (tempo != 0)
                dj.SetTempo (0);

			var result = dj.SeekSound (0, pos);

            if (tempo != 0)
                dj.SetTempo (tempo);

			ThrowIfExceptional (result);
		}

        public static void SetTempo (this AudioDjStudio.AudioDjStudio dj, float tempo)
        {
            var result = dj.SetTempoPerc (0, tempo);

            ThrowIfExceptional (result);
        }

        public static void SetForwardRewindGranularity (this AudioDjStudio.AudioDjStudio dj, float granularity)
        {
            var result = dj.SetForwardRewindGranularity (0, granularity);

            ThrowIfExceptional (result);
        }

        public static void Rewind (this AudioDjStudio.AudioDjStudio dj)
        {
            var result = dj.RewindSound (0);

            ThrowIfExceptional (result);
        }

        public static void FastForward (this AudioDjStudio.AudioDjStudio dj)
        {
            var result = dj.ForwardSound (0);

            ThrowIfExceptional (result);
        }

		private static void ThrowIfExceptional (enumErrorCodes result)
		{
			if (result == enumErrorCodes.ERR_NOERROR)
				return;

			throw new InvalidOperationException (result.ToString ());
		}
	}
}
