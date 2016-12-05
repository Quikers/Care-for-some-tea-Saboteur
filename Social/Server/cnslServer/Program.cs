using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using Library;
using System.Net;


namespace cnslServer
{

    //receive data port = 25002;
    //submit data port = 25003;
    class Program
    {
        private static List<Player> PlayerQueue;
        private static string ReceivedData;
        private static string PacketString;
        private static NetworkStream stream;


        static void Main(string[] args)
        {   
            PlayerQueue = new List<Player>();
            PlayerQueue.Add(new Player { UserID = 10, IP = "1.1.1.1" });

            Task Matchmaking = new Task(HandleMatchmaking);
            Matchmaking.Start();
            
            //Task AddPlayers = new Task(AddPlayersToQueue);
            //AddPlayers.Start();

            Task Listen = new Task(ListenTcp);
            Listen.Start();
            
            Console.ReadLine();
            
        }

        private static async void HandleMatchmaking()
        {
            while (true)
            {
                if (PlayerQueue.Count < 2) continue;

                Match match = new Match();
                match.player1 = PlayerQueue[0];
                match.player2 = PlayerQueue[1];

                StartMatch(match);
                Console.WriteLine("Match has been started!");
                PlayerQueue.Remove(match.player1);
                PlayerQueue.Remove(match.player2);
            }
        }

        private static async void AddPlayersToQueue()
        {
            int loopcount = 0;

            while (true)
            {
                if (loopcount == 200000000) loopcount = 0;
                else
                {
                    loopcount++;
                    continue;
                }

                Player player = new Player()
                {
                    UserID = loopcount,
                    SelectedDeck = null,
                    CurrentEnergy = 10,
                    MaxEnergy = 10
                };

                PlayerQueue.Add(player);
                //Console.WriteLine("Loopcount: 20000000");
            }
        }

        public void ReceiveData(Packet packet)
        {
            ReceivedData = packet.ToString();
        }

        private void AddPlayerToQueue(string Data)
        {

        }

        private static void StartMatch(Match match)
        {
            // Send response to player 1
            Packet packet = new Packet();
            packet.From = match.player2.IP;
            packet.To = match.player1.IP;
            packet.Type = TcpMessageType.MatchStart;
            var variables = new Dictionary<string, string>();
            variables.Add("User1ID", match.player1.UserID.ToString());
            variables.Add("User1IP", match.player1.IP);
            variables.Add("User2ID", match.player2.UserID.ToString());
            variables.Add("User2IP", match.player2.IP.ToString());
            packet.Variables = variables;

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(packet.ToString());

            stream.Write(msg, 0, packet.ToString().Length);

            // Send response to player 2
        }

        private static void Asynctesting()
        {
            //Asynctesting();
            Console.WriteLine("Het begint");
            long currentaantal = testasync().Result;
            Console.WriteLine(currentaantal.ToString());

            
        }

        private static long increase()
        {
            long currentaantal = 0;

            do
            {
                currentaantal++;
                Console.WriteLine(currentaantal.ToString());
            } while (currentaantal < 150);

            return currentaantal;
        }

        private static async Task<long> testasync()
        {
            Task<long> task = new Task<long>(increase);
            task.Start();
            long currentaantal = await task;

            return currentaantal;
        }

        private static void ListenTcp()
        {
            TcpListener server = null;

            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 25002;
                IPAddress localAddr = IPAddress.Parse("0.0.0.0");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine("TcpListener started. Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = new Packet(System.Text.Encoding.ASCII.GetString(bytes, 0, i)).ToString(); //Veranderd
                        
                        Console.WriteLine("Received: {0}", data);


                        // Process the data sent by the client.
                        //data = data.ToUpper();
                        Packet packet = PacketParser.Parse(data);
                        HandlePacket(packet);

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }

        private static void HandlePacket(Packet packet)
        {
            if (packet == null) return;
            

            switch (packet.Type)
            {
                case TcpMessageType.ChatMessage:
                    break;
                case TcpMessageType.Command:
                    break;
                case TcpMessageType.MapData:
                    break;
                case TcpMessageType.Message:
                    int from = int.Parse(packet.From);
                    int to = int.Parse(packet.To);
                    string message = packet.Variables["Message"];
                    string IPdestination = packet.Variables["IPdestination"];
                    break;

                case TcpMessageType.None:
                    break;
                case TcpMessageType.PlayerUpdate:
                    {
                        Player player = new Player();
                        player.UserID = int.Parse(packet.Variables["UserID"]);
                        player.CurrentEnergy = int.Parse(packet.Variables["CurrentEnergy"]);
                        player.MaxEnergy = int.Parse(packet.Variables["MaxEnergy"]);
                        
                        break;
                    }

                case TcpMessageType.AddPlayerToQueue:
                    {
                        string value = "";
                        Player playerr = new Player();
                        bool canReadd = packet.Variables.TryGetValue("UserID", out value);
                        if (canReadd)
                        {
                            playerr.UserID = int.Parse(value);
                            PlayerQueue.Add(playerr);
                            Console.WriteLine("UserID {0} has been added to the player queue", playerr.UserID.ToString());
                        }
                        break;
                    }

            }
        }

        private static void SendMessage(int from, int to, string message, string IPdestination)
        {
            throw new NotImplementedException();
        }
    }
}
