using System;

namespace Data
{
    [Serializable]
    struct User
    {
        public static int Id;
        public static string Email;
        public static string Username;
        public static int AccountType;
        public static string TimeCreated;
        public static string TimeEdited;


        public static void Empty()
        {
            Id = 0;
            Email = "";
            Username = "";
            AccountType = 0;
            TimeCreated = "";
            TimeEdited = "";
        }
    }
}
