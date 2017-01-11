using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public struct Deck
    {
        public string id;
        public string userid;
        public string deckname;
        public string activated;
        public string deleted;
        public string created;
        public string editted;
        public Card[] Cards;
    }
}
