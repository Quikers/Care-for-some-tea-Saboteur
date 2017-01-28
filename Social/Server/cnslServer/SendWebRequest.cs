using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace cnslServer
{
    public static class SendWebRequest
    {
        public static string GetResponse(string uri)
        {
            // Create a request for the URL. 		
            WebRequest request = WebRequest.Create(uri);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Display the status.
            //Console.WriteLine(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();

            if (responseFromServer == "false") return null;
            else return (responseFromServer);

        }
    }
}
