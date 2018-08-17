using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class GeneralPolicy
    {
        public string PolicyNumber { get; set; }        
        public string InsuredName { get; set; }
        public List<string> Address { get; set; }
        public string VehicleNumber { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double SumInsured { get; set; }
        public List<CoverDetails> AdditionalCovers { get; set; }
        public string MakeYear { get; set; }
        public string Make { get; set; }
        public string ChassisNo { get; set; }
        public string EngineNo { get; set; }
        public string Department { get; set; }
        public string MobileNumber { get; set; }
        public bool isCancelled { get; set; }
        public double EngineCapacity { get; set; }
    }
}
