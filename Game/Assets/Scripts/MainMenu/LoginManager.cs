using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    public Text email;
    public void Login()
    {
        Utilities.API.UserbyEmail( email.text );

    }
}