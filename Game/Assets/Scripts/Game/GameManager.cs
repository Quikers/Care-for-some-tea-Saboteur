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
            if( NetCode.NetworkController.PlayCardsQueue.Count < 1 ) return;

            foreach( Data.Card card in NetCode.NetworkController.PlayCardsQueue )
            {
                FindObjectOfType<DeckManager>().PlayEnemyCard( card );
                NetCode.NetworkController.PlayCardsQueue.Remove( card );
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