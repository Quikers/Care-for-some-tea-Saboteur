using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Newtonsoft.Json;

namespace TestEnvironment
{
    class Program
    {
        static void Main(string[] args)
        {
            string response = SendWebRequest.GetResponse("http://careforsometeasaboteur.com/api/getuserbyusername/Shifted");

            if(response == null)
            {
                Console.WriteLine("Could not find player.");
            }
            else
            {
                User user = JsonConvert.DeserializeObject<User>(response);
                Console.WriteLine(user.ToString());
            }

            Console.ReadLine();

        }
    }
}
