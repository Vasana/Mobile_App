using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public sealed class User_profile
    {
        private static readonly User_profile instance = new User_profile();

        public static int agentCode { get; set; }
        public static bool IsOrganizer { get; set; }

        private User_profile() { }

        public static User_profile Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
