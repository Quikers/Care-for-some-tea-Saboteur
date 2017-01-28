using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Library
{
    public static class SendTcp
    {

        public static Packet ReceivePacket(TcpClient client)
        {
            // Buffer for reading data
            Byte[] bytes = new Byte[1024];
            String data = null;

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            int read = stream.Read(bytes, 0, bytes.Length);
            data = Encoding.ASCII.GetString(new List<byte>(bytes).GetRange(0, read).ToArray());

            if (data == null || data == "" || data == string.Empty) return null;

            //Console.WriteLine("Received: {0}", data);
            return new Packet(data);
        }


        public static void SendPacket(Packet packet, TcpClient client)
        {
            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = Encoding.ASCII.GetBytes(packet.ToString());

            // Get a client stream for reading and writing.
            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer. 
            stream.Write(data, 0, data.Length);

            //Console.WriteLine("Sent: {0}", packet.ToString());
        }
    }
}
