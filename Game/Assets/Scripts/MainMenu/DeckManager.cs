using UnityEngine;
using System.Collections.Generic;

namespace MainMenu
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] int _deckId;
        [SerializeField] string _deckName;

        public string DeckName
        {
            get { return _deckName; }
            set
            {
                GetComponentInChildren<UnityEngine.UI.Text>().text = value;
                _deckName = value;
            }
        }

        public void BeginGameWithThisDeck()
        {
            System.Threading.Thread test = new System.Threading.Thread( GetDeck );
            test.Start();

            UnityEngine.SceneManagement.SceneManager.LoadScene( "main" );

        }

        void GetDeck()
        {
            string json = Utilities.Api.Deck.ByDeckId( _deckId );
            deckdata playerDeck = JsonUtility.FromJson<deckdata>( json );
            Data.Player.CurrentDeck = playerDeck.data[ 0 ];
        }

        [System.Serializable]
        public class deckdata
        {
            public List<Data.Deck> data;
        }
    }
}