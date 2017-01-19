using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public int CardId;
    string cardname;
    int cardCost;
    Data.effectobject effect;
    int attack;
    int health;

    public string Cardname
    {
        get { return cardname; }
        set
        {
            CardTitleText.text = value;
            cardname = value;
        }
    }

    public int CardCost
    {
        get { return cardCost; }
        set
        {
            cardCost = value;
            CardCostText.text = cardCost.ToString();

        }
    }

    public Data.effectobject Effect
    {
        get { return effect; }
        set
        {
            CardDiscrText.text = value.effect;
            effect = value;
        }
    }

    public int Attack
    {
        get { return attack; }
        set
        {
            CardAttackText.text = value.ToString();
            attack = value;
        }
    }

    public int Health
    {
        get { return health; }
        set
        {
            CardHealthText.text = value.ToString();
            health = value;
        }
    }

    public Text CardCostText;
    public Text CardTitleText;
    public Text CardDiscrText;
    public Text CardHealthText;
    public Text CardAttackText;

    public void InstantiateCard( Data.Card dataObject )
    {
        CardId = dataObject.id;
        Cardname = dataObject.name;
        CardCost = dataObject.cost;
        Effect = dataObject.effect;
        Attack = dataObject.attack;
        Health = dataObject.health;
    }
}