using Agent_App.Helpers;
using Agent_App.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
        public string AgentComment { get; set; } = "";
        public bool Flagged { get; set; }
        public string FlagImage { get; set; }
        public string RemindOnDate { get; set; } = "";// yyyy/MM/dd
        public string CommentCreatedDate { get; set; } = "";
        public string PremiumTypeImg { get; set; } = ""; //new or renewal
        public string DebitCancelDate { get; set; } = "";
        public string DebitNoteNumber { get; set; } = "";
        public string DebitOutstAmt { get; set; } = "";

        ApiServices _apiServices = new ApiServices();

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
