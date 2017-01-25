using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using Library;
using System.Net;

//namespace cnslServer
namespace Server
{

    //receive data port = 25002;
    //submit data port = 25003;
    class Program
    {
        private static Dictionary<int, Client> PlayerQueue;
        private static Dictionary<int, Client> OnlinePlayers;
        private static List<Match> ActiveGames;
        private static List<Match> PendingGames;

        static void Main(string[] args)
        {   
            OnlinePlayers = new Dictionary<int, Client>();
            PlayerQueue = new Dictionary<int, Client>();
            ActiveGames = new List<Match>();
            PendingGames = new List<Match>();

            Task Matchmaking = new Task(HandleMatchmaking);
            Matchmaking.Start();

            Task Listen = new Task(ListenTcp);
            Listen.Start();

            Task CheckAllPlayers = new Task(CheckAllPlayersValidation);
            CheckAllPlayers.Start();

           
            do
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    default: break;

                    case "showplayers":
                        {
                            ShowOnlinePlayers();
                            break;
                        }
                }
            } while (true);
        }

        private static void HandleMatchmaking()
        {
            while (true)
            {
                //2 Players required in matchmaking queue to start game.
                if (PlayerQueue.Count < 2) continue;

                //Display players in matchmaking queue
                Console.WriteLine("\nMatchmaking queue:\n");
                foreach(KeyValuePair<int, Client> pair in PlayerQueue)
                {
                    Console.WriteLine(pair.Value.UserID + " - " + pair.Value.Username);
                }
                Console.WriteLine();

                //Create and start match
                Match match = new Match();
                match.Client1 = PlayerQueue.ElementAt(0).Value;
                match.Client2 = PlayerQueue.ElementAt(1).Value;
                StartMatch(match);
                
                Console.WriteLine("Match has been started between UserID {0} and {1}", match.Client1.UserID, match.Client2.UserID);

                //Remove players from matchmaking queue
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
                variables.Add("Username", match.Client2.Username);
                variables.Add("Turn", "1");
                packet1.Variables = variables;

                SendTcp.SendPacket(packet1, match.Client1.Socket);

                // Send response to player 2
                Packet packet2 = new Packet();
                packet2.From = "Server";
                packet2.To = match.Client1.Socket.Client.LocalEndPoint.ToString();
                packet2.Type = TcpMessageType.MatchStart;
                var variables2 = new Dictionary<string, string>();
                variables2.Add("UserID", match.Client1.UserID.ToString());
                variables2.Add("Username", match.Client1.Username);
                variables2.Add("Turn", "2");
                packet2.Variables = variables2;

                SendTcp.SendPacket(packet2, match.Client2.Socket);

                //Add match to ActiveGames list.
                ActiveGames.Add(match);
            }
            catch
            {
                Console.WriteLine("Unable to connect players. Check if the UserID or Socket are not empty.");
            }
            
        }

        private static void ListenTcp()
        {
            TcpListener listener = null;

            try
            {
                // Initialize port & local IP
                Int32 port = 25002;
                IPAddress localAddr = IPAddress.Parse("0.0.0.0");
                
                listener = new TcpListener(localAddr, port);

                // Start listening for client requests.
                listener.Start();
                Console.WriteLine("TcpListener started. Waiting for a connection... ");

                // Enter the listening loop.
                while (true)
                {   
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Incoming connection detected.");
                    

                    Packet packet = SendTcp.ReceivePacket(client);
                    if (packet == null) continue;

                    HandlePacket(packet, new Client {Socket = client });
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                listener.Stop();
            }
        }

        private static void HandlePacket(Packet packet, Client client)
        {
            if (packet == null) return;

            //Check if sender and target are valid
            //From
            if (packet.From != "Server" && packet.From != "server" && packet.Type != TcpMessageType.Login)
            {   
                if (!IsClientValid(int.Parse(packet.From))) return;
            }

            //To 
            if (packet.To != "Server" && packet.To != "server") 
            {
                if (!IsClientValid(int.Parse(packet.To)))
                {
                    SendErrorToClient("Server", client, "Targeted user is offline.");
                }
            }

            try
            {
                switch (packet.Type)
                {
                    default:
                        {
                            Console.WriteLine("Error handling packet.");
                            break;
                        }

                    case TcpMessageType.ChatMessage:
                        {
                            if (!Communicate(packet))
                            {
                                SendErrorToClient("Server", client, packet.Type, "Could not send message to offline user.");
                            }
                            else SendSuccessResponse(packet, client);
                            break;
                        }

                    case TcpMessageType.None:
                        break;

                    case TcpMessageType.PlayerUpdate:
                        {
                            if (!packet.Variables.ContainsKey("PlayerAction")) break;

                            //Get match
                            Match match = ActiveGames.Where(x => x.Client1 == client || x.Client2 == client).FirstOrDefault();

                            if (match == null)
                            {
                                SendErrorToClient("Server", client, packet.Type, "Could not find active match");
                                break;
                            }

                            Client player = null;
                            Client opponent = null;

                            //Check which Client is Client1 and Client2
                            if (client == match.Client1)
                            {
                                player = match.Client1;
                                opponent = match.Client2;
                            }
                            else
                            {
                                player = match.Client2;
                                opponent = match.Client1;
                            }

                            //Check if opponent == null
                            if (opponent == null)
                            {
                                SendErrorToClient(packet.From, client, packet.Type, "Could not connect with opponent");
                                Console.WriteLine("Error in Program.cs > HandlePacket > PlayerUpdate : opponent == null");
                                break;
                            }

                            //Switch PlayerAction
                            switch (packet.Variables["PlayerAction"])
                            {
                                case "PlayCard":
                                    {
                                        if (!packet.Variables.ContainsKey("CardType") || (!packet.Variables.ContainsKey("CardID")) || (!packet.Variables.ContainsKey("CardName")))
                                        {
                                            SendErrorToClient("Server", client, packet.Type, "Invalid card");
                                            Console.WriteLine("UserID {0} tried to process an invalid packet with TcpMessageType.PlayerAction.", packet.From);
                                            break;
                                        }

                                        string targetID = null;
                                        string cardName = null;
                                        if (packet.Variables.ContainsKey("TargetID"))
                                            targetID = packet.Variables["TargetID"];

                                        cardName = packet.Variables["CardName"];

                                        switch (packet.Variables["CardType"])
                                        {
                                            case "Minion":
                                                {
                                                    //Check requirements of incoming packet
                                                    if (!packet.Variables.ContainsKey("Health")
                                                        || (!packet.Variables.ContainsKey("Attack"))
                                                        || (!packet.Variables.ContainsKey("EnergyCost"))
                                                        || (!packet.Variables.ContainsKey("EffectType")) 
                                                        || (!packet.Variables.ContainsKey("Effect")))
                                                    {
                                                        SendErrorToClient(packet.From, client, "Invalid packet");
                                                        Console.WriteLine("UserID {0} tried to process an invalid packet. HandlePacket > PlayerUpdate > PlayCard > Minion", packet.From);
                                                        return;
                                                    }
                                                        

                                                    //Create packet for opponent
                                                    Packet minionPlayed = new Packet(
                                                        packet.From,
                                                        opponent.UserID.ToString(),
                                                        TcpMessageType.PlayerUpdate,
                                                        new[] {
                                                            "PlayerAction", PlayerAction.PlayCard.ToString(),
                                                            "CardType", CardType.Minion.ToString(),
                                                            "Health", packet.Variables["Health"],
                                                            "Attack", packet.Variables["Attack"],
                                                            "EnergyCost", packet.Variables["EnergyCost"],
                                                            "EffectType", packet.Variables["EffectType"],
                                                            "Effect", packet.Variables["Effect"],
                                                            "CardID", packet.Variables["CardID"],
                                                            "CardName", packet.Variables["CardName"]
                                                        });
                                                    if (targetID != null) minionPlayed.Variables.Add("TargetID", targetID);

                                                    //Send packet to opponent
                                                    SendTcp.SendPacket(minionPlayed, opponent.Socket);

                                                    SendSuccessResponse(packet, client);
                                                    break;
                                                }
                                            case "Spell":
                                                {
                                                    //Check requirements of incoming packet
                                                    if ((!packet.Variables.ContainsKey("EnergyCost")) || (!packet.Variables.ContainsKey("Effect")))
                                                    {
                                                        Console.WriteLine("UserID {0} tried to process an invalid packet. HandlePacket > PlayerUpdate > PlayCard > Spell", packet.From);
                                                        SendErrorToClient("Server", client, "Invalid Packet.");
                                                        break;
                                                    }

                                                    //Create packet for opponent
                                                    Packet spellPlayed = new Packet(
                                                        packet.From,
                                                        opponent.UserID.ToString(),
                                                        TcpMessageType.PlayerUpdate,
                                                        new[] {
                                                            "PlayerAction", PlayerAction.PlayCard.ToString(),
                                                            "CardType", CardType.Minion.ToString(),
                                                            "EnergyCost", packet.Variables["EnergyCost"],
                                                            "Effect", packet.Variables["Effect"],
                                                            "CardID", packet.Variables["CardID"],
                                                            "CardName", packet.Variables["CardName"]
                                                        });
                                                    if (targetID != null)
                                                        spellPlayed.Variables.Add("TargetID", targetID);
                                                    

                                                    //Send packet to opponent
                                                    SendTcp.SendPacket(spellPlayed, opponent.Socket);

                                                    SendSuccessResponse(packet, client);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                                case "Attack":
                                    {
                                        //Check requirements for incoming packet
                                        if ((!packet.Variables.ContainsKey("AttackingMinionID"))
                                            || (!packet.Variables.ContainsKey("TargetMinionID")))
                                            break;

                                        //Create packet for opponent
                                        Packet attack = new Packet(
                                                        packet.From,
                                                        opponent.UserID.ToString(),
                                                        TcpMessageType.PlayerUpdate,
                                                        new[] {
                                                            "PlayerAction", PlayerAction.PlayCard.ToString(),
                                                            "CardType", CardType.Minion.ToString(),
                                                            "EnergyCost", packet.Variables["EnergyCost"],
                                                            "Effect", packet.Variables["Effect"]
                                                        });

                                        //Send packet to opponent
                                        SendTcp.SendPacket(attack, opponent.Socket);

                                        SendSuccessResponse(packet, client);
                                        break;
                                    }
                                case "EndTurn":
                                    {   
                                        SendTcp.SendPacket(packet, opponent.Socket);
                                        SendSuccessResponse(packet, client);
                                        break;
                                    }
                            }
                            break;
                        }

                    case TcpMessageType.AddPlayerToQueue:
                        {
                            client.UserID = int.Parse(packet.From);

                            if (!PlayerQueue.ContainsKey(client.UserID))
                            {
                                PlayerQueue.Add(client.UserID, client);
                                Console.WriteLine("UserID {0} has been added to the player queue", client.UserID.ToString());
                            }
                            else if (PlayerQueue.ContainsKey(client.UserID))
                            {
                                Console.WriteLine("UserID {0} tried to add himself to the queue while he's already queued up!");
                            }

                            SendSuccessResponse(packet, client);
                            break;
                        }
                    case TcpMessageType.Login:
                        {
                            int userID = int.Parse(packet.From);
                            string username = packet.Variables["Username"];
                            
                            //Check for valid username
                            if (!packet.Variables.ContainsKey("Username") || username == "" || username == string.Empty)
                            {
                                Packet error = new Packet("Server", packet.From, TcpMessageType.Error, new[] {
                                    "ErrorMessage", "Please provide a valid username"
                                });
                                SendTcp.SendPacket(error, client.Socket);
                                break;
                            }

                            //Create new Client instance
                            Client _client = new Client
                            {
                                UserID = userID,
                                Username = username,
                                Socket = client.Socket
                            };

                            //If client isnt logged in already: log in
                            if (!IsClientValid(_client.UserID))
                            {
                                OnlinePlayers.Add(_client.UserID, _client);
                                Console.WriteLine(_client.Username + " logged in");

                                SendSuccessResponse(packet, client);

                                _client.Listen = new Thread(() => ListenToClient(_client));
                                _client.Listen.Start();
                            }
                            else //If client is logged in, check its validity
                            {
                                Console.WriteLine(_client.Username + " tried to log in while it's already logged in. Login aborted.");

                                if (IsClientValid(_client.UserID))
                                {
                                    SendSuccessResponse(packet, client);
                                } 
                                else
                                {   
                                    Console.WriteLine("{0} tried to log in while its already logged in. Its socket isn't valid anymore", _client.Username);
                                }
                            }

                            ShowOnlinePlayers();
                            break;
                        }
                    case TcpMessageType.Logout:
                        {
                            int userID = int.Parse(packet.From);

                            if (IsClientValid(userID))
                            {
                                Logout(client);

                                Console.WriteLine("UserID {0} logged out", userID);
                                ShowOnlinePlayers();
                            }
                            else
                            {
                                Console.WriteLine("User {0} tried to log out while its not logged in. \n\n", packet.From);
                            }

                            client.Socket.Close();
                            break;
                        }
                    case TcpMessageType.CancelMatchmaking:
                        {
                            int fromUserID = int.Parse(packet.From);
                            if (PlayerQueue.ContainsKey(fromUserID))
                            {
                                PlayerQueue.Remove(fromUserID);
                            }

                            SendSuccessResponse(packet, client);
                            break;
                        }
                    case TcpMessageType.SendGameInvite:
                        {
                            int fromUserID = int.Parse(packet.From);
                            int toUserID = int.Parse(packet.To);

                            if (!IsClientValid(toUserID))
                            {
                                SendErrorToClient("Server", client, packet.Type, "Targeted user is offline.");
                                break;
                            }
                            
                            //If pending game already exists, do nothing
                            if (PendingGames.Where(x => x.Client1 == client).FirstOrDefault() != null) break;

                            //Create new pending game
                            Match match = new Match()
                            {
                                Client1 = client,
                                Client2 = OnlinePlayers.Where(x => x.Key == toUserID).FirstOrDefault().Value
                            };

                            if(match.Client2 != null)
                            {
                                PendingGames.Add(match);

                                //Send packet to client2
                                Communicate(packet);
                            }
                            break;
                        }
                    case TcpMessageType.CancelGameInvite:
                        {
                            var pendinggame = PendingGames.Where(x => x.Client1 == client).FirstOrDefault();
                            if(pendinggame != null)PendingGames.Remove(pendinggame);

                            Communicate(packet);
                            break;
                        }
                    case TcpMessageType.AcceptIncomingGameInvite:
                        {
                            if (IsClientValid(int.Parse(packet.To)))
                            {
                                int senderID = int.Parse(packet.To);
                                Match game = PendingGames.Where(x => x.Client1.UserID == senderID).FirstOrDefault();
                                if (game != null)
                                {
                                    PendingGames.Remove(game);
                                    StartMatch(game);
                                }
                                else SendErrorToClient("Server", client, TcpMessageType.AcceptIncomingGameInvite, "Could not find pending game.");
                                break;
                            }
                            else
                            {
                                SendErrorToClient("Server", client, "Could not send game invite response. Target user is offline.");
                                break;
                            }
                        }
                    case TcpMessageType.RefuseIncomingGameInvite:
                        {
                            int from = int.Parse(packet.From);
                            int to = int.Parse(packet.To);
                            
                            if (!IsClientValid(to)) break;

                            Communicate(packet);

                            //Remove game from PendinGames
                            Match pendinggame = PendingGames.Where(x => x.Client1.UserID == to).FirstOrDefault();
                            if (pendinggame != null) PendingGames.Remove(pendinggame);
                            break;
                        }
                    case TcpMessageType.SendFriendRequest:
                        {
                            int targetUserID = int.Parse(packet.To);
                            if (!IsClientValid(targetUserID))
                            {
                                SendErrorToClient("Server", client, packet.Type, "Could not send friend request. Targeted user is offline.");
                                break;
                            }

                            Communicate(packet);
                            SendSuccessResponse(packet, client);
                            break;
                        }
                    case TcpMessageType.CancelFriendRequest:
                        {
                            Communicate(packet);
                            SendSuccessResponse(packet, client);
                            break;
                        }
                    case TcpMessageType.RefuseFriendRequest:
                        {
                            Communicate(packet);
                            SendSuccessResponse(packet, client);
                            break;
                        }
                    case TcpMessageType.AcceptFriendRequest:
                        {
                            if (!Communicate(packet)) SendErrorToClient("Server", client, packet.Type, "Could not accept friend request. Targeted user is offline.");
                            else SendSuccessResponse(packet, client);
                            break;
                        }
                    case TcpMessageType.MatchEnd:
                        {
                            int fromUserID = int.Parse(packet.From);

                            //Foreach Match in ActiveGames remove the match that has packet.from as userID for Client1 or Client2.
                            int loopcount = 0;
                            foreach(var x in ActiveGames.Where(x => x.Client1.UserID == fromUserID || x.Client2.UserID == fromUserID))
                            {   
                                ActiveGames.Remove(x);
                                loopcount++;
                                if (loopcount > 1) Console.WriteLine("UserID {0} had multiple active games. HandlePacket > MatchEnd", fromUserID);
                            }

                            break;
                        }
                    case TcpMessageType.Broadcast:
                        {
                            Task broadcast = new Task(() => Broadcast(packet));
                            broadcast.Start();
                            break;
                        }
                    case TcpMessageType.DrawCard:
                        {
                            Communicate(packet);
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
                    if (packet == null) continue;
                    HandlePacket(packet, client);
                } catch (Exception ex)
                {
                    Logout(client);
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
            if (!IsClientValid(UserID)) return null;
            else return OnlinePlayers.Where(x => x.Key == UserID).FirstOrDefault().Value;
        }

        private static bool IsClientValid(int UserID)
        {
            if (OnlinePlayers.Count == 0) return false;
            Client user = OnlinePlayers.Where(x => x.Key == UserID).FirstOrDefault().Value;
            if (user == null) return false;

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

        private static void Broadcast(Packet packet)
        { 
            foreach (var player in OnlinePlayers)
            {
                packet.Type = TcpMessageType.ChatMessage;
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

        private static void SendErrorToClient(string from, Client to,  string errormessage)
        {
            try
            {
                Packet packet = new Packet(from, to.UserID.ToString(), TcpMessageType.Error, new[] { "ErrorMessage", errormessage });
                SendTcp.SendPacket(packet, to.Socket);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error! In Program.cs > SendErrorToClient(). ErrorMessage: {0}", e.ToString());
            }
        }

        private static void SendErrorToClient(string from, Client to, TcpMessageType receivedpackettype, string errormessage)
        {
            try
            {
                Packet packet = new Packet(from, to.UserID.ToString(), TcpMessageType.Error, new[] { "ErrorType", receivedpackettype.ToString(), "ErrorMessage", errormessage });
                SendTcp.SendPacket(packet, to.Socket);
            }catch(Exception e)
            {
                Console.WriteLine("Error! In Program.cs > SendErrorToClient(). ErrorMessage: {0}", e.ToString());
            }
            
        }

        private static bool Communicate(Packet packet)
        {
            try
            {
                int targetUserID = int.Parse(packet.To);
                if (!IsClientValid(targetUserID)) return false;

                Client targetClient = GetClientFromOnlinePlayersByUserID(targetUserID);
                SendTcp.SendPacket(packet, targetClient.Socket);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in Server > Program.cs > Communicate function. Error: " + ex);
                return false;
            }
           

        }

        private static void CheckAllPlayersValidation()
        {
            do
            {
                foreach (int key in OnlinePlayers.Keys)
                    IsClientValid(key);

                Thread.Sleep(500);

            } while (true);
        }

        private static void Logout(Client client)
        {
            //Close connection
            client.Socket.Close();

            //Remove from Pending games
            foreach (var pendinggame in PendingGames.Where(x => x.Client1.UserID == client.UserID))
                PendingGames.Remove(pendinggame);

            //Remove from Online Players
            if(OnlinePlayers.ContainsKey(client.UserID)) OnlinePlayers.Remove(client.UserID);

            Console.WriteLine("UserID {0} logged out.", client.UserID.ToString());
            ShowOnlinePlayers();
        }

        private static void ReadInput()
        {
            do
            {
                string input = Console.ReadLine();
                switch (input)
                {
                    default: break;

                    case "showplayers":
                        ShowOnlinePlayers();
                        break;
                }
            } while (true);
        }
    }
}
