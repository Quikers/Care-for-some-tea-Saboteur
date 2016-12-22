using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
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

            client.Socket.Connect("213.46.57.198", 25002);

            Packet login = new Packet("3", "Server", TcpMessageType.Login, new[] { "UserID", "3", "Username", "Shifted" });
            Packet sendmsg = new Packet("3", "2", TcpMessageType.ChatMessage, new[] { "Chatmessage", "Hoi dit is mijn chatmessage" });
            Packet logout = new Packet("3", "Server", TcpMessageType.Logout, new Dictionary<string, string>());
            
            Library.SendTcp.SendPacket(login, client.Socket);
            Library.SendTcp.SendPacket(sendmsg, client.Socket);
            //Library.SendTcp.SendPacket(logout, client.Socket);

            Packet response = Library.SendTcp.ReceivePacket(client.Socket);
            if (response.Type == TcpMessageType.MatchStart)
            {
                Console.WriteLine("Match is starting with {0}", response.Variables["UserID"]);
            } 

            Console.ReadLine();
           
        }

        //private static void SendTcp(Packet packet)
        //{
        //    string ip = string.Empty;

        //    if(packet.To == "Server" || packet.To == "server")
        //    {
        //        ip = "213.46.57.198";
        //        //ip = "127.0.0.1";
        //        //string server = "0.0.0.0";
        //    }
        //    else
        //    {
        //        ip = packet.To;
        //    }

        //    // Create a TcpClient.
        //    // Note, for this client to work you need to have a TcpServer 
        //    // connected to the same address as specified by the server, port
        //    // combination.



        //    Int32 port = 25002;

        //    TcpClient client = new TcpClient(ip, port);

        //    // Translate the passed message into ASCII and store it as a Byte array.
        //    Byte[] data = System.Text.Encoding.ASCII.GetBytes(packet.ToString());

        //    // Get a client stream for reading and writing.
        //    //  Stream stream = client.GetStream();

        //    NetworkStream stream = client.GetStream();

        //    // Send the message to the connected TcpServer. 
        //    stream.Write(data, 0, data.Length);

        //    Console.WriteLine("Sent: {0}", packet.ToString());

        //    // Receive the TcpServer.response.

        //    // Buffer to store the response bytes.
        //    data = new Byte[256];

        //    // String to store the response ASCII representation.
        //    String responseData = String.Empty;

        //    // Read the first batch of the TcpServer response bytes.
        //    Int32 bytes = stream.Read(data, 0, data.Length);
        //    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
        //    Console.WriteLine("Received: {0}", responseData);

        //    // Close everything.
        //    stream.Close();
        //    client.Close();// Create a TcpClient.
        //                   // Note, for this client to work you need to have a TcpServer 
        //                   // connected to the same address as specified by the server, port
        //                   // combination.
        //}
    }
}
