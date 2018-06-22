using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public sealed class SearchCriteria
    {
        private static readonly SearchCriteria instance = new SearchCriteria();

        public bool NewSearch { get; set; }
        public bool PremiumsPending { get; set; }
        public bool ClaimPending { get; set; }
        public bool Flagged { get; set; }
        public bool BadClaims { get; set; }
        public bool DebitOutstanding { get; set; }
        public bool AllPolicies { get; set; }
        public string PolicyNumber { get; set; } = "";
        public string VehicleNumber { get; set; } = "";
        public string StartFromDt { get; set; } = ""; // yyyy/MM/dd
        public string StartToDt { get; set; } = ""; // yyyy/MM/dd
        public bool TopTen { get; set; }


        //---- when adding a field update the apiServices.GetPoliciesAsync method to re-initilize the field

        private SearchCriteria() { }

        public static SearchCriteria Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
