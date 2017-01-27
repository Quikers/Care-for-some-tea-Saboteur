using System.Net.Sockets;

namespace Data
{
    public struct Player
    {
        public static TcpClient PlayerClient;

        public static Deck CurrentDeck;
        public static int CurrentEnergy = 1;
        public static int GlobalEnergy = 1;

        public static int CurrentHealth = 1;
    }

    public struct Enemy
    {
        public static int CurrentHealth = 1;
    }
}