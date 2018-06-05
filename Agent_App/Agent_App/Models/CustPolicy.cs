﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class CustPolicy
    {
        public string PolicyNumber { get; set; }
        public string AgentCode { get; set; }
        public string InsuredName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Department { get; set; }
        public string PolicyType { get; set; }
        public string PolTypeDesc { get; set; }
        public string VehicleNumber { get; set; }
        public bool IsSelected { get; set; }
        public string PolTypeImage { get; set; }
        public string PolStatusImage { get; set; }
        public string ClaimStatusImage { get; set; }
        public string MobileNumber { get; set; }
        public bool MotorPolicy { get; set; }
        public string AgentComment { get; set; }
        public bool Flagged { get; set; }
        public string FlagImage { get; set; }


        internal void SetFlag()
        {
            string policyNo = PolicyFlag.Instance.PolicyNumber;
            string comment = PolicyFlag.Instance.Comment;
            bool flagged = PolicyFlag.Instance.Flagged;            

            //string polNo = policy.PolicyNumber;
            if (this.PolicyNumber == policyNo)
            {
                this.AgentComment = comment;
                this.Flagged = flagged;
                this.FlagImage = "filledStar.jpg";                
            }

            PolicyFlag.Instance.PolicyNumber = null;
            PolicyFlag.Instance.Comment = null;
            PolicyFlag.Instance.Flagged = false;
        }
        
    }
}
