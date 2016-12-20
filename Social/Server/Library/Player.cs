using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

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
        public string ServerIP;
        public string LocalIP;

        public Player()
        {
            LocalIP = GetLocalIP();
        }

        public Player(string LocalIPAddress)
        {
            this.LocalIP = LocalIPAddress;
        }

        private string GetLocalIP()
        {
            //// First get the host name of local machine.
            //string strHostName = Dns.GetHostName();

            //// Then using host name, get the IP address list..
            //IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            //IPAddress[] addr = ipEntry.AddressList;

            //string localIP = null;
            //for (int i = 0; i < addr.Length; i++)
            //{
            //    if (addr[i].AddressFamily == AddressFamily.InterNetwork)
            //    {
            //        localIP = addr[i].ToString();
            //    }
            //}

            //return localIP;

            //Get Public IP
                try
                {
                    //Get string from api
                    String direction = "";
                    WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");

                    using (WebResponse response = request.GetResponse())
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        direction = stream.ReadToEnd();
                    }

                    //Search for the ip in the returned data
                    int first = direction.IndexOf("Address: ") + 9;
                    int last = direction.LastIndexOf("</body>");
                    direction = direction.Substring(first, last - first);

                    return direction;
                }
                catch(Exception e)
                {
                    throw new Exception(e.ToString());
                }
            }

        public void AddToQueue()
        {   //======Send Message
            //Create message
            Packet packet = new Library.Packet(this.UserID.ToString(), "Server", TcpMessageType.AddPlayerToQueue, new string[] {"UserID", this.UserID.ToString(), "IP", this.LocalIP });

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
