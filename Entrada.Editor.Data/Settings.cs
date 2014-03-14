using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entrada.WebServices.Client;

namespace Entrada.Editor.Data
{
    public static class Settings
    {
        internal static string EditingServiceUrl { get; set; }
        internal static string LoginToken { get; set; }
        internal static string UserName { get; set; }
        internal static string Password { get; set; }

        private static JobEditingServicesClient ws;
        private static ADTSearchServicesClient adt_ws;

        public static bool MustChangePassword { get; set; }

        static Settings ()
        {
            EditingServiceUrl = "editing_ws_app.entrada-dev.local";
        }

        public static void SetEnvironment (int env)
        {
            switch (env) {
                case 1:
                    EditingServiceUrl = "greenocean.entradahealth.net";
                    ws = null;
                    return;
                case 2:
                    EditingServiceUrl = "editing-sales.entradahealth.net";
                    ws = null;
                    return;
                case 3:
                    EditingServiceUrl = "editing_ws_app.entrada-dev.local";
                    ws = null;
                    return;
            }
        }

        public static JobEditingServicesClient CreateEditService (bool skipSignIn = false)
        {
            if (ws == null) {
                ws = new JobEditingServicesClient ();
                var ep = string.Format ("https://{0}/job.editing.services.svc", EditingServiceUrl);

                ws.Endpoint.Address = new System.ServiceModel.EndpointAddress (ep);
            }

            if (skipSignIn)
                return ws;

            if (!ws.IsSessionAlive ())
                LoginToken = ws.SignIn (UserName, Settings.CalculateMD5Hash (Password));

            return ws;
        }

        public static ADTSearchServicesClient CreateAdtService ()
        {
            if (adt_ws == null) {
                adt_ws = new ADTSearchServicesClient ();
                var ep = string.Format ("https://{0}/adt.search.services.svc", EditingServiceUrl);

                adt_ws.Endpoint.Address = new System.ServiceModel.EndpointAddress (ep);
            }

            if (!adt_ws.IsSessionAlive ())
                LoginToken = adt_ws.SignIn (UserName, Settings.CalculateMD5Hash (Password));

            return adt_ws;
        }

        public static string CalculateMD5Hash (string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = System.Security.Cryptography.MD5.Create ();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes (input);
            byte[] hash = md5.ComputeHash (inputBytes);
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder ();
            for (int i = 0; i < hash.Length; i++)
                sb.Append (hash[i].ToString ("X2"));
            return sb.ToString ().ToLower ();
        }

        public static string GetHashedPassword ()
        {
            return Settings.CalculateMD5Hash (Password);
        }

        public static void ResetPassword (string password)
        {
            Password = password;
            MustChangePassword = false;

            // Clear out our cached web services so they'll
            // be recreated with the new password
            ws = null;
            adt_ws = null;
        }
    }
}
