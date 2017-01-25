using System.Linq;
using UnityEngine;

namespace Game
{
    public class DeckManager : MonoBehaviour
    {
        public GameObject Card;
        public GameObject NonPlayerCard;

        public Transform EnemyBoardDrop;

        int[] DrawnCards = new int[ 20 ];

        bool _startCards;
        void LateUpdate()
        {
            if( Data.Player.CurrentDeck.name == null | _startCards ) return;

            for( int i = 0; i < 3; i++ )
            {
                DrawCard();
                _startCards = true;
            }
        }

        bool IsInHand( int index )
        {
            return DrawnCards.Any( t => t == index );
        }
        
        public void PlayEnemyCard( Data.Card playedCard )
        {
            GameObject card = Instantiate( NonPlayerCard, Vector3.zero, Quaternion.identity );

            card.GetComponent<CardManager>().InstantiateCard( playedCard );
            card.transform.SetParent( EnemyBoardDrop, false );
        }

        public void DrawCard()
        {
            //Instantiate( Card, Vector3.zero, Quaternion.identity );
            if( transform.childCount > 8 ) return;

            float randomNum = Random.Range( 0, 19 );
            while( IsInHand( ( int )randomNum ) ) randomNum = Random.Range( 0, 19 );
            Data.Card drawnCard = Data.Player.CurrentDeck.cards[ ( int )randomNum ];

            GameObject card = Instantiate( Card, Vector3.zero, Quaternion.identity );

            card.GetComponent<CardManager>().InstantiateCard( drawnCard );
            card.transform.SetParent( transform, false );
        }
        
    }
}