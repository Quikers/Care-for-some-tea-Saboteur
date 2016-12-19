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
        private static Packet Response;
        private static NetworkStream stream;
        private static Dictionary<int, Client> OnlinePlayers;


        static void Main(string[] args)
        {
            OnlinePlayers = new Dictionary<int, Client>();

            PlayerQueue = new List<Player>();
            PlayerQueue.Add(new Player("1.1.1.1") { UserID = 10});

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
                Console.WriteLine("Match has been started between UserID {0} and {1}", match.player1.UserID, match.player2.UserID);
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
        
        private static void StartMatch(Match match)
        {
            //player 1 wordt automatisch in de code aangemaakt voor test purposes

            //// Send response to player 1
            //Packet packet1 = new Packet();
            //packet1.From = match.player2.UserID.ToString();
            //packet1.To = match.player1.UserID.ToString();
            //packet1.Type = TcpMessageType.MatchStart;
            //var variables = new Dictionary<string, string>();
            //variables.Add("User1ID", match.player1.UserID.ToString());
            //variables.Add("User1IP", match.player1.IP);
            //variables.Add("User2ID", match.player2.UserID.ToString());
            //variables.Add("User2IP", match.player2.IP.ToString());
            //packet1.Variables = variables;

            //byte[] msg1 = System.Text.Encoding.ASCII.GetBytes(packet1.ToString());
            //stream.Write(msg1, 0, packet1.ToString().Length);

            // Send response to player 2
            try
            {
                Packet packet2 = new Packet();
                packet2.From = match.player2.LocalIP;
                packet2.To = match.player1.LocalIP;
                packet2.Type = TcpMessageType.MatchStart;
                var variables2 = new Dictionary<string, string>();
                variables2.Add("User1ID", match.player1.UserID.ToString());
                variables2.Add("User1IP", match.player1.LocalIP);
                variables2.Add("User2ID", match.player2.UserID.ToString());
                variables2.Add("User2IP", match.player2.LocalIP.ToString());
                
                packet2.Variables = variables2;

                byte[] msg2 = System.Text.Encoding.ASCII.GetBytes(packet2.ToString());
                stream.Write(msg2, 0, packet2.ToString().Length);
            }
            catch
            {
                Console.WriteLine("Unable to connect players. Check if the Player.UserID and Player.IP are not empty.");
            }
            
        }

        private static void ListenTcp()
        {
            TcpListener server = null;

            try
            {
                // Initialize port & local IP
                Int32 port = 25002;
                IPAddress localAddr = IPAddress.Parse("0.0.0.0");
                
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
                    Console.WriteLine("Incoming connection detected.");
                    Response = null;
                    data = null;

                    //Console.WriteLine(">>>>>>>>>>>>>" + client.Client.RemoteEndPoint.ToString());

                    // Get a stream object for reading and writing
                    stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = new Packet(System.Text.Encoding.ASCII.GetString(bytes, 0, i)).ToString();
                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client.
                        HandlePacket(new Packet(data), new Client {Socket = client });

                        // Send back a response.
                        if(Response != null)
                        {
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(Response.ToString());
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine("Sent: {0}", Response.ToString());
                        }
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

        private static void HandlePacket(Packet packet, Client client)
        {
            if (packet == null) return;
            Packet response = new Packet();
            response.Type = TcpMessageType.Response;
            

            try
            {
                switch (packet.Type)
                {
                    case TcpMessageType.ChatMessage:
                        {
                            string from = packet.From;
                            string to = packet.To;
                            string chatmessage = packet.Variables["Chatmessage"];
                            string IPdestination = packet.Variables["IPdestination"];

                            SendSuccessResponse(packet);
                            //response.From = from;
                            //response.To = to;
                            //response.Variables = 
                            break;
                        }

                    case TcpMessageType.Command:
                        break;

                    case TcpMessageType.MapData:
                        break;

                    case TcpMessageType.Message:
                        {
                            int from = int.Parse(packet.From);
                            int to = int.Parse(packet.To);
                            string message = packet.Variables["Message"];
                            string IPdestination = packet.Variables["IPdestination"];
                            break;
                        }

                    case TcpMessageType.None:
                        break;

                    case TcpMessageType.PlayerUpdate:
                        {
                            Player player = new Player();
                            player.UserID = int.Parse(packet.Variables["UserID"]);
                            player.CurrentEnergy = int.Parse(packet.Variables["CurrentEnergy"]);
                            player.MaxEnergy = int.Parse(packet.Variables["MaxEnergy"]);
                            player.CurrentHealth = int.Parse(packet.Variables["CurrentHealth"]);
                            player.MaxHealth = int.Parse(packet.Variables["MaxHealth"]);
                            break;
                        }

                    case TcpMessageType.AddPlayerToQueue:
                        {
                            Player player = new Player();
                            player.UserID = int.Parse(packet.Variables["UserID"]);
                            player.LocalIP = packet.Variables["IP"];
                            PlayerQueue.Add(player);
                            Console.WriteLine("UserID {0} has been added to the player queue", player.UserID.ToString());
                            //Response = new Packet("Server", player.LocalIP, TcpMessageType.Response, new string[] {"Operation", "AddPlayerToQueue", "Result", "Success" });
                            //SendTcp.SendPacket(Response);
                            SendSuccessResponse(packet);
                            break;
                        }
                    case TcpMessageType.Login:
                        {
                            Client _client = new Client
                            {
                                UserID = int.Parse(packet.Variables["UserID"]),
                                Username = packet.Variables["Username"],
                                Socket = client.Socket
                                
                            };
                            if (!OnlinePlayers.ContainsKey(_client.UserID))
                            OnlinePlayers.Add(_client.UserID , _client);

                            //OnlinePlayers.Add(packet.Variables["UserID"]);
                            break;
                        }
                    case TcpMessageType.Logout:
                        {
                            client.Socket.Close();
                            OnlinePlayers.Remove(client.UserID);
                            break;
                        }
                }
            }
            catch
            {
                Console.WriteLine("Could not handle packet. Please check the syntax.");
                return;
            }
            
        }

        private static void SendSuccessResponse(Packet ReceivedPacket)
        {
            Packet packet = new Packet("Server", ReceivedPacket.From, TcpMessageType.Response, new[] { "TcpMessageType", ReceivedPacket.Type.ToString() });
            SendTcp.SendPacket(packet);
        }
    }
}
