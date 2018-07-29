using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
     public class PremiumHistory
    {
        public string POLICY_START_DATE { get; set; }
        public string POLICY_END_DATE { get; set; }
        public double TOTAL_PREMIUM { get; set; }
        public double BASIC_PREMIUM { get; set; }
        public double RCC { get; set; }
        public double TC { get; set; }
        public int CLAIM_COUNT { get; set; }
        public double NCB_ADJ_PREMIUM { get; set; }
        public double PAID_PREMIUM { get; set; }
        public string LAST_PAY_DATE { get; set; }
    }
}
