using OxyPlot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class AgtPerfmStat
    {
        public int agentID { get; set; }
        public string agentName { get; set; }
        public int year { get; set; }
        public string month { get; set; }
        public bool IsSelected { get; set; }
        // ---------------------- Individual Total --------------------------
        public double indMonthNoPolTotal { get; set; }
        public double indYearPolTotal { get; set; }
        public double indMonthPremTotal { get; set; }
        public string indMonthPremTotal_str { get; set; }
        public double indYearPremTotal { get; set; }
        public string indYearPremTotal_str { get; set; }

        public double indMonthNoPolTotal_cash { get; set; }
        public double indYearPolTotal_cash { get; set; }
        public double indMonthPremTotal_cash { get; set; }
        public double indYearPremTotal_cash { get; set; }

        public double indMonthNoPolTotal_Dbt{ get; set; }
        public double indYearPolTotal_Dbt { get; set; }
        public double indMonthPremTotal_Dbt { get; set; }
        public double indYearPremTotal_Dbt { get; set; }

        public double branchMonthNoPolTotal { get; set; }
        public double branchYearPolTotal { get; set; }
        public double branchMonthPremTotal { get; set; }
        public double branchYearPremTotal { get; set; }

        public double indMonthNoPol_New { get; set; }
        public double indYearPol_New { get; set; }
        public double indMonthPrem_New { get; set; }
        public string indMonthPrem_New_str { get; set; }
        public double indYearPrem_New { get; set; }
        public string indYearPrem_New_str { get; set; }

        public double indMonthNoPol_New_Cash { get; set; }
        public double indYearPol_New_Cash { get; set; }
        public double indMonthPrem_New_Cash { get; set; }
        public double indYearPrem_New_Cash { get; set; }

        public double indMonthNoPol_New_Dbt { get; set; }
        public double indYearPol_New_Dbt { get; set; }
        public double indMonthPrem_New_Dbt { get; set; }
        public double indYearPrem_New_Dbt { get; set; }

        public double branchMonthNoPol_New { get; set; }
        public double branchYearPol_New { get; set; }
        public double branchMonthPrem_New { get; set; }
        public double branchYearPrem_New { get; set; }

        public double indMonthNoPol_Renewal { get; set; }
        public double indYearPol_Renewal { get; set; }
        public double indMonthPrem_Renewal { get; set; }
        public string indMonthPrem_Renewal_str { get; set; }
        public double indYearPrem_Renewal { get; set; }
        public string indYearPrem_Renewal_str { get; set; }

        public double indMonthNoPol_Renewal_cash { get; set; }
        public double indYearPol_Renewal_cash { get; set; }
        public double indMonthPrem_Renewal_cash { get; set; }
        public double indYearPrem_Renewal_cash { get; set; }

        public double indMonthNoPol_Renewal_Dbt { get; set; }
        public double indYearPol_Renewal_Dbt { get; set; }
        public double indMonthPrem_Renewal_Dbt { get; set; }
        public double indYearPrem_Renewal_Dbt { get; set; }

        public double branchMonthNoPol_Renewal { get; set; }
        public double branchYearPol_Renewal { get; set; }
        public double branchMonthPrem_Renewal { get; set; }
        public double branchYearPrem_Renewal { get; set; }




    }
}
