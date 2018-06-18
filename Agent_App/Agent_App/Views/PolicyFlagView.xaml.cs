using Agent_App.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agent_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PolicyFlagView
	{
        private string PolicyNumber { get; set; }
        private string Agentcomment { get; set; }

        public PolicyFlagView (string policyNum, string comment, string remindDate)
		{
			InitializeComponent ();

            var closeImage = new TapGestureRecognizer();
            closeImage.Tapped += CloseImage_Tapped;
            btnExit.GestureRecognizers.Add(closeImage);

            PolicyNumber = policyNum;
            Agentcomment = comment;
            if (Agentcomment != null)
            {
                entComment.Text = Agentcomment;
            }

            if (remindDate != null)
            {
                remindDtPicker.Date = DateTime.ParseExact(remindDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
            }
        }

        private void CloseImage_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        private void btnSubmit_Clicked(object sender, EventArgs e)
        {
            PolicyFlag.Instance.PolicyNumber = PolicyNumber.Trim();
            
            if (entComment.Text != null)
            {
                PolicyFlag.Instance.Comment = entComment.Text.Trim();
            }
            PolicyFlag.Instance.Flagged = true;
            PolicyFlag.Instance.RemindOnDate = remindDtPicker.Date.ToString("dd-MMM-yyyy");

            PopupNavigation.Instance.PopAsync(true);
        }

        private void btnUnflag_Clicked(object sender, EventArgs e)
        {
            PolicyFlag.Instance.PolicyNumber = PolicyNumber.Trim();
            
            PolicyFlag.Instance.Flagged = false;

            PopupNavigation.Instance.PopAsync(true);
        }
    }
}