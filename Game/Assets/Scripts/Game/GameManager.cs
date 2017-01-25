using System.Linq;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static CardManager GetCard( bool playerCard, int id )
        {
            return Utilities.Find.CardById( id );
        }



        void Update()
        {
            if( NetCode.NetworkController.AttackingQueue.Count >= 1 )
            {
                foreach( var attacker in NetCode.NetworkController.AttackingQueue )
                {
                    Debug.Log( attacker.Value + "  " + attacker.Key );

                    Debug.Log( Utilities.Find.CardById( attacker.Value ) );
                    Debug.Log( Utilities.Find.CardById( attacker.Key ) );

                    Utilities.Find.CardById( attacker.Value )
                        .GetComponent< EnemyCardController >()
                        .Attack( Utilities.Find.CardById( attacker.Value ), Utilities.Find.CardById( attacker.Key ) );

                    NetCode.NetworkController.AttackingQueue.Remove( attacker.Key );

                }
            }

            if( NetCode.NetworkController.PlayCardsQueue.Count >= 1 )
            {
                foreach( Data.Card card in NetCode.NetworkController.PlayCardsQueue )
                {
                    FindObjectOfType<DeckManager>().PlayEnemyCard( card );
                    NetCode.NetworkController.PlayCardsQueue.Remove( card );
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