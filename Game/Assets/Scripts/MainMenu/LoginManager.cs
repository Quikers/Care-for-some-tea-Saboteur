using System;
using System.Collections;
using System.Reflection;
using System.Security.Policy;
using UnityEngine.UI;
using UnityEngine;

namespace MainMenu
{
    public class LoginManager : MonoBehaviour
    {
        string message;

        public GameObject LoginPanel;

        public InputField Email;
        public InputField Password;

        public void Register()
        {
            // you can go fuck yourself i ain't gonna make that.
            Application.OpenURL( "http://careforsometeasaboteur.com/account?type=register" );
        }

        public void Login()
        {
            string login = Utilities.Api.User.UserbyEmail( Email.text, Password.text );

            if( login == null )
            {
                message = "Could not connect to server.";
                Debug.Log( message );
                return;
            }

            try
            {
                TempUserData temp = JsonUtility.FromJson< TempUserData >( login );
                
                Data.User.Email = temp.email;
                Data.User.TimeCreated = temp.created;
                Data.User.TimeEdited = temp.editted;
                Data.User.Username = temp.username;
                Data.User.AccountType = temp.accountType;
                Data.User.Id = temp.id;

            }
            catch( Exception ex )
            {
                Debug.Log( ex );
                return;
            }
            Email.text = "";
            Password.text = "";
            LoginPanel.SetActive( false );
            FindObjectOfType<LoginInfoManager>().Login();
        }
    }
}

[Serializable]
public class TempUserData
{
    public int id = 0;
    public string email = "";
    public string username = "";
    public int accountType = 0;
    public string created = "";
    public string editted = "";
}