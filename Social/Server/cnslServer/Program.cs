using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using Library;
using TcpMessage;
using System.Net;


namespace cnslServer
{
    class Program
    {
        private static List<Player> PlayerQueue;
        private static string ReceivedData;

        static void Main(string[] args)
        {
            ////Initializers
            //PlayerQueue = new List<Player>();
            //int loopcount = 0;

            ////Server loop
            //do
            //{   
            //    HandleMatchmaking();


            //    loopcount++;
            //    Console.WriteLine(loopcount.ToString());
            //}
            //while (true);

            Listen();
        }

        public void ReceiveData(Packet packet)
        {
            ReceivedData = packet.ToString();
        }

        public void AddPlayerToQueue(Player player)
        {
            PlayerQueue.Add(player);
        }

        private void AddPlayerToQueue(string Data)
        {

        }

        private static void HandleMatchmaking()
        {
            if (PlayerQueue.Count < 2) return;

            for(int i=0; i < PlayerQueue.Count; i++)
            {
                Match match = new Match();
                match.player1 = PlayerQueue[0];
                match.player2 = PlayerQueue[1];

                StartMatch(match);
                PlayerQueue.Remove(match.player1);
                PlayerQueue.Remove(match.player2);
            }
        }

        private static void StartMatch(Match match)
        {
            
        }

        private static void Asynctesting()
        {
            Asynctesting();
            Console.WriteLine("Het begint");
            long currentaantal = testasync().Result;
            Console.WriteLine(currentaantal.ToString());


            Console.ReadLine();
        }

        private static long increase()
        {
            long currentaantal = 0;

            do
            {
                currentaantal++;
            } while (currentaantal < 1500000000);

            return currentaantal;
        }

        private static async Task<long> testasync()
        {
            Task<long> task = new Task<long>(increase);
            task.Start();
            long currentaantal = await task;

            return currentaantal;
        }

        private static void Listen()
        {
            TcpListener server = null;

            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 25001;
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
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client.
                        data = data.ToUpper();

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
        
    }
}
