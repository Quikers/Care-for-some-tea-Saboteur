using Library;
using UnityEngine;
using UnityEngine.UI;

public class LoginInfoManager : MonoBehaviour
{
    public Text EmailText;
    public Button LogoutButton;
    public Button LoginButton;
    public Button GameButton;

    public MainMenu.QueueInfoController QueuePanel;

    public void Login()
    {
        LoginButton.gameObject.SetActive( false );

        EmailText.gameObject.SetActive( true );
        LogoutButton.gameObject.SetActive( true );
        GameButton.interactable = true;

        EmailText.text = Data.PlayerUser.Email;
    }

    public void Logout( GameObject loginCover )
    {
        SendTcp.SendPacket( new Packet( Data.PlayerUser.Id.ToString(), "Server", TcpMessageType.Logout, new[] { "Username", Data.PlayerUser.Username } ), Data.Network.ServerSocket );

        QueuePanel.CancelQueue();

        LoginButton.gameObject.SetActive( true );

        EmailText.gameObject.SetActive( false );
        LogoutButton.gameObject.SetActive( false );

        loginCover.SetActive( true );

        GameButton.interactable = false;

        Data.PlayerUser.Empty();
    }
}