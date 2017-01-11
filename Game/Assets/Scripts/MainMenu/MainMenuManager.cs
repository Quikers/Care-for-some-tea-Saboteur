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

    public void AddToQueue( int DeckId )
    {
        if( Data.User.Id == 0 ) return;
        SendTcp.SendPacket( new Packet( Data.User.Id.ToString(), "Server", TcpMessageType.AddPlayerToQueue, new[] { "Username", Data.User.Username } ), Data.Network.ServerSocket );

        Debug.Log( SendTcp.ReceivePacket( Data.Network.ServerSocket ) );
        Thread queueThread = new Thread( CheckQueue );
    }

    void CheckQueue()
    {
        while( true )
        {
            Debug.Log( SendTcp.ReceivePacket( Data.Network.ServerSocket ) );
        }
    }
}