using System.Threading;
using Library;
using System.Collections.Generic;

namespace NetCode
{
    public class NetworkController
    {
        public static bool GameStart;
        public static bool EndTurn;
        public static List< Data.Card > PlayCardsQueue = new List< Data.Card >();
        public static Dictionary<int , int> AttackingQueue = new Dictionary< int, int >();

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

                    Data.EnemyUser.Id = int.Parse( recievedPacket.Variables[ "UserID" ] );
                    Data.EnemyUser.UserName = recievedPacket.Variables[ "Username" ];

                    Data.Turn.First = ( Data.TurnType )int.Parse( recievedPacket.Variables[ "Turn" ] ) - 1;

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
                    else if( recievedPacket.Variables[ "PlayerAction" ] == "PlayCard" )
                    {
                        PlayCardsQueue.Add( new Data.Card(
                            int.Parse( recievedPacket.Variables[ "CardID" ] ),
                            recievedPacket.Variables[ "CardName" ],
                            int.Parse( recievedPacket.Variables[ "Health" ] ),
                            int.Parse( recievedPacket.Variables[ "Attack" ] ),
                            int.Parse( recievedPacket.Variables[ "EnergyCost" ] ),
                            recievedPacket.Variables[ "EffectType" ],
                            recievedPacket.Variables[ "Effect" ] ) );
                    }
                    else if( recievedPacket.Variables[ "PlayerAction" ] == "Attack" )
                    {
                        AttackingQueue.Add( int.Parse( recievedPacket.Variables[ "TargetMinionID" ] ),
                            int.Parse( recievedPacket.Variables[ "AttackingMinionID" ] ) );
                    }

                    break;
            }
        }
    }
}