using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public int CardId;
    public string cardname;
    public int cardCost;
    public Data.effectobject effect;
    public int attack;
    public int health;

    void Update()
    {
        if( health <= 0 )
            Destroy( gameObject );
    }

    public string Cardname
    {
        get { return cardname; }
        set
        {
            cardname = value;
            CardTitleText.text = cardname;
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
            effect = value;
            CardDiscrText.text = effect.effect;
        }
    }

    public int Attack
    {
        get { return attack; }
        set
        {
            attack = value;
            CardAttackText.text = attack.ToString();
        }
    }

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            CardHealthText.text = health.ToString();
        }
    }

    public Text CardCostText;
    public Text CardTitleText;
    public Text CardDiscrText;
    public Text CardHealthText;
    public Text CardAttackText;
    public Image CardImage;

    public void InstantiateCard( Data.Card dataObject )
    {
        CardId = dataObject.id;
        Cardname = dataObject.name;
        CardCost = dataObject.cost;
        Effect = dataObject.effect;
        Attack = dataObject.attack;
        Health = dataObject.health;

        Sprite sprite = new Sprite();

        if( CardCost <= 2 )
            sprite = Resources.Load<Sprite>( "0" );
        else if( CardCost <= 4 )
            sprite = Resources.Load<Sprite>( "2" );
        else if( CardCost <= 6)
            sprite = Resources.Load<Sprite>( "4" );
        else if( CardCost <= 8 )
            sprite = Resources.Load<Sprite>( "6" );
        else if( CardCost <= 10 )
            sprite = Resources.Load<Sprite>( "8" );

        CardImage.sprite = sprite;
    }

    public void DoEffect()
    {
        if( Effect.id > 0 )
            Attack += Effect.id;
    }
}