using System.Net.Sockets;

namespace Data
{
    struct Player
    {
        public TcpClient PlayerClient;
        public Deck CurrentDeck;

    }
}