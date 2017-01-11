using System;

namespace Data
{
    [Serializable]
    enum EffectType
    {
        Attack,
        Battlecry,
        Deathrattle,
        Healing,
        CanNotAttack
    }

    [Serializable]
    public class Card
    {
        public string id;
        public string energyCost;
        public string effect;

        public string currentAttack;
        public string maxAttack;
    }
}
