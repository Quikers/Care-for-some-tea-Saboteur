using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Library;

namespace SendTcp
{
    class Program
    {
        static Client client;

        static void Main(string[] args)
        {
            client = new Client
            {
                UserID = 3,
                Username = "Shifted",
                Socket = new TcpClient()
            };

            try
            {
                client.Socket.Connect("213.46.57.198", 25002);
                //client.Socket.Connect("127.0.0.1", 25002);
            }
            catch(SocketException se)
            {
                Console.WriteLine(se);
                Console.ReadLine();
            }
            

            Packet login = new Packet("3", "Server", TcpMessageType.Login, new[] { "UserID", "3", "Username", "Shifted" });
            Packet sendmsg = new Packet("3", "2", TcpMessageType.ChatMessage, new[] { "Chatmessage", "Hoi dit is mijn chatmessage" });
            Packet logout = new Packet("3", "Server", TcpMessageType.Logout, new Dictionary<string, string>());
            Packet addtoqueue = new Packet("3", "Server", TcpMessageType.AddPlayerToQueue);

            Library.SendTcp.SendPacket(login, client.Socket);
            
            //Library.SendTcp.SendPacket(logout, client.Socket);

            Thread thread = new Thread(() => Listen(client.Socket));
            thread.Start();

            do
            {
                string result = Console.ReadLine();
                if (result == "logout") Library.SendTcp.SendPacket(logout, client.Socket);
                if (result == "msg") Library.SendTcp.SendPacket(sendmsg, client.Socket);
                if (result == "match") Library.SendTcp.SendPacket(addtoqueue, client.Socket);
            } while (true);
            
           
        }

        private static void Listen(TcpClient client)
        {
            while (client.Connected)
            {
                Packet response = Library.SendTcp.ReceivePacket(client);
                if (response == null) continue;
                if (response.Type == TcpMessageType.MatchStart)
                {
                    Console.WriteLine("Match is starting with {0}", response.Variables["UserID"]);
                }
            }
        }
    }
}
