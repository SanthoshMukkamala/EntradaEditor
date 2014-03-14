using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Entrada.Bugsense;
using Entrada.Editor.Core;

namespace Entrada.Editor
{
    static class Program
    {
        private const string bugsenseApiKey = "w8c19730";
        private static IdleTimerWatcher idle_watcher;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main ()
        {
            bool in_use;
            var mutex_name = string.Format ("Local\\Entrada EditOne");

            EditorCore.LogDebug ("*** Application Started [{0}] ***", Assembly.GetEntryAssembly ().GetName ().Version);
            EditorCore.LogDebug ("*** {0} ***", Environment.OSVersion);

            using (var m = new Mutex (true, mutex_name, out in_use)) {
                if (!in_use) {
                    var msg = "Only one copy of Entrada Editor can be running at once.  Please use or close the other opened instance.";
                    MessageBox.Show (msg, "Entrada Editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                } else {
                    Application.EnableVisualStyles ();
                    Application.SetCompatibleTextRenderingDefault (false);

                    BugSense.Init (bugsenseApiKey);

                    DevExpress.Skins.SkinManager.EnableFormSkins ();

                    Application.ThreadException += Application_ThreadException;
                    EditorCore.Background.UnhandledException += Background_UnhandledException;

                    using (var dialog = new LoginDialog ())
                        if (dialog.ShowDialog () == DialogResult.OK) {
                            // We send the editor name in the "UID" BugSense field
                            BugSense.SetUid (EditorCore.Settings.Editor.EditorID);

                            // Set up our idle time monitor
                            idle_watcher = new IdleTimerWatcher (EditorCore.Settings.Editor.IdleTime);
                            Application.AddMessageFilter (idle_watcher);

                            Application.Run (new EditorForm (dialog.MustChangePassword));
                        }
                }
            }

            EditorCore.LogDebug ("*** Application Exited ***{0}", Environment.NewLine);
        }

        private static void Application_ThreadException (object sender, ThreadExceptionEventArgs e)
        {
            // Try to log the exception
            try {
                EditorCore.Logging.LogException ("Unhandled Exception:\n{0}", e.Exception);
            } catch {
                Console.WriteLine ("Error logging unhandled exception:\n{0}", e.Exception);
            }

            if (!ShouldDisplayExceptionToUser (e.Exception))
                return;

            // Display the exception
            using (var dialog = new ExceptionDialog ("An unexpected error has occured that the application cannot recover from. Things may not work correctly until you restart the application.", e.Exception, false)) {
                dialog.ShowDialog ();

                try {
                    if (!System.Diagnostics.Debugger.IsAttached)
                        BugSense.SendException (e.Exception);
                } catch (Exception) {
                    // If we didn't successfully send the error to BugSense, just ignore
                    // it.  There's no sense in telling the user we couldn't log it.
                }
            }
        }

        private static void Background_UnhandledException (object sender, Core.UnhandledExceptionEventArgs e)
        {
            if (!ShouldDisplayExceptionToUser (e.Exception))
                return;
            
            using (var dialog = new ExceptionDialog (e.Text, e.Exception, e.CanRetry)) {
                if (dialog.ShowDialog () != System.Windows.Forms.DialogResult.OK) {
                    e.Cancel = true;

                    try {
                        if (!System.Diagnostics.Debugger.IsAttached && ShouldSendExceptionToBugSense (e.Exception))
                            BugSense.SendException (e.Exception);
                    } catch (Exception) {
                        // If we didn't successfully send the error to BugSense, just ignore
                        // it.  There's no sense in telling the user we couldn't log it.
                    }
                }
            }
        }

        // Obviously we should be very careful about which exceptions we choose to ignore
        private static bool ShouldSendExceptionToBugSense (Exception ex)
        {
            // This means it's a QA user and they aren't on the VPN
            if (ex.Message.StartsWith ("There was no endpoint listening at https://editing_ws_app.entrada-dev.local/"))
                return false;

            // This means we couldn't send a previous exception to bugsense
            if (ex.Message.StartsWith ("The remote name could not be resolved: 'bugsense.appspot.com'"))
                return false;

            return true;
        }

        private static bool ShouldDisplayExceptionToUser (Exception ex)
        {
            // DexExpress errors
            if (ex is IndexOutOfRangeException && ex.StackTrace.Contains ("WordsDocumentModelIteratorBase.GetCharacter"))
                return false;

            if (ex is NullReferenceException && ex.StackTrace.Contains ("IXtraTabPage.get_PageEnabled"))
                return false;

            if (ex is ArgumentException && ex.StackTrace.Contains ("FindRunStartLogPosition"))
                return false;

            return true;
        }
    }
}