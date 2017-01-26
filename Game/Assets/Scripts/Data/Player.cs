using System.Net.Sockets;

namespace Data
{
    public struct Player
    {
        public static TcpClient PlayerClient;

        public static Deck CurrentDeck;
        public static int CurrentEnergy = 1;

        public static int CurrentHealth = 20;
    }
}