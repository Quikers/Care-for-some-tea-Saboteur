using Library;
using UnityEngine;
using UnityEngine.UI;

public class LoginInfoManager : MonoBehaviour
{
    public Text EmailText;
    public Button LogoutButton;
    public Button LoginButton;

    public void Login()
    {
        LoginButton.gameObject.SetActive( false );

        EmailText.gameObject.SetActive( true );
        LogoutButton.gameObject.SetActive( true );

        EmailText.text = Data.User.Email;
    }

    public void Logout( GameObject loginCover )
    {
        SendTcp.SendPacket( new Packet( Data.User.Id.ToString(), "Server", TcpMessageType.Logout, new[] { "Username", Data.User.Username } ), Data.Network.ServerSocket );

        LoginButton.gameObject.SetActive( true );

        EmailText.gameObject.SetActive( false );
        LogoutButton.gameObject.SetActive( false );

        loginCover.SetActive( true );

        Data.User.Empty();
    }
}