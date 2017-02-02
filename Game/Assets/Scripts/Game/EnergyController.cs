using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{

    public GameObject EnergyObject;

    public List< GameObject > EnergyObjects = new List< GameObject >();

    void Start()
    {
        GameObject spawnedObject = Instantiate( EnergyObject );
        EnergyObjects.Add( spawnedObject );
        spawnedObject.transform.SetParent( transform, false );
    }

    public void GainEnergy( int amount )
    {
        if( Data.Player.GlobalEnergy >= 10 ) return;
        Data.Player.GlobalEnergy += amount;
        Data.Player.CurrentEnergy = Data.Player.GlobalEnergy;

        for( int i = 0; i < amount; i++ )
        {
            GameObject spawnedObject = Instantiate( EnergyObject );
            EnergyObjects.Add( spawnedObject );
            spawnedObject.transform.SetParent( transform, false );
        }
    }

    public void UseEnergy( int amount )
    {
        for( int i = 0; i < amount; i++ )
        {
            EnergyObjects[ i ].SetActive( false );
        }
        Data.Player.CurrentEnergy -= amount;
    }
}