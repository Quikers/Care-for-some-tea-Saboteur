using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library
{
    public class User
    {
        public int ID;
        public string Username;
        public string Password;
        public string Email;
        public int AdminLevel;

        public User()
        {

        }

        public User(string json)
        {
            User user = new User();
            user = JsonConvert.DeserializeObject<User>(json);
        }

        public override string ToString()
        {
            return "ID: " + this.ID + " Username: " + this.Username + " Password: " + this.Password + " Email: " + this.Email + " Adminlevel: " + this.AdminLevel.ToString();
        }
    }
}
