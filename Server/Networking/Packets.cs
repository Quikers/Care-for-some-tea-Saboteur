using System;
using System.Collections.Generic;
using System.Linq;

namespace Networking {
    public enum PacketType {
        None = 0,
        Login = 1,
        LoginFailed = 2,
        LoginSuccess = 3,
        Logout = 4,
        SystemMessage = 5,
        ChatMessage = 6,
        GetConnectedPlayersList = 7,
        GetFriendsList = 8
    }

    public enum PlayerAction {

    }

    public static class PacketParser {
        public static Packet Parse (string data) {
            Packet packet = new Packet();

            if (!data.Contains("\\~\\"))
                throw new Exception("String does not contain a valid TcpMessage Packet. Error (1)");

            string[] variables = data.Split(new[] { "\\~\\" }, StringSplitOptions.None);
            foreach (string pair in variables) {
                if (!pair.Contains("\\@\\"))
                    throw new Exception("String does not contain a valid TcpMessage Packet. Error (2)");

                string[] keyValue = pair.Split(new[] { "\\@\\" }, StringSplitOptions.None);
                switch (keyValue[0]) {
                    default:
                        if (!packet.Variables.ContainsKey(keyValue[0]))
                            packet.Variables.Add(keyValue[0], TryParse(keyValue[1]));
                        break;
                    case "From":
                        packet.From = keyValue[1];
                        break;
                    case "To":
                        packet.To = keyValue[1];
                        break;
                    case "Type":
                        try {
                            packet.Type = (PacketType)Enum.Parse(typeof(PacketType), keyValue[1]);
                        } catch (Exception) {
                            throw new Exception("Type \"" + keyValue[1] + "\" is not an underlying value of PacketType.");
                        }

                        break;
                }
            }

            return packet;
        }

        public static object TryParse(string str) {
            int iresult;
            bool bresult;

            if (int.TryParse(str, out iresult))
                return iresult;
            if (bool.TryParse(str, out bresult))
                return bresult;

            return str;
        }
    }

    public class Packet {
        public string From = "";
        public string To = "";
        public PacketType Type = PacketType.None;
        public Dictionary<string, object> Variables = new Dictionary<string, object>();

        public Packet () { }
        public Packet (string data) {
            Packet p = PacketParser.Parse(data);

            if (p != null) {
                From = p.From;
                To = p.To;
                Type = p.Type;
                Variables = p.Variables;
            } else
                Console.WriteLine("Packet was not parsed correctly.");
        }

        public Packet (string from, string to, PacketType type, Dictionary<string, object> variables = null) {
            From = from;
            To = to;
            Type = type;
            if (variables != null)
                Variables = variables;
        }

        public Packet (string from, string to, PacketType type, string[] variable) {
            From = from;
            To = to;
            Type = type;

            if (variable.Length <= 0)
                return;
            for (int i = 0; i < variable.Length; i += 2) {
                if (!Variables.ContainsKey(variable[i]))
                    Variables.Add(variable[i], variable[i + 1]);
            }
        }

        public override string ToString () {
            string str = "From\\@\\" + From + "\\~\\To\\@\\" + To + "\\~\\Type\\@\\" + Type;

            if (Variables.Count <= 0)
                return str;
            str += "\\~\\" + string.Join("\\~\\", Variables.Select(pair => pair.Key + "\\@\\" + pair.Value).ToArray());

            return str;
        }
    }
}

