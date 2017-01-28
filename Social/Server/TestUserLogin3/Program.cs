using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Library;

namespace TestUserLogin3
{
    class Program
    {
        static Client client;
        static int targetUserID = 2;
        static int interval = 103;

        static void Main(string[] args)
        {
            client = new Client
            {
                UserID = 13,
                Username = "TestUser3",
                Socket = new TcpClient()
            };

            try
            {
                //client.Socket.Connect("213.46.57.198", 25002);
                client.Socket.Connect("127.0.0.1", 25002);
                //client.Socket.Connect("172.16.80.168", 25002);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se);
                Console.ReadLine();
            }


            Packet login = new Packet(client.UserID.ToString(), "Server", TcpMessageType.Login, new[] { client.UserID.ToString(), targetUserID.ToString(), "Username", client.Username.ToString() });
            Packet sendmsg = new Packet(client.UserID.ToString(), targetUserID.ToString(), TcpMessageType.ChatMessage, new[] { "Chatmessage", "Hoi dit is mijn chatmessage" });
            Packet logout = new Packet(client.UserID.ToString(), "Server", TcpMessageType.Logout, new Dictionary<string, string>());
            Packet addtoqueue = new Packet(client.UserID.ToString(), "Server", TcpMessageType.AddPlayerToQueue);

            Thread listen = new Thread(() => Listen(client.Socket));
            listen.Start();

            Library.SendTcp.SendPacket(login, client.Socket);

            //Library.SendTcp.SendPacket(logout, client.Socket);

            Thread stresstest = new Thread(() => StressTest());

            do
            {
                string result = Console.ReadLine();
                if (result == "logout") Library.SendTcp.SendPacket(logout, client.Socket);
                if (result == "msg") Library.SendTcp.SendPacket(sendmsg, client.Socket);
                if (result == "match") Library.SendTcp.SendPacket(addtoqueue, client.Socket);
                if (result == "stresstest")
                {
                    StressTest();
                }

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

        private static void StressTest()
        {
            while (true)
            {
                Packet sendmsg = new Packet(client.UserID.ToString(), targetUserID.ToString(), TcpMessageType.ChatMessage, new[] { "Chatmessage", "Hoi dit is mijn chatmessage" });
                Library.SendTcp.SendPacket(sendmsg, client.Socket);
                Thread.Sleep(interval);
            }
        }
    }
}
