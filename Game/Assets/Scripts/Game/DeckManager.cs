using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class DeckManager : MonoBehaviour
{

    void Start()
    {}

    void Update()
    {}

    void OnEnable()
    {
        string[] fromJsonDeck = JsonUtility.FromJson< string[] >( Utilities.Api.Deck.GetDeckByUserId( Data.User.Id ) );
        Debug.Log( Utilities.Api.Deck.GetDeckByUserId( Data.User.Id ) );
        Debug.Log( fromJsonDeck );
    }
}