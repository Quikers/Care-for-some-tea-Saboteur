using UnityEngine;
using Newtonsoft.Json;

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
            //UnityEngine.SceneManagement.SceneManager.LoadScene( "main" );
            string json = Utilities.Api.Deck.ByDeckId( _deckId );
            Debug.Log( json );

            ArrayEntry temp = JsonConvert.DeserializeObject<ArrayEntry>( json );
            Debug.Log( temp.deckname );
        }
        
        [System.Serializable]
        public struct ArrayEntry
        {
            public string id;
            public string userid;
            public string deckname;
            public string activated;
            public string deleted;
            public string created;
            public string editted;
            public Card[] Cards;
        }
        [System.Serializable]
        public class Card
        {
            public string id;
            public string energyCost;
            public string effect;

            public string currentAttack;
            public string maxAttack;
        }
    }
}