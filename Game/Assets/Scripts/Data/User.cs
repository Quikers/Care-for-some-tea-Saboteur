using System;

namespace Data
{
    [Serializable]
    struct PlayerUser
    {
        public static int Id = 5;
        public static string Email;
        public static string Username = "jemoeders";
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

    struct EnemyUser
    {
        public static int Id;
    }
}
