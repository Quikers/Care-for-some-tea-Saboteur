using UnityEngine;
using System.Collections.Generic;

namespace MainMenu
{
    public class DeckManager : MonoBehaviour
    {
        [ SerializeField ] int _deckId;
        [ SerializeField ] string _deckName;

        void FixedUpdate()
        {
            if( NetCode.NetworkController.GameStart )
                UnityEngine.SceneManagement.SceneManager.LoadScene( "main" );

        }
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
            DeckName = dataObject.name;
        }
        public void AddToQueue()
        {
            if( Data.PlayerUser.Id == 0 )
                return;
            Library.SendTcp.SendPacket( new Library.Packet( Data.PlayerUser.Id.ToString(), "Server", Library.TcpMessageType.AddPlayerToQueue, new[] { "Username", Data.PlayerUser.Username } ), Data.Network.ServerSocket );

            Resources.FindObjectsOfTypeAll<QueueInfoController>()[ 0 ].gameObject.SetActive( true );
        }

        public void BeginGameWithThisDeck()
        {
            string json = Utilities.Api.Deck.ByDeckId( _deckId );
            Data.Deck playerDeck = JsonUtility.FromJson<Data.Deck>( json );
            Data.Player.CurrentDeck = playerDeck;

            AddToQueue();
        }

        void GetDeck()
        {
        }
    }
}