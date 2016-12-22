using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Net.Sockets;

namespace TestUserLogin
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client
            {
                UserID = 3,
                Username = "Shifted",
                Socket = new TcpClient()
            };

            client.Socket.Connect("213.46.57.198", 25002);

            Packet login = new Packet("Sjoerd", "Server", TcpMessageType.Login, new[] { "UserID", "3", "Username", "Shifted"});
            Packet logout = new Packet("3", "Server", TcpMessageType.Logout, new Dictionary<string, string>());

            SendTcp.SendPacket(login, client.Socket);
            Console.WriteLine("Proberen in te loggen. Er zal geen reactie komen. Type logout om weer uit te loggen.");
            if (Console.ReadLine() == "logout") SendTcp.SendPacket(logout, client.Socket);
            
        }
    }
}
