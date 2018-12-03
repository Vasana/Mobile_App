using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class Audit_trail
    {
        public DateTime Log_date { get; set; }
        public string App_Id { get; set; }
        public string User_Id { get; set; }
        public string Action { get; set; }
        public string Stream { get; set; }
    }
}
