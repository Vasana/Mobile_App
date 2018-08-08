using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class MonthlyPerformance
    {
        //public string BUSS_TYPE { get; set; }
        //public int Y_MONTH { get; set; }
        //public int NO_BUSINESS { get; set; }
        //public double AMOUNT { get; set; }
        public int YEAR_MONTH { get; set; }
        public int NO_OF_NEW_BUSINESS { get; set; }
        public double TOTAL_NEW_BUSINESS_PREMIUM { get; set; }
        public int NO_OF_RENEWAL_BUSINESS { get; set; }
        public double TOTAL_RENEWAL_BUSINESS_PREMIUM { get; set; }
        public int NO_OF_TOTAL_BUSINESS { get; set; }
        public double TOTAL_PREMIUM { get; set; }
        public double TOTAL_REFUND { get; set; }
        public double NEW_BUSINESS_AVERAGE { get; set; }
        public double NEW_BUSINESS_PREMIUM_AVERAGE { get; set; }
        public double RENEWAL_BUSINESS_AVERAGE { get; set; }
        public double RENEWAL_PREMIUM_AVERAGE { get; set; }
        public double TOTAL_BUSINESS_AVERAGE { get; set; }
        public double TOTAL_PREMIUM_AVERAGE { get; set; }
        public int MAX_NEW_BUSINESS_COUNT { get; set; }
        public int MAX_RENEWAL_BUSINESS_COUNT { get; set; }
        public int MAX_TOTAL_BUSINESS_COUNT { get; set; }
        public double MAX_NEW_BUSINESS_PREMIUM { get; set; }

    }
}
