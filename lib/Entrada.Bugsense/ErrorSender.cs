using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Text;
using System;

namespace Entrada.Bugsense
{
    class ErrorSender
    {
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private readonly string backup_url = "https://alberta.entradahealth.net/api/errors";

        public ErrorSender (string apiKey, string apiUrl)
        {
            _apiKey = apiKey;
            _apiUrl = apiUrl;
        }

        private string ToJsonString<T> (T o)
        {
            var ms = new MemoryStream ();
            var jsonSerializer = new DataContractJsonSerializer (typeof (T));
            jsonSerializer.WriteObject (ms, o);
            return Encoding.UTF8.GetString (ms.GetBuffer (), 0, (int)ms.Length);
        }

        internal void SendOrStore (BugSenseRequest errorReport)
        {
            string serializedErrorReport = Serialize (errorReport);
            try {
                Send (serializedErrorReport, _apiUrl);
            } catch (IOException) {
                Store (serializedErrorReport);
            } catch (SecurityException) {
                Store (serializedErrorReport);
            }

            try {
                // Try to send it to our internal crash database
                Send (serializedErrorReport, backup_url + "/" + _apiKey);
            } catch (Exception) {
                try {
                    // Sometimes the proxy barfs, try once more just in case
                    Send (serializedErrorReport, backup_url + "/" + _apiKey);
                } catch (Exception) {
                    // Give up
                }
            }
        }

        private string Serialize (BugSenseRequest errorReport)
        {
            var ms = new MemoryStream ();
            using (var requestStream = new StreamWriter (ms)) {
                requestStream.Write ("data=");
                requestStream.Write (Uri.EscapeDataString (ToJsonString (errorReport)));
            }
            return Encoding.UTF8.GetString (ms.ToArray ());
        }

        private void Send (string serializedErrorReport, string url)
        {
            var request = WebRequest.Create (url);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["X-BugSense-Api-Key"] = _apiKey;

            using (var requestStream = new StreamWriter (request.GetRequestStream ()))
                requestStream.Write (serializedErrorReport);

            var response = request.GetResponse ();

            //var content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //Console.WriteLine(content);
            //foreach (var header in response.Headers)
            //{
            //    Console.WriteLine(header);
            //}
            // TODO: Handle response...?

            //string text = string.Empty;
            //var responseStream = response.GetResponseStream();
            //if (responseStream != null)
            //{
            //    var streamReader = new StreamReader(responseStream);
            //    text = streamReader.ReadToEnd();
            //    streamReader.Close();
            //}
        }

        private void Store (string serializedErrorReport)
        {
        }
    }
}