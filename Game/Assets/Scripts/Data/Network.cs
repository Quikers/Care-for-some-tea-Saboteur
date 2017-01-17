using System.Net.Sockets;

namespace Data
{
    public static class Network
    {
        public static TcpClient PlayerSocket;
        public static TcpClient ServerSocket = new TcpClient( "127.0.0.1", 25002 ); // 213.46.57.198
    }
}