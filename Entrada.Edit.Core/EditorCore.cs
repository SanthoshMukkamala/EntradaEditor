using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrada.Editor.Core
{
	public static class EditorCore
	{
		public static DocumentManager Documents { get; private set; }
		public static JobManager Jobs { get; private set; }
        public static PatientManager Patients { get; private set; }
        public static PhysicianManager Physicians { get; private set; }
        public static BackgroundManager Background { get; private set; }
        public static SettingsManager Settings { get; private set; }
        public static LoggingManager Logging { get; private set; }
        public static EditorManager Editor { get; private set; }

		static EditorCore ()
		{
            Settings = new SettingsManager ();
            Logging = new LoggingManager ();
			Documents = new DocumentManager ();
			Jobs = new JobManager ();
			Patients = new PatientManager ();
            Physicians = new PhysicianManager ();
            Background = new BackgroundManager ();
            Editor = new EditorManager ();
		}

        public static void LogError (string text, params object[] args)
        {
            Logging.LogError (text, args);
        }

        public static void LogException (string text, Exception ex)
        {
            Logging.LogException (text, ex);
        }

        public static void LogDebug (string text, params object[] args)
        {
            Logging.LogDebug (text, args);
        }

        public static AutoStopwatch CreateStopwatch (string text, params object[] args)
        {
            return new AutoStopwatch (string.Format (text, args));
        }

        public static AutoStatusUpdate CreateStatusUpdate (string text)
        {
            return new AutoStatusUpdate (text);
        }

        public static AutoStatusUpdate CreateStatusUpdate (string text, string imageHint)
        {
            return new AutoStatusUpdate (text, imageHint);
        }
    }
}
