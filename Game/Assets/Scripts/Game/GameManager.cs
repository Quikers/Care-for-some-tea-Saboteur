using System.Linq;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        CardManager GetCard( int id )
        {
            return Utilities.Find.CardById( id );
        }

        public static void ActiveAllCards()
        {
            CardAttackController[] attackingCards = FindObjectsOfType< CardAttackController >();
            foreach( var attackingCard in attackingCards )
            {
                attackingCard.HasAttacked = false;
            }
        }

        void Update()
        {
            if( NetCode.NetworkController.PlayCardsQueue.Count >= 1 )
            {
                for( int i = 0; i < NetCode.NetworkController.PlayCardsQueue.Count; i++ )
                {
                    FindObjectOfType<DeckManager>().PlayEnemyCard( NetCode.NetworkController.PlayCardsQueue[ i ] );
                    NetCode.NetworkController.PlayCardsQueue.RemoveAt( i );
                }
            }

            if( NetCode.NetworkController.AttackingQueue.Count >= 1 )
            {
                foreach( var attacker in NetCode.NetworkController.AttackingQueue.ToArray() )
                {
                    Debug.Log( attacker.Value + "  " + attacker.Key );


                    if( attacker.Key >= 0 )
                    {
                        Debug.Log( Utilities.Find.CardById( attacker.Value ) );
                        Debug.Log( Utilities.Find.CardById( attacker.Key ) );

                        GetCard( attacker.Value )
                            .GetComponent< EnemyCardController >()
                            .Attack( GetCard( attacker.Value ), GetCard( attacker.Key ) );
                    }
                    else if( attacker.Key == -1 )
                    {
                        FindObjectsOfType< FaceController >()[ 0 ]
                            .Attack( GetCard( attacker.Value ) );
                    }
                    else if( attacker.Key == -2 )
                    {
                        FindObjectsOfType< FaceController >()[ 1 ]
                            .Attack( GetCard( attacker.Value ) );

                    }
                    NetCode.NetworkController.AttackingQueue.Remove( attacker.Key );
                }
            }            
        }

        void OnApplicationQuit()
        {
            Library.SendTcp.SendPacket( new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.Logout ), Data.Network.ServerSocket );
        }

        //void LateUpdate()
        //{
        //    Rect canvasRect = new Rect( GetComponent<RectTransform>().rect );
        //    if( !canvasRect.Contains( Input.mousePosition ) )
        //    { }
        //}
    }
}