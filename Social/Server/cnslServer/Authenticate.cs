using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cnslServer
{
    public class Authentication
    {
        public bool Login(string Email, string Password)
        {
            if (Email == null || Password == null) return false;

            string response = SendWebRequest.GetResponse("http://careforsometeasaboteur.com/api/checklogin/Sjoerdk@outlook.com/Sjoerd123");

            if (response == "false") return false;
            else return true;

        }
    }
}
