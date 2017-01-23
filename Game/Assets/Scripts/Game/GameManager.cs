using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {

        public static CardManager GetCard( bool playerCard, int id )
        {
            return Utilities.Find.CardById( id );
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