﻿using System;
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
        static void Main(string[] args)
        {
            Packet packet = new Packet("Sjoerd", "Server", TcpMessageType.AddPlayerToQueue, new[] { "UserID", "3"});

            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer 
            // connected to the same address as specified by the server, port
            // combination.

            //string server = "213.46.57.198";
            string server = "127.0.0.1";
            //string server = "0.0.0.0";
            Int32 port = 25002;
            
            string message = "Gelukt!";

            TcpClient client = new TcpClient(server, port);

            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(packet.ToString());

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", message);

            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);

            // Close everything.
            stream.Close();
            client.Close();// Create a TcpClient.
                           // Note, for this client to work you need to have a TcpServer 
                           // connected to the same address as specified by the server, port
                           // combination.

            Console.ReadLine();
           
        }
    }
}
