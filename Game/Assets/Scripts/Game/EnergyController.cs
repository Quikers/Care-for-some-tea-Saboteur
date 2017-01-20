using UnityEngine;

public class EnergyController : MonoBehaviour
{

    public void GainEnergy( int amount )
    {
        if( Data.Player.CurrentEnergy >= 10 ) return;

        Data.Player.CurrentEnergy += amount;

    }
}