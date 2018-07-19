using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class ClaimHistory
    {
        public string INV_NO { get; set; } 
        public string STATUS { get; set; } 
        public string DATE_OF_LOSS { get; set; } 
        public string INT_DATE { get; set; }
        public string REG_DATE { get; set; } 
        public double EST_LIB { get; set; }
        public string PAY_TYP { get; set; } 
        public string VOU_STS { get; set; } 
        public double PAD_AMOUNT { get; set; }
        public string PAY_DATE { get; set; }
        public string VOU_NO { get; set; }
    }
}
