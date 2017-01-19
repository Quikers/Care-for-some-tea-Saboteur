using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static int AmountOfCardsNextTurn;

        static int[ , ] cards = new int[ 2, 8 ];

        public static void SetPlayerCard( int index, int id )
        {
            Debug.Log( "CardId: " + id );
            if( index == 0)
                for( int i = 0; i < 7; i++ )
                {
                    if( cards[ 0, i + 1 ] != 0 )
                        cards[ 0, i + 1 ] = cards[ 0, i ];
                }

            cards[ 0, index ] = id;

            for( int i = 0; i < 7; i++ )
            {
                Debug.Log( "Card: " + i + " = " + cards[ 0, i ] );
            }
        }

        public static CardManager GetCard( bool playerCard, int index )
        {
            return Utilities.Find.CardById( cards[ playerCard ? 0 : 1, index ] );
        }

        //void LateUpdate()
        //{
        //    Rect canvasRect = new Rect( GetComponent<RectTransform>().rect );
        //    if( !canvasRect.Contains( Input.mousePosition ) )
        //    { }
        //}
    }
}