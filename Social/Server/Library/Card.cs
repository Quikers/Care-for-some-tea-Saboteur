using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class Card
    {
        public int ID;
        public int EnergyCost;
        Effect Effect;
        
    }

    public enum Effect
    {
        Battlecry, Deathrattle
    }
}
