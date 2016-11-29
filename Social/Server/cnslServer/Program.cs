using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Library;

namespace cnslServer
{
    class Program
    {
        private static List<Player> PlayerQueue;
        private static string ReceivedData;

        static void Main(string[] args)
        {
            //Initializers
            PlayerQueue = new List<Player>();
            int loopcount = 0;

            //Server loop
            do
            {   
                HandleMatchmaking();

                
                loopcount++;
                Console.WriteLine(loopcount.ToString());
            }
            while (true);
        }

        public void ReceiveData(string Data)
        {
            ReceivedData = Data;

            
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
        
    }
}
