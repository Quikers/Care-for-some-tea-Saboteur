using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace Utilities
{
    public static class Find
    {
        public static T InParents<T>( GameObject go ) where T : Component
        {
            if( go == null )
                return null;
            var comp = go.GetComponent<T>();

            if( comp != null )
                return comp;

            var t = go.transform.parent;
            while( t != null && comp == null )
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return comp;
        }
    }

    public static class Api
    {
        public static string UserbyEmail( string email, string password )
        {
            WebRequest request = WebRequest.Create( "http://careforsometeasaboteur.com/api/checklogin/" + email + "/" + password );

            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();

            if( ( ( HttpWebResponse )response ).StatusDescription == "OK" )
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader( dataStream );

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
