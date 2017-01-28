using System;
using System.Net.Sockets;
using Library;
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
        public InputField IP;

        void Start()
        {
        }

        public void Register()
        {
            // you can go fuck yourself i ain't gonna make that.
            Application.OpenURL( "http://213.46.57.198/account?type=register" );
        }

        public void Login()
        {
            string login = Utilities.Api.User.ByEmail( Email.text, Password.text );

            if( login == null )
            {
                message = "Could not connect to server.";
                Debug.Log( message );
                return;
            }

            try
            {
                TempUserData temp = JsonUtility.FromJson< TempUserData >( login );

                // transfer data to user object.
                Data.PlayerUser.Email = temp.email;
                Data.PlayerUser.TimeCreated = temp.created;
                Data.PlayerUser.TimeEdited = temp.editted;
                Data.PlayerUser.Username = temp.username;
                Data.PlayerUser.AccountType = temp.accountType;
                Data.PlayerUser.Id = temp.id;
            }
            catch( Exception ex )
            {
                Debug.Log( ex );
                Utilities.Screen.LogError( ex );

                Data.PlayerUser.Empty();
                Email.text = "";
                Password.text = "";
                LoginPanel.SetActive( false );

                return;
            }

            try
            {
                Data.Network.ServerSocket = new TcpClient( IP.text, 25002 );

                // login to social server.
                SendTcp.SendPacket(
                    new Packet( Data.PlayerUser.Id.ToString(), "Server", TcpMessageType.Login,
                        new[] { "Username", Data.PlayerUser.Username } ), Data.Network.ServerSocket );

            }
            catch( SocketException ex )
            {                
                Debug.Log( ex );
                Utilities.Screen.LogError( ex );

                Email.text = "";
                Password.text = "";
                LoginPanel.SetActive( false );
                FindObjectOfType< LoginInfoManager >().Login();

                return;
            }
            Email.text = "";
            Password.text = "";
            LoginPanel.SetActive( false );
            FindObjectOfType< LoginInfoManager >().Login();
            FindObjectOfType< MainMenuManager >().ServerListener();
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