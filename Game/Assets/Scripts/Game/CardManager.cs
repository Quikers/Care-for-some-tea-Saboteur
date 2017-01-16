using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public int CardId;
    public string Cardname;
    public int Cardcost;
    public Data.effectobject Effect;

    public int Attack;
    public int Health;

    public Text CardCost;
    public Text CardTitle;
    public Text CardDiscr;
    public Text CardHealth;
    public Text CardEnergy;

    public void Instantiate( Data.Card dataObject )
    {
        CardId = dataObject.id;
        Cardname = dataObject.cardname;
        Cardcost = dataObject.cost;
        Effect = dataObject.effect;
        Attack = dataObject.attack;
        Health = dataObject.health;

        CardTitle.text = Cardname;
        CardCost.text = Cardcost.ToString();
        CardDiscr.text = Effect.effect;
        CardHealth.text = Attack.ToString();
        CardEnergy.text = Health.ToString();
    }
}