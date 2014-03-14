using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Entrada.Editor.Core
{
    public class LoggingManager
    {
        public LogLevel ConsoleLogLevel { get; set; }
        public LogLevel FileLogLevel { get; set; }

        private object file_lock = new object ();

        public string LogFile { get {
            return Path.Combine (EditorCore.Settings.LogsDirectory, DateTime.Now.ToString ("yyyy-MM-dd") + ".txt");
        } }

        public void LogError (string text, params object[] args)
        {
            var txt = string.Format (text, args);

            LogToConsole (LogLevel.Error, txt);
            LogToFile (LogLevel.Error, txt);
        }

        public void LogException (string text, Exception ex)
        {
            if (!text.Contains ("{0}"))
                text += "\n{0}";

            var txt = string.Format (text, ex);

            LogToConsole (LogLevel.Error, txt);
            LogToFile (LogLevel.Error, txt);
        }

        public void LogDebug (string text, params object[] args)
        {
            var txt = string.Format (text, args);

            LogToConsole (LogLevel.Debug, txt);
            LogToFile (LogLevel.Debug, txt);
        }

        private void LogToConsole (LogLevel level, string text)
        {
            if (level < ConsoleLogLevel)
                return;

            Console.WriteLine (text);
        }

        private void LogToFile (LogLevel level, string text)
        {
            if (level < FileLogLevel)
                return;

            try {
                lock (file_lock)
                    File.AppendAllText (LogFile, DateTime.Now.ToString ("HH:mm:ss.fff").PadRight (13) + text + Environment.NewLine);
            } catch (Exception) {
                // Ignore if we can't log to the file
            }
        }
    }
}
