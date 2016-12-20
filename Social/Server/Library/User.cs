using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public override string ToString()
        {
            return "ID: " + this.ID + " Username: " + this.Username + " Password: " + this.Password + " Email: " + this.Email + " Adminlevel: " + this.AdminLevel.ToString();
        }
    }
}
