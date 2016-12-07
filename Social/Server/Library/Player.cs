using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Library
{
    public class Player
    {
        public int UserID;
        public List<Card> SelectedDeck;
        public int CurrentEnergy;
        public int MaxEnergy;
        public int CurrentHealth;
        public int MaxHealth;
        public string IP;

        public void StartTurn()
        {

        }

        public void EndTurn()
        {

        }

        public void AddToQueue()
        {   //======Send Message
            //Create message
            Packet packet = new Library.Packet(this.UserID.ToString(), "Server", TcpMessageType.AddPlayerToQueue, new string[] {"UserID", this.UserID.ToString(), "IP", this.IP });

            //Assign destination
            TcpClient client = new TcpClient("213.46.57.198", 25002);

            //Create package
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(packet.ToString());

            //Open stream
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);

            //=====Receive response

            // Buffer to store the response bytes.
            data = new Byte[256];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            

            // Close everything.
            stream.Close();
            client.Close();
        }
    }
}
