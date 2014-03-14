using System;
using System.Reflection;
using System.Text;

namespace Entrada.Bugsense
{
    internal class CrashInformationCollector
    {
        private readonly string _version;
        private string _uid;

        public CrashInformationCollector (string version)
        {
            _version = version;
            _uid = Environment.MachineName;
        }

        public void SetUid (string uid)
        {
            _uid = uid;
        }

        public BugSenseRequest CreateCrashReport (Exception exception)
        {
            var entryAssemblyName = Assembly.GetEntryAssembly ().GetName ();
            var operatingSystem = Environment.OSVersion;

            var fullStacktrace = GetStackTrace (exception);

            return new BugSenseRequest (
                new BugSenseEx {
                    ExceptionType = exception.GetType ().ToString (),
                    Message = exception.Message,
                    DateOccured = DateTime.UtcNow,
                    StackTrace = fullStacktrace
                },
                new AppEnvironment {
                    AppName = entryAssemblyName.Name,
                    AppVersion = _version ?? entryAssemblyName.Version.ToString (4),
                    OsVersion = operatingSystem.Version.ToString (4),
                    UniqueID = _uid
                }
                );
        }

        private static string GetStackTrace (Exception exception)
        {
            var sb = new StringBuilder ();
            var ex = exception;
            sb.AppendLine (string.IsNullOrEmpty (ex.StackTrace) ? "not available" : ex.StackTrace);
            ex = ex.InnerException;
            while (ex != null) {
                sb.AppendLine (string.Format ("Caused by: {0}: {1}", ex.GetType ().Name, ex.Message));
                sb.AppendLine (string.IsNullOrEmpty (ex.StackTrace) ? "not available" : ex.StackTrace);
                ex = ex.InnerException;
            }
            return sb.ToString ();
        }
    }
}