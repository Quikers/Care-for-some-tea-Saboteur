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
            PlayerQueue = new Dictionary<int, Client>();

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

                Console.WriteLine("\nMatchmaking queue:\n");
                foreach(KeyValuePair<int, Client> pair in PlayerQueue)
                {
                    Console.WriteLine(pair.Value.UserID + " - " + pair.Value.Username);
                }
                Console.WriteLine();

                Match match = new Match();
                match.Client1 = PlayerQueue.ElementAt(0).Value;
                match.Client2 = PlayerQueue.ElementAt(1).Value;

                StartMatch(match);
                
                Console.WriteLine("Match has been started between UserID {0} and {1}", match.Client1.UserID, match.Client2.UserID);
                PlayerQueue.Remove(match.Client1.UserID);
                PlayerQueue.Remove(match.Client2.UserID);
            }
        }
        
        private static void StartMatch(Match match)
        {
            try { 
                // Send response to player 1
                Packet packet1 = new Packet();
                packet1.From = "Server";
                packet1.To = match.Client1.UserID.ToString();
                packet1.Type = TcpMessageType.MatchStart;
                var variables = new Dictionary<string, string>();
                variables.Add("UserID", match.Client2.UserID.ToString());
                variables.Add("UserIP", match.Client2.Socket.Client.LocalEndPoint.ToString());
                packet1.Variables = variables;

                SendTcp.SendPacket(packet1, match.Client1.Socket);

                // Send response to player 2
                Packet packet2 = new Packet();
                packet2.From = "Server";
                packet2.To = match.Client1.Socket.Client.LocalEndPoint.ToString();
                packet2.Type = TcpMessageType.MatchStart;
                var variables2 = new Dictionary<string, string>();
                variables2.Add("UserID", match.Client1.UserID.ToString());
                variables2.Add("UserIP", match.Client1.Socket.Client.LocalEndPoint.ToString());
                packet2.Variables = variables2;

                SendTcp.SendPacket(packet2, match.Client2.Socket);
            }
            catch
            {
                Console.WriteLine("Unable to connect players. Check if the UserID or Socket are not empty.");
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
                Console.WriteLine("TcpListener started. Waiting for a connection... ");

                // Enter the listening loop.
                while (true)
                {
                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Incoming connection detected.");
                    Response = null;

                    Packet packet = SendTcp.ReceivePacket(client);
                    HandlePacket(packet, new Client {Socket = client });

                    // Send back a response.
                    //if(Response != null)
                    //{
                    //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(Response.ToString());
                    //    stream.Write(msg, 0, msg.Length);
                    //    Console.WriteLine("Sent: {0}", Response.ToString());
                    //}
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

            if (packet.From != "Server") IsClientValid(int.Parse(packet.From));
            if (packet.To != "Server") IsClientValid(int.Parse(packet.To));

            try
            {
                switch (packet.Type)
                {
                    case TcpMessageType.ChatMessage:
                        {
                            int fromUserID = int.Parse(packet.From);
                            int targetUserID = int.Parse(packet.To);
                            string chatmessage = packet.Variables["Chatmessage"];

                            if (!OnlinePlayers.ContainsKey(targetUserID))
                            {
                                Console.WriteLine("UserID {0} tried to send a message to offline UserID {1}", fromUserID, targetUserID);
                                return;
                            } 
                            else
                            {
                                //Send Packet to destination
                                SendTcp.SendPacket(new Packet(fromUserID.ToString(), targetUserID.ToString(), TcpMessageType.ChatMessage, new[] {"Chatmessage", chatmessage }), GetClientFromOnlinePlayersByUserID(targetUserID).Socket);

                                //Send response to sender
                                SendSuccessResponse(packet, client);

                                Console.WriteLine("Chatmessage sent from {0} to {1}",packet.From, packet.To);
                            }
                            
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
                            //Player player = new Player();
                            //player.UserID = int.Parse(packet.Variables["UserID"]);
                            //player.CurrentEnergy = int.Parse(packet.Variables["CurrentEnergy"]);
                            //player.MaxEnergy = int.Parse(packet.Variables["MaxEnergy"]);
                            //player.CurrentHealth = int.Parse(packet.Variables["CurrentHealth"]);
                            //player.MaxHealth = int.Parse(packet.Variables["MaxHealth"]);
                            break;
                        }

                    case TcpMessageType.AddPlayerToQueue:
                        {
                            client.UserID = int.Parse(packet.From);

                            if (!PlayerQueue.ContainsKey(client.UserID))
                            {
                                PlayerQueue.Add(client.UserID, client);
                                Console.WriteLine("UserID {0} has been added to the player queue", client.UserID.ToString());
                                SendSuccessResponse(packet, client);
                            }
                            else if (PlayerQueue.ContainsKey(client.UserID))
                            {
                                Console.WriteLine("UserID {0} tried to add himself to the queue while he's already queued up!");
                                SendSuccessResponse(packet, client);
                            }
                            
                            break;
                        }
                    case TcpMessageType.Login:
                        {   
                            int userID = int.Parse(packet.From);
                            string username = packet.Variables["Username"];

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
                                SendSuccessResponse(packet, client);

                                _client.Listen = new Thread(() => ListenToClient(_client));
                                _client.Listen.Start();
                                
                            }
                            else if (OnlinePlayers.ContainsKey(_client.UserID))
                            {
                                Console.WriteLine(_client.Username + " tried to log in while it's already logged in. Login aborted.");

                                if (IsClientValid(_client.UserID))
                                {
                                    SendSuccessResponse(packet, client);
                                } 
                                else
                                {
                                    Packet error = new Packet();
                                    packet.From = "Server";
                                    packet.To = _client.UserID.ToString();
                                    Console.WriteLine("{0} tried to log in while its already logged in. Its socket isn't valid anymore", _client.Username);
                                    SendTcp.SendPacket(error, _client.Socket);
                                }
                            }

                            ShowOnlinePlayers();
                            break;
                        }
                    case TcpMessageType.Logout:
                        {
                            int userID = int.Parse(packet.From);

                            if (OnlinePlayers.ContainsKey(userID))
                            {
                                Console.WriteLine("UserID {0} logged out", userID);
                                SendSuccessResponse(packet, client);

                                OnlinePlayers[userID].Socket.Close();
                                OnlinePlayers.Remove(userID);

                                Console.WriteLine("Online players:\n");
                                foreach (KeyValuePair<int, Client> pair in OnlinePlayers)
                                {
                                    Console.WriteLine("{0} - {1}", pair.Value.UserID, pair.Value.Username);
                                }
                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine("Player tried to log out while its not logged in.");
                            }

                            client.Socket.Close();
                            break;
                        }
                    case TcpMessageType.CancelMatchmaking:
                        {
                            if (PlayerQueue.ContainsKey(int.Parse(packet.From)))
                            {
                                PlayerQueue.Remove(int.Parse(packet.From));
                                Packet _response = new Packet("Server", packet.From, TcpMessageType.Response, new[] { "Result", "0" });
                                SendTcp.SendPacket(_response, client.Socket);
                            }
                            else
                            {
                                Packet _response = new Packet("Server", packet.From, TcpMessageType.Response, new[] { "Result", "0" });
                                SendTcp.SendPacket(_response, client.Socket);
                            }
                            break;
                        }
                }

                Console.WriteLine("====================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not handle packet. Please check the packet syntax.\n\n{0}", ex.ToString());
                return;
            }
        }

        private static void ListenToClient(Client client)
        {
            while (client.Socket.Connected)
            {
                try {
                    Packet packet = SendTcp.ReceivePacket(client.Socket);
                    HandlePacket(packet, client);
                } catch (Exception ex)
                {
                    client.Socket.Close();
                }
            }
        }

        private static void SendSuccessResponse(Packet ReceivedPacket, Client client)
        {
            Packet packet = new Packet("Server", ReceivedPacket.To, TcpMessageType.Response, new[] { "TcpMessageType", ReceivedPacket.Type.ToString() });
            SendTcp.SendPacket(packet, client.Socket);
        }

        private static Client GetClientFromOnlinePlayersByUserID(int UserID)
        {
            if (!OnlinePlayers.ContainsKey(UserID)) return null;
            else return OnlinePlayers[UserID];
        }

        private static bool IsClientValid(int UserID)
        {
            if (!OnlinePlayers.ContainsKey(UserID)) return false;

            Client user = OnlinePlayers[UserID];

            if (user.Socket.Connected)
            {
                return true;
            }
            else
            {   //Remove client
                user.Socket.Close();
                OnlinePlayers.Remove(UserID);
                return false;
            }
            
        }

        private static void SendPlayerList(Dictionary<int, Client> PlayerList)
        {
            Dictionary<string, string> players = new Dictionary<string, string>();

            foreach(var player in OnlinePlayers)
            {
                string usrID = player.Key.ToString();
                string usrName = player.Value.Username;

                players.Add(usrID, usrName);
            }

            Packet packet = new Packet("Server", "Everyone", TcpMessageType.Message, players);
            Broadcast(packet);
        }

        private static void Broadcast(Packet packet)
        { 
            foreach (var player in OnlinePlayers)
            {
                SendTcp.SendPacket(packet, player.Value.Socket);
            }
        }

        private static void ShowOnlinePlayers()
        {
            Console.WriteLine("Online players:\n");
            foreach (KeyValuePair<int, Client> pair in OnlinePlayers)
            {
                Console.WriteLine("{0} - {1}", pair.Value.UserID, pair.Value.Username);
            }
            Console.WriteLine("");
        }
        
    }
}
