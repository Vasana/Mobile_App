using Agent_App.Models;
using Agent_App.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agent_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PolicySearchView
	{
        DateTime minPickerDate;
        DateTime maxPickerDate;

		public PolicySearchView ()
		{
			InitializeComponent ();
            minPickerDate = stFromDtPicker.Date;
            maxPickerDate = stToDtPicker.Date;
		}

        private void btnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = typePicker.SelectedIndex;
                if (selectedIndex == 0)
                {
                    SearchCriteria.Instance.PremiumsPending = true;
                }
                else if (selectedIndex == 1)
                {
                    SearchCriteria.Instance.DebitOutstanding = true;
                }
                else if (selectedIndex == 2)
                {
                    SearchCriteria.Instance.ClaimPending = true;
                }
                else if (selectedIndex == 3)
                {
                    SearchCriteria.Instance.Flagged = true;
                }
                else if (selectedIndex == 4)
                {
                    SearchCriteria.Instance.BadClaims = true;
                }
                else if (selectedIndex == 5)
                {
                    SearchCriteria.Instance.AllPolicies = true;
                }

                if (entPolicyNumber.Text != null)
                {
                    SearchCriteria.Instance.PolicyNumber = entPolicyNumber.Text.Trim();
                }
                if (entVehiNum1.Text != null || entVehiNum2.Text != null)
                {
                    SearchCriteria.Instance.VehicleNumber = entVehiNum1.Text.Trim() + " " + entVehiNum2.Text.Trim();
                }

               // if (stFromDtPicker.Date != minPickerDate || stToDtPicker.Date != maxPickerDate)
               // {
                    SearchCriteria.Instance.StartFromDt = stFromDtPicker.Date.ToString("yyyy/MM/dd");
                    SearchCriteria.Instance.StartToDt = stToDtPicker.Date.ToString("yyyy/MM/dd");
               // }

                SearchCriteria.Instance.NewSearch = true;
                if (selectedIndex == -1)
                {
                    SearchCriteria.Instance.AllPolicies = true;
                }

                //MessagingCenter.Send<MainPage, string>(, "SearchPolicy", "John");
                PopupNavigation.Instance.PopAsync(true);
            }
            catch
            {
                DisplayAlert("Search Error", "Invalid Search Options.", "OK");
            }
        }

        private void typePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = typePicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                entPolicyNumber.IsEnabled = false;
                entVehiNum1.IsEnabled = false;
                entVehiNum2.IsEnabled = false;
                stFromDtPicker.IsEnabled = false;
                stToDtPicker.IsEnabled = false;

                if (selectedIndex == 5)
                {
                    DisplayAlert("Search Alert", "Choosing All Policies may take a long time to process depending on the number of policies.", "OK");
                }
            }
            else
            {
                entPolicyNumber.IsEnabled = true;
                entVehiNum1.IsEnabled = true;
                entVehiNum2.IsEnabled = true;
                stFromDtPicker.IsEnabled = true;
                stToDtPicker.IsEnabled = true;
            }
        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {
            typePicker.SelectedIndex = -1;
            entPolicyNumber.Text = "";
            entVehiNum1.Text = "";
            entVehiNum2.Text = "";
            stFromDtPicker.Date = minPickerDate;
            stToDtPicker.Date = maxPickerDate;
        }
    }
}