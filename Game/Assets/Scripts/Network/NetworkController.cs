using System.Threading;
using Library;
using UnityEngine;

public class NetworkController : MonoBehaviour
{
    public bool GameStart;

    public void StartListener()
    {
        Thread recieveThread = new Thread( GetMessages );
        recieveThread.Start();
    }

    void GetMessages()
    {
        while( Data.Network.ServerSocket.Connected )
        {
            HandleMessage( SendTcp.ReceivePacket( Data.Network.ServerSocket ) );
        }

    }

    void HandleMessage( Packet recievedPacket )
    {
        switch( recievedPacket.Type )
        {
            case TcpMessageType.MatchStart :
                Data.EnemyUser.Id = int.Parse( recievedPacket.Variables[ "UserID" ] );

                GameStart = true;
                break;
            default:
                Debug.Log( recievedPacket );
                break;
        }
    }
}