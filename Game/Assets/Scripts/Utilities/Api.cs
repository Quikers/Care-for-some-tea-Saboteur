using System.Diagnostics;
using System.IO;
using System.Net;

namespace Utilities
{
    namespace Api
    {
        public static class User
        {
            public static string ByEmail( string email, string password )
            {                
                // Make request to server.
                WebRequest request =
                    WebRequest.Create( "http://213.46.57.198/api/checklogin/" + email + "/" + password );

                request.Credentials = CredentialCache.DefaultCredentials;

                // Get the response from the server.
                WebResponse response = request.GetResponse();

                // if request is not OK close it and return null.
                if( ( ( HttpWebResponse ) response ).StatusDescription == "OK" )
                {
                    // Get the response stream and make a reader.
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader( dataStream );

                    // Put the response in a var, close the reader and webresponse and return response.
                    string responseFromServer = reader.ReadToEnd();

                    reader.Close();
                    response.Close();
                    return responseFromServer;
                }

                response.Close();
                return null;
            }
        }

        public static class Deck
        {
            public static string ByUserId( int userId )
            {
                // Make request to server.
                WebRequest request =
                    WebRequest.Create( "http://213.46.57.198/api/getdecksbyuserid/" + userId );
                request.Credentials = CredentialCache.DefaultCredentials;

                // Get the response from the server.
                WebResponse response = request.GetResponse();

                // if request is not OK close it and return null.
                if( ( ( HttpWebResponse ) response ).StatusDescription == "OK" )
                {
                    // Get the response stream and make a reader.
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader( dataStream );

                    // Put the response in a var, close the reader and webresponse and return response.
                    string responseFromServer = reader.ReadToEnd();

                    reader.Close();
                    response.Close();
                    return responseFromServer;
                }

                response.Close();
                return null;
            }

            public static string ByDeckId( int deckId )
            {
                // Make request to server.
                WebRequest request =
                    WebRequest.Create( "http://213.46.57.198/api/getdeckbydeckid/" + deckId );
                request.Credentials = CredentialCache.DefaultCredentials;

                // Get the response from the server.
                WebResponse response = request.GetResponse();

                // if request is not OK close it and return null.
                if( ( ( HttpWebResponse )response ).StatusDescription == "OK" )
                {
                    // Get the response stream and make a reader.
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader( dataStream );

                    // Put the response in a var, close the reader and webresponse and return response.
                    string responseFromServer = reader.ReadToEnd();

                    reader.Close();
                    response.Close();
                    return responseFromServer;
                }

                response.Close();
                return null;
            }
        }
    }
}
