using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace Library
{
    public class Client
    {
        public int UserID;
        public string Username;
        public TcpClient Socket;
        public Thread Listen;
    }
}
