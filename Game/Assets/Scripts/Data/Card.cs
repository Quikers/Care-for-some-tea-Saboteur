using System.Reflection;

namespace Data
{
    [System.Serializable]
    public struct effectobject
    {
        public int id;
        public string type;
        public string effect;

        public effectobject( int id, string type, string effect )
        {
            this.id = id;
            this.type = type;
            this.effect = effect;
        }
    }

    [System.Serializable]
    public class Card
    {
        public int id;
        public int userid;
        public string name;
        public int cost;
        public effectobject effect;

        public int attack;
        public int health;
        public int activated;
        public int deleted;
        public string created;
        public string editted;

        public Card( int id, string name, int health, int attack, int cost, int effectid, string type, string effect )
        {
            this.id = id;
            this.name = name;
            this.health = health;
            this.attack = attack;
            this.cost = cost;
            this.effect = new effectobject( effectid, type, effect );
        }
    }
}