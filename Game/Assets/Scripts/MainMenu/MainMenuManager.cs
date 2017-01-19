using System.Threading;
using Library;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadScene( string SceneName )
    {
        SceneManager.LoadScene( SceneName );
    }

    public void LoadScene( int SceneId )
    {
        SceneManager.LoadScene( SceneId );
    }

    public void AddToQueue()
    {
        if( Data.PlayerUser.Id == 0 ) return;
        SendTcp.SendPacket( new Packet( Data.PlayerUser.Id.ToString(), "Server", TcpMessageType.AddPlayerToQueue, new[] { "Username", Data.PlayerUser.Username } ), Data.Network.ServerSocket );

        //Debug.Log( SendTcp.ReceivePacket( Data.Network.ServerSocket ) );
    }

    void ServerListener()
    {
        GetComponent<NetworkController>().StartListener();
    }
}