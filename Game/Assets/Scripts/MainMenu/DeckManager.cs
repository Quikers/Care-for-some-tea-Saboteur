using UnityEngine;
using System.Collections.Generic;

namespace MainMenu
{
    public class DeckManager : MonoBehaviour
    {
        [ SerializeField ] int _deckId;
        [ SerializeField ] string _deckName;

        public string DeckName
        {
            get { return _deckName; }
            set
            {
                GetComponentInChildren< UnityEngine.UI.Text >().text = value;
                _deckName = value;
            }
        }

        public void Instantiate( Data.Deck dataObject )
        {
            _deckId = dataObject.id;
            DeckName = dataObject.deckname;
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
            Data.Deck playerDeck = JsonUtility.FromJson<Data.Deck>( json );
            Data.Player.CurrentDeck = playerDeck ;
        }
    }
}