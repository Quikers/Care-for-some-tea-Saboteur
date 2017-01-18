using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckLoader : MonoBehaviour
{
    public GameObject DeckSelect;

    void OnEnable()
    {
        string json = Utilities.Api.Deck.ByUserId( Data.PlayerUser.Id );
        DeckData playerDeck = JsonUtility.FromJson< DeckData >( json );

        foreach( var Deck in playerDeck.data )
        {
            MainMenu.DeckManager deckManager =
                Instantiate( DeckSelect, Vector3.zero, Quaternion.identity ).GetComponent< MainMenu.DeckManager >();
            deckManager.transform.SetParent( transform, false );
            deckManager.Instantiate( Deck );
        }

    }

    struct DeckData
    {
        public List< Data.Deck > data;
    }
}