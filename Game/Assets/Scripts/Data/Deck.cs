using System.Collections.Generic;

namespace Data
{
    [System.Serializable]
    public struct Deck
    {
        public int id;
        public int userid;
        public string deckname;
        public int activated;
        public int deleted;
        public string created;
        public string editted;
        public List<Card> cards;
    }
}
