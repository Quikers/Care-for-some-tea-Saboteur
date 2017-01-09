using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Net.Sockets;
using System.Threading;

namespace TestUserLogin
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client
            {
                UserID = 2,
                Username = "Quikers",
                Socket = new TcpClient()
            };

            AppDomain.CurrentDomain.ProcessExit += (o,e) => CurrentDomain_ProcessExit(null, null, client);

            client.Socket.Connect("213.46.57.198", 25002);

            Packet login = new Packet(client.UserID.ToString(), "Server", TcpMessageType.Login, new[] { "UserID", client.UserID.ToString(), "Username", client.Username });
            Packet logout = new Packet(client.UserID.ToString(), "Server", TcpMessageType.Logout, new Dictionary<string, string>());
            Packet chatMessage = new Packet(client.UserID.ToString(), "3", TcpMessageType.ChatMessage, new[] { "Chatmessage", "Hallo dit is een chatmessage van " + client.Username });
            Packet queue = new Packet(client.UserID.ToString(), "Server", TcpMessageType.AddPlayerToQueue, new Dictionary<string, string>());

            Thread listen = new Thread(() => ListenForMessages(client.Socket));
            listen.Start();

            Console.WriteLine("Client started, waiting for input...");

            bool isActive = true;
            while (isActive) {
                string result = Console.ReadLine();

                switch (result) {
                    default:
                        Console.WriteLine("Command \"" + result + "\" was not recognized.");
                        break;
                    case "login":
                        SendTcp.SendPacket(login, client.Socket);
                        break;
                    case "logout":
                        SendTcp.SendPacket(logout, client.Socket);
                        while (client.Socket.Connected) client.Socket.Close();
                        break;
                    case "chatmessage":
                        SendTcp.SendPacket(chatMessage, client.Socket);
                        break;
                    case "queue":
                        SendTcp.SendPacket(queue, client.Socket);
                        break;
                    case "exit": case "quit": case "close":
                        isActive = false;
                        break;
                }
            }

            SendTcp.SendPacket(logout, client.Socket);
            while (client.Socket.Connected) client.Socket.Close();
        }

        private static void ListenForMessages(TcpClient client) {
            while (client.Connected) {
                Packet packet = new Packet();

                try {
                    packet = SendTcp.ReceivePacket(client);
                    if (packet == null) continue;

                    switch (packet.Type) {
                        default:
                            Console.WriteLine("Packet type \"" + packet.Type + "\" was not recognized.");
                            break;
                        case TcpMessageType.ChatMessage:
                            Console.WriteLine("\n" + packet.From + ": " + packet.Variables["Chatmessage"] + "\n");
                            break;
                        case TcpMessageType.MatchStart:
                            Console.WriteLine("The match has started!");
                            break;
                    }
                } catch (Exception ex) {
                    Console.WriteLine("\n" + packet + "\n\n" + ex + "\n");

                    while (client.Connected) client.Close();
                }
            }
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e, Client client) {
            SendTcp.SendPacket(new Packet(client.UserID.ToString(), "Server", TcpMessageType.Logout, new Dictionary<string, string>()), client.Socket);
            while (client.Socket.Connected) client.Socket.Close();
        }
    }
}
