using System;
using System.Collections.Generic;
using System.Linq;

namespace Library {
    public enum TcpMessageType {
        None = 0,
        ChatMessage = 3,
        PlayerUpdate = 4,
        AddPlayerToQueue = 6,
        MatchStart = 7,
        Response = 8,
        Login = 9,
        Logout = 10,
        Playerlist = 11,
        CancelMatchmaking = 12,
        Error = 13,
        SendGameInvite = 14,
        CancelGameInvite = 15,
        RefuseIncomingGameInvite = 16,
        AcceptIncomingGameInvite = 17,
        SendFriendRequest = 18,
        CancelFriendRequest = 19,
        RefuseFriendRequest = 20,
        AcceptFriendRequest = 21,
        EndTurn = 22,
        MatchEnd = 23,
        Broadcast = 24,
        DrawCard = 25
    }

    public enum PlayerAction
    {
        PlayCard = 1,
        Attack = 2
    }

    public enum CardType
    {
        Minion = 1,
        Spell = 2
    }

    public static class PacketParser {
        public static Packet Parse(string data) {
            Packet packet = new Packet();

            if (!data.Contains("\\~\\"))
                throw new Exception("String does not contain a valid TcpMessage Packet. Error (1)");

            string[] variables = data.Split(new[] { "\\~\\" }, StringSplitOptions.None);
            foreach (string pair in variables) {
                if( !pair.Contains( "\\@\\" ) )
                    throw new Exception( "String does not contain a valid TcpMessage Packet. Error (2)" );

                string[] keyValue = pair.Split(new[] { "\\@\\" }, StringSplitOptions.None);
                switch (keyValue[0]) {
                    default:
                        if (!packet.Variables.ContainsKey(keyValue[0]))
                            packet.Variables.Add(keyValue[0], keyValue[1]);
                        break;
                    case "From":
                        packet.From = keyValue[1];
                        break;
                    case "To":
                        packet.To = keyValue[1];
                        break;
                    case "Type":
                        try {
                            packet.Type = (TcpMessageType)Enum.Parse(typeof(TcpMessageType), keyValue[1]);
                        } catch (Exception) {
                            throw new Exception("Type \"" + keyValue[1] + "\" is not an underlying value of TcpMessageType.");
                        }

                        break;
                }
            }

            return packet;
        }


    }

    public class Packet {
        public string From = "";
        public string To = "";
        public TcpMessageType Type = TcpMessageType.None;
        public Dictionary<string, string> Variables = new Dictionary<string, string>();

        public Packet() { }
        public Packet(string data) {
            Packet p = PacketParser.Parse(data);

            if (p != null) {
                From = p.From;
                To = p.To;
                Type = p.Type;
                Variables = p.Variables;
            } else
                Console.WriteLine("Packet was not parsed correctly.");
        }

        public Packet(string from, string to, TcpMessageType type, Dictionary<string, string> variables = null) {
            From = from;
            To = to;
            Type = type;
            if (variables != null)
                Variables = variables;
        }

        public Packet(string from, string to, TcpMessageType type, string[] variable)
        {
            From = from;
            To = to;
            Type = type;

            if (variable.Length <= 0)
                return;
            for (int i = 0; i < variable.Length; i += 2)
            {
                if (!Variables.ContainsKey(variable[i]))
                    Variables.Add(variable[i], variable[i + 1]);
            }
        }

        public override string ToString() {
            string str = "From\\@\\" + From + "\\~\\To\\@\\" + To + "\\~\\Type\\@\\" + Type;

            if (Variables.Count <= 0)
                return str;
            str += "\\~\\" + string.Join("\\~\\", Variables.Select(pair => pair.Key + "\\@\\" + pair.Value).ToArray());

            return str;
        }
    }
}

