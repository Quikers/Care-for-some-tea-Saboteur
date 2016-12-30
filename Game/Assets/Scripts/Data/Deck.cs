using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    struct Deck
    {
        public int DeckId;
        public string DeckName;
        public List< Card > Cards;
    }
}
