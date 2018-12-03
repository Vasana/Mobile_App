using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public sealed class SearchCriteriaLife
    {
        private static readonly SearchCriteriaLife instance = new SearchCriteriaLife();

        public bool NewSearch { get; set; }
        public bool AllPolicies { get; set; }
        public string PolicyTable { get; set; } = "A";
        public bool inforce_pol { get; set; }
        public bool templapse_pol { get; set; }
        public bool lapsed_pol { get; set; }
        public bool Flagged { get; set; }
        public string PolicyNumber { get; set; } = "";
        public bool TopTen { get; set; }
        public bool TodayReminders { get; set; }
        public string ListDesc { get; set; } = "";
        public string NicNumber { get; set; } = "";
        public string TableId { get; set; } = "";
        public string policy_year { get; set; } = "";

        //---- when adding a field update the apiServices.GetPoliciesAsync method to re-initilize the field

        private SearchCriteriaLife() { }

        public static SearchCriteriaLife Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
