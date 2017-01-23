using System.Threading;
using Library;

namespace NetCode
{
    public class NetworkController
    {
        public static bool GameStart;
        public static bool EndTurn;

        public static void StartListener()
        {
            Thread recieveThread = new Thread( GetMessages );
            recieveThread.Start();
        }

        static void GetMessages()
        {
            while( Data.Network.ServerSocket.Connected )
            {
                HandleMessage( SendTcp.ReceivePacket( Data.Network.ServerSocket ) );
            }
        }

        static void HandleMessage( Packet recievedPacket )
        {
            UnityEngine.Debug.Log( recievedPacket );
            switch( recievedPacket.Type )
            {
                case TcpMessageType.MatchStart:
                    UnityEngine.Debug.Log( "Match Start" );

                    Data.EnemyUser.Id = int.Parse( recievedPacket.Variables[ "UserID" ] );
                    Data.EnemyUser.UserName = recievedPacket.Variables[ "Username" ];

                    GameStart = true;
                    break;

                case TcpMessageType.Broadcast :
                    break;
                case TcpMessageType.PlayerUpdate:
                    if( recievedPacket.Variables[ "PlayerAction" ] == "EndTurn" )
                    {
                        UnityEngine.Debug.Log( "EndTurn" );

                        EndTurn = true;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}