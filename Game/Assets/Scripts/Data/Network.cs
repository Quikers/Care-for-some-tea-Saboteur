using System.Net.Sockets;

namespace Data
{
    public static class Network
    {
        public static TcpClient PlayerSocket;
        public static TcpClient ServerSocket = new TcpClient( "213.46.57.198", 25002 ); // 10.8.0.1
    }
}