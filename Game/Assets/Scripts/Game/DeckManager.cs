using UnityEngine;

namespace Game
{
    public class DeckManager : MonoBehaviour
    {
        Data.Deck PlayerDeck;
        int[] DrawnCards = new int[ 20 ];

        void Start()
        {
            PlayerDeck = Data.Player.CurrentDeck;
            for( int i = 0; i < 3; i++ )
            {
                DrawCard();
            }
        }

        void Update()
        {
            Debug.Log( Data.Player.CurrentDeck.deckname );
        }

        void DrawCard()
        {
            
        }
        
    }
}