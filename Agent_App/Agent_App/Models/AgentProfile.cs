using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public sealed class AgentProfile
    {
        //private static readonly AgentProfile instance = new AgentProfile();

        public int Agent_code { get; set; }
        public int Organizer_code { get; set; }

        public int Organizer_codeTeam { get; set; }

        public string Role { get; set; }

        public bool Is_org { get; set; }
        public string title { get; set; }
        public string initials { get; set; }
        public string last_name { get; set; }
        public string NIC_number { get; set; }

        public string email { get; set; }
        public string mobileNo { get; set; }
        public string residentialNo { get; set; }

        public int DoB { get; set; }
        public string ibslNo { get; set; }
        public string ibslPassed { get; set; }


        //private AgentProfile() { }

        //public static AgentProfile Instance
        //{
        //    get
        //    {
        //        return instance;
        //    }
        //}
    }
}
