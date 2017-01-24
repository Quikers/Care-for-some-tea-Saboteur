using UnityEngine;

public class EnergyController : MonoBehaviour
{

    public GameObject EnergyObject;

    public void GainEnergy( int amount )
    {
        if( Data.Player.CurrentEnergy >= 10 ) return;

        Data.Player.CurrentEnergy += amount;

        for( int i = 0; i < amount; i++ )
        {
            GameObject spawnedObject = Instantiate( EnergyObject );
            spawnedObject.transform.SetParent( transform, false );
        }

    }
}