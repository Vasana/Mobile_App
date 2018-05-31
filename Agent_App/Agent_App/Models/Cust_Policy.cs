using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class Cust_Policy
    {
        public string PolicyNumber { get; set; }
        public string AgentCode { get; set; }
        public string InsuredName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Department { get; set; }
        public string PolicyType { get; set; }
        public string PolTypeDesc { get; set; }
        public string VehicleNumber { get; set; }
        public bool IsVisible { get; set; }
        public string PolTypeImage { get; set; }
        public string PolStatusImage { get; set; }
        public string ClaimStatusImage { get; set; }
    }
}
