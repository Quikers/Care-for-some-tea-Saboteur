using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace TestUserLogin
{
    class Program
    {
        static void Main(string[] args)
        {
            Packet login = new Packet("test user", "Server", TcpMessageType.Login, new[] { "UserID", "2", "Username", "Quikers"});
            Packet logout = new Packet("2", "Server", TcpMessageType.Logout, new Dictionary<string, string>());

            SendTcp.SendPacket(login);
            Console.WriteLine("Proberen in te loggen. Er zal geen reactie komen. Type logout om weer uit te loggen.");
            if (Console.ReadLine() == "logout") SendTcp.SendPacket(logout);
            
        }
    }
}
