using OxyPlot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class AgtPerfmStat
    {
        public int agentID { get; set; }
        public double indMonthNoPolTotal { get; set; }
        public double indYearPolTotal { get; set; }
        public double indMonthPremTotal { get; set; }
        public double indYearPremTotal { get; set; }

        public double branchMonthNoPolTotal { get; set; }
        public double branchYearPolTotal { get; set; }
        public double branchMonthPremTotal { get; set; }
        public double branchYearPremTotal { get; set; }

        public double indMonthNoPolCash { get; set; }
        public double indYearPolCash { get; set; }
        public double indMonthPremCash { get; set; }
        public double indYearPremCash { get; set; }

        public double indMonthNoPolDbt { get; set; }
        public double indYearPolDbt { get; set; }
        public double indMonthPremDbt { get; set; }
        public double indYearPremDbt { get; set; }

       
        

    }
}
