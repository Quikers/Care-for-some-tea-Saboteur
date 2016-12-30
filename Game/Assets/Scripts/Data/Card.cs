using System;

namespace Data
{
    enum EffectType
    {
        Battlecry,
        Deathrattle,
        Healing,
        CanNotAttack
    }

    class Card
    {
        int id;
        int energyCost;
        EffectType[] effect;

        int currentAttack;
        int maxAttack;
    }
}
