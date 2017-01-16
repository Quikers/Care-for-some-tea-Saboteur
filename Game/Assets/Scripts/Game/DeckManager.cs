using System.Linq;
using UnityEngine;

namespace Game
{
    public class DeckManager : MonoBehaviour
    {
        public GameObject Card;

        int[] DrawnCards = new int[ 20 ];

        bool _startCards;
        void LateUpdate()
        {
            
            if( Data.Player.CurrentDeck.deckname == null | _startCards ) return;
            Debug.Log( Data.Player.CurrentDeck.deckname );
            Debug.Log( _startCards );

            for( int i = 0; i < 3; i++ )
            {
                DrawCard();
                _startCards = true;
            }
        }

        bool IsInHand(int index)
        {
            return DrawnCards.Any( t => t == index );
        }

        public void DrawCard()
        {
            if( transform.childCount > 8 ) return;

            float randomNum = Random.Range( 0, 19 );
            while( IsInHand( ( int )randomNum ) ) randomNum = Random.Range( 0, 19 );
            Data.Card drawnCard = Data.Player.CurrentDeck.cards[ ( int )randomNum ];

            CardManager card = Instantiate( Card, Vector3.zero, Quaternion.identity ).GetComponent<CardManager>();

            card.Instantiate( drawnCard );
            card.transform.SetParent( transform, false );
        }
        
    }
}