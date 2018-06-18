using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class LifePolicy
    {
        public string PolicyNumber { get; set; }
        public string InsuredName { get; set; }
        public List<string> Address { get; set; }
        //public string VehicleNumber { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SumInsured { get; set; }
        public List<string> AdditionalCovers { get; set; }
    }
}
