using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class ClubResponse
    {
        public bool IsSelected { get; set; }

        public bool DateFound { get; set; }

        public string CurrentClub { get; set; }
        public double CurrentLimit { get; set; }

        public string NextClub { get; set; }
        public double NextLimit { get; set; }

        public string Target { get; set; }
        public List<string> FailedReason = new List<string>();
        public string Life_appointDate { get; set; }
        public string Gen_appointDate { get; set; }
        public string Life_persistancy { get; set; }
        public string Gen_persistancy { get; set; }

        public double last5yearAvg { get; set; }
        public double last3yearMin { get; set; }

        public string eligibleClub { get; set; }

        public string clubYear { get; set; }

        public List<Double> Last5yearList = new List<double>();

        public string LastUpdatedDate { get; set; }
        public bool DataNotFound { get; set; }
    }
}
