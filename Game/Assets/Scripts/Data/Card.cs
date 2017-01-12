namespace Data
{
    [System.Serializable]
    public struct effectobject
    {
        public int id;
        public string effect;
    }

    [System.Serializable]
    public class Card
    {
        public int id;
        public int userid;
        public string cardname;
        public string cardimage;
        public effectobject effect;

        public int attack;
        public int health;
        public int activated;
        public int deleted;
        public string created;
        public string editted;
    }
}