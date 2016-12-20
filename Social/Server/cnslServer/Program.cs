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
        private static Dictionary<int, Client> PlayerQueue;
        private static Packet Response;
        private static NetworkStream stream;
        private static Dictionary<int, Client> OnlinePlayers;


        static void Main(string[] args)
        {   
            OnlinePlayers = new Dictionary<int, Client>();
            
            //PlayerQueue.Add(new Player("1.1.1.1") { UserID = 10});

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
                match.Client1 = PlayerQueue[0];
                match.Client2 = PlayerQueue[1];

                StartMatch(match);
                Console.WriteLine("Match has been started between UserID {0} and {1}", match.Client1.UserID, match.Client2.UserID);
                PlayerQueue.Remove(match.Client1.UserID);
                PlayerQueue.Remove(match.Client2.UserID);
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
                packet2.From = match.Client2.Socket.Client.LocalEndPoint.ToString();
                packet2.To = match.Client1.Socket.Client.LocalEndPoint.ToString();
                packet2.Type = TcpMessageType.MatchStart;
                var variables2 = new Dictionary<string, string>();
                variables2.Add("User1ID", match.Client1.UserID.ToString());
                variables2.Add("User1IP", match.Client1.Socket.Client.LocalEndPoint.ToString());
                variables2.Add("User2ID", match.Client2.UserID.ToString());
                variables2.Add("User2IP", match.Client2.Socket.Client.LocalEndPoint.ToString());
                
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
                            //============Under construction

                            string from = packet.From;
                            //string to = OnlinePlayers.Where(x => x.Key == int.Parse(packet.To)).FirstOrDefault().Value.;
                            string chatmessage = packet.Variables["Chatmessage"];
                            
                            

                            SendTcp.SendPacket(packet); 

                            //SendSuccessResponse(packet);
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
                            client.UserID = int.Parse(packet.Variables["UserID"]);

                            if (!PlayerQueue.ContainsKey(client.UserID))
                            {
                                PlayerQueue.Add(client.UserID, client);
                                Console.WriteLine("UserID {0} has been added to the player queue", client.UserID.ToString());
                                //SendSuccessResponse(packet);
                            }
                            
                            //Response = new Packet("Server", player.LocalIP, TcpMessageType.Response, new string[] {"Operation", "AddPlayerToQueue", "Result", "Success" });
                            //SendTcp.SendPacket(Response);
                            
                            break;
                        }
                    case TcpMessageType.Login:
                        {
                            string username = packet.Variables["Username"];
                            int userID = int.Parse(packet.Variables["UserID"]);
                            string email = packet.Variables["Email"];
                            string password = packet.Variables["Password"];

                            Client _client = new Client
                            {
                                UserID = userID,
                                Username = username,
                                Socket = client.Socket
                            };

                            if (!OnlinePlayers.ContainsKey(_client.UserID))
                            {
                                OnlinePlayers.Add(_client.UserID, _client);
                                Console.WriteLine(_client.Username + " logged in");
                                SendSuccessResponse(packet);
                            }
                            else if (OnlinePlayers.ContainsKey(_client.UserID))
                            {
                                Console.WriteLine(_client.Username + " tried to log in while it's already logged in. Login aborted.");
                                SendSuccessResponse(packet);
                            }

                            //OnlinePlayers.Add(packet.Variables["UserID"]);
                            break;
                        }
                    case TcpMessageType.Logout:
                        {
                            int userID = int.Parse(packet.Variables["UserID"]);

                            if (OnlinePlayers.ContainsKey(userID))
                            {
                                OnlinePlayers.Remove(userID);
                            }
                            else
                            {
                                Console.WriteLine("Player tried to log out while its not logged in.");
                            }

                            SendSuccessResponse(packet);
                            client.Socket.Close();
                            break;
                        }
                }
            }
            catch
            {
                Console.WriteLine("Could not handle packet. Please check the packet syntax.");
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
