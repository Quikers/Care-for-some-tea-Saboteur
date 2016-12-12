using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class SendTcp
    {
        public static void SendPacket(Packet packet)
        {   
            //string server = "127.0.0.1";
            //string server = "0.0.0.0";
            Int32 port = 25002;

            TcpClient client = new TcpClient(packet.To, port);

            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(packet.ToString());

            // Get a client stream for reading and writing.
            
            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);

            Console.WriteLine("Sent: {0}", packet.ToString());

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
        }
    }
}
