using Agent_App.Helpers;
using Agent_App.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
﻿using System;
using System.Collections.Generic;

namespace Agent_App.Models
{
    public class CustPolicyLife
    {
        public string PolicyNumber { get; set; }
        public string AgentCode { get; set; }
        public string InsuredName { get; set; }
        public string ComDate { get; set; }
        public string Premium { get; set; }
        public string LastPaidDueDate { get; set; }
        public string NoOfOutstandings { get; set; }
        public string PayMode { get; set; }
        public string PolTableNo { get; set; }
        public string PolTerm { get; set; }
        public string PolicyStatus { get; set; }
        public string PolTableTerm { get; set; }
        public bool IsSelected { get; set; }
        public string PolTypeImage { get; set; }
        public string PolStatusImage { get; set; }
        public string MobileNumber { get; set; }
        public string AgentComment { get; set; } = "";
        public bool Flagged { get; set; }
        public string FlagImage { get; set; }
        public string RemindOnDate { get; set; } = "";// yyyy/MM/dd
        public string CommentCreatedDate { get; set; } = "";
        public string PolTypeDesc { get; set; }


    ApiServicesLife _apiServices = new ApiServicesLife();

        public Color BackgroundColor
        {
            get
            {
                if (IsSelected)
                    return Color.FromHex("#FAEBD7");
                else
                    return Color.Transparent;
            }
        }

        public async Task<bool> SetFlag()
        {
            bool updated = false;
            string policyNo = PolicyFlag.Instance.PolicyNumber;
            string comment = PolicyFlag.Instance.Comment;
            bool flagged = PolicyFlag.Instance.Flagged;
            string remindOnDate = PolicyFlag.Instance.RemindOnDate;

            bool ret;
            if (flagged)
            {
                ret = await _apiServices.FlagPolicyAsync(Settings.AccessToken);
            }
            else
            {
                ret = await _apiServices.UnFlagPolicyAsync(Settings.AccessToken);
            }

            if (ret)
            {
                //string polNo = policy.PolicyNumber;
                if (this.PolicyNumber == policyNo)
                {
                    this.AgentComment = comment;
                    this.Flagged = flagged;
                    if (flagged)
                    {
                        this.FlagImage = "filledStar.jpg";
                    }
                    else
                    {
                        this.FlagImage = "starFrame.png";
                    }
                    this.RemindOnDate = remindOnDate;

                    updated = true;
                }
            }

            return updated;
        }
    }
}
