using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class AgentPerformance
    {
        public int AGENT_CODE { get; set; }
        public string AGENT_NAME { get; set; }
        public int CASH_NEW_MOTOR { get; set; }
        public int DEBIT_NEW_MOTOR { get; set; }
        public int CASH_REN_MOTOR { get; set; }
        public int DEBIT_REN_MOTOR { get; set; }
        public int TOT_CASH_MOTOR { get; set; }
        public int TOT_DEBIT_MOTOR { get; set; }
        public double CASH_NEW_MOTOR_PRM { get; set; }
        public double DEBIT_NEW_MOTOR_PRM { get; set; }
        public double CASH_REN_MOTOR_PRM { get; set; }
        public double DEBIT_REN_MOTOR_PRM { get; set; }
        public double TOT_CASH_MOTOR_PRM { get; set; }
        public double TOT_DEBIT_MOTOR_PRM { get; set; }
        public int CASH_NEW_NON_MOTOR { get; set; }
        public int DEBIT_NEW_NON_MOTOR { get; set; }
        public int CASH_REN_NON_MOTOR { get; set; }
        public int DEBIT_REN_NON_MOTOR { get; set; }
        public int TOT_CASH_NON_MOTOR { get; set; }
        public int TOT_DEBIT_NON_MOTOR { get; set; }
        public double CASH_NEW_NON_MOTOR_PRM { get; set; }
        public double DEBIT_NEW_NON_MOTOR_PRM { get; set; }
        public double CASH_REN_NON_MOTOR_PRM { get; set; }
        public double DEBIT_REN_NON_MOTOR_PRM { get; set; }
        public double TOT_CASH_NON_MOTOR_PRM { get; set; }
        public double TOT_DEBIT_NON_MOTOR_PRM { get; set; }
        
        public double TOT_REFUND { get; set; }
    }
}
