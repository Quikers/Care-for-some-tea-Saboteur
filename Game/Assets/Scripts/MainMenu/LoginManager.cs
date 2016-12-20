using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    public string message;

    public Text Email;
    public InputField Password;
    public void Login()
    {
        string Login = Utilities.API.UserbyEmail( Email.text, Password.text );

        if( Login == null )
        {
            message = "Could not connect to server.";
            Debug.Log( message );
        }
            

    }
}