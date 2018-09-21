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
using System.Globalization;

namespace Agent_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PolicySearchView
	{
        //DateTime minPickerDate;
        //DateTime maxPickerDate;

		public PolicySearchView ()
		{
			InitializeComponent ();
            BusiTypePicker.SelectedIndex = 0;
            //minPickerDate = stFromDtPicker.Date;
            //maxPickerDate = stToDtPicker.Date;

            //entPolicyNumber.IsEnabled = false;
            //entVehiNum1.IsEnabled = false;
            //entVehiNum2.IsEnabled = false;
            //stFromDtPicker.IsEnabled = false;
            //entStartFromDt.IsEnabled = false;
            //stToDtPicker.IsEnabled = false;
            //entStartToDt.IsEnabled = false;
            //entMobileNumber.IsEnabled = false;
            //entStartFromDt.Keyboard = null;
            //entStartToDt.Keyboard = null;

            var closeImage = new TapGestureRecognizer();
            closeImage.Tapped += CloseImage_Tapped;
            btnExit.GestureRecognizers.Add(closeImage);
        }

        private void CloseImage_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        private void btnSubmit_Clicked(object sender, EventArgs e)
        {
            try
            {
                int busiType = BusiTypePicker.SelectedIndex;

                if(busiType == 1)
                {
                    SearchCriteria.Instance.BusinessType = "M";
                }
                else if (busiType == 2)
                {
                    SearchCriteria.Instance.BusinessType = "G";
                }

                int policyStatus = typePicker.SelectedIndex;
                if (policyStatus == 0)
                {
                    SearchCriteria.Instance.PremiumsPending = true;
                }
                else if (policyStatus == 1)
                {
                    SearchCriteria.Instance.DebitOutstanding = true;
                }
                else if (policyStatus == 2)
                {
                    SearchCriteria.Instance.ClaimPending = true;
                }
                else if (policyStatus == 3)
                {
                    SearchCriteria.Instance.Flagged = true;
                }
                else if (policyStatus == 4)
                {
                    SearchCriteria.Instance.BadClaims = true;
                }

                if (entPolicyNumber.Text != null)
                {
                    SearchCriteria.Instance.BusinessType = "M";
                    SearchCriteria.Instance.PolicyNumber = entPolicyNumber.Text.ToUpper().Trim();
                }
                
                if (entVehiNum1.Text != null || entVehiNum2.Text != null)
                {
                    SearchCriteria.Instance.VehicleNumber = entVehiNum1.Text.Trim().ToUpper() + " " + entVehiNum2.Text.Trim();
                }
                if (entMobileNumber.Text != null)
                {                    
                    int mobileNo = int.Parse(entMobileNumber.Text.Trim());                   
                    SearchCriteria.Instance.MobileNumber = entMobileNumber.Text.Trim();
                }

                if (entStartFromDt.Text != null && entStartFromDt.Text != "")
                {
                    DateTime fromDate = DateTime.ParseExact(entStartFromDt.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    SearchCriteria.Instance.StartFromDt = fromDate.ToString("yyyy/MM/dd");
                }
                else
                {
                    SearchCriteria.Instance.StartFromDt = "1990/01/01";
                }
                if (entStartToDt.Text != null && entStartToDt.Text != "")
                {
                    DateTime toDate = DateTime.ParseExact(entStartToDt.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    SearchCriteria.Instance.StartToDt = toDate.ToString("yyyy/MM/dd");
                }
                else
                {
                    SearchCriteria.Instance.StartToDt = "2100/01/01";
                }                    

                if (policyStatus == -1)
                {
                    SearchCriteria.Instance.AllPolicies = true;
                }
                SearchCriteria.Instance.NewSearch = true;
                SearchCriteria.Instance.ListDesc = "Search Result List";

                //MessagingCenter.Send<MainPage, string>(, "SearchPolicy", "John");
                PopupNavigation.Instance.PopAsync(true);
            }
            catch (Exception e2)
            {
                DisplayAlert("Search Error", "Invalid Search Options." + e2.ToString(), "OK");
                SearchCriteria.Instance.NewSearch = false;
                SearchCriteria.Instance.BusinessType = "A";
                SearchCriteria.Instance.PremiumsPending = false;
                SearchCriteria.Instance.ClaimPending = false;
                SearchCriteria.Instance.Flagged = false;
                SearchCriteria.Instance.BadClaims = false;
                SearchCriteria.Instance.DebitOutstanding = false;
                SearchCriteria.Instance.AllPolicies = false;
                SearchCriteria.Instance.PolicyNumber = "";
                SearchCriteria.Instance.VehicleNumber = "";
                SearchCriteria.Instance.StartFromDt = "";
                SearchCriteria.Instance.StartToDt = "";
                SearchCriteria.Instance.TopTen = false;
                SearchCriteria.Instance.TodayReminders = false;
                SearchCriteria.Instance.MobileNumber = "";
            }
        }

        private void typePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = typePicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                entPolicyNumber.Text = null;
                entVehiNum1.Text = null;
                entVehiNum2.Text = null;                
                stFromDtPicker.Date = DateTime.Now;                
                stToDtPicker.Date = DateTime.Now;
                entStartFromDt.Text = null;
                entStartToDt.Text = null;
                entMobileNumber.Text = null;

                entPolicyNumber.IsEnabled = false;
                entVehiNum1.IsEnabled = false;
                entVehiNum2.IsEnabled = false;
                entStartFromDt.IsEnabled = false;
                stFromDtPicker.IsEnabled = false;
                entStartToDt.IsEnabled = false;
                stToDtPicker.IsEnabled = false;
                entMobileNumber.IsEnabled = false;


                //if (selectedIndex == 5)
                //{
                //    DisplayAlert("Search Alert", "Choosing All Policies may take a long time to process depending on the number of policies.", "OK");
                //}
            }
            else
            {
                entPolicyNumber.IsEnabled = true;
                entVehiNum1.IsEnabled = true;
                entVehiNum2.IsEnabled = true;
                entStartFromDt.IsEnabled = true;
                stFromDtPicker.IsEnabled = true;
                entStartToDt.IsEnabled = true;
                stToDtPicker.IsEnabled = true;
                entMobileNumber.IsEnabled = true;
            }
        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {
            BusiTypePicker.SelectedIndex = 0;
            typePicker.SelectedIndex = -1;
            entPolicyNumber.Text = null;
            entVehiNum1.Text = null;
            entVehiNum2.Text = null;
            stFromDtPicker.Date = DateTime.Now;
            stToDtPicker.Date = DateTime.Now;
            entStartFromDt.Text = null;
            entStartToDt.Text = null;
            entMobileNumber.Text = null;
            //BusiTypePicker.Focus();
        }

        private void BusiTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = BusiTypePicker.SelectedIndex;

            if (selectedIndex != 0)
            {
                entPolicyNumber.IsEnabled = true;
                entVehiNum1.IsEnabled = true;
                entVehiNum2.IsEnabled = true;
                entStartFromDt.IsEnabled = true;
                stFromDtPicker.IsEnabled = true;
                entStartToDt.IsEnabled = true;
                stToDtPicker.IsEnabled = true;
                entMobileNumber.IsEnabled = true;

                entPolicyNumber.Text = null;
                entVehiNum1.Text = null;
                entVehiNum2.Text = null;
                stFromDtPicker.Date = DateTime.Now;
                stToDtPicker.Date = DateTime.Now;
                entStartFromDt.Text = null;
                entStartToDt.Text = null;
                entMobileNumber.Text = null;
            }
            else
            {
                entPolicyNumber.Text = null;
                entVehiNum1.Text = null;
                entVehiNum2.Text = null;
                stFromDtPicker.Date = DateTime.Now;
                stToDtPicker.Date = DateTime.Now;
                entStartFromDt.Text = null;
                entStartToDt.Text = null;
                entMobileNumber.Text = null;

                //entPolicyNumber.IsEnabled = false;
                //entVehiNum1.IsEnabled = false;
                //entVehiNum2.IsEnabled = false;
                //entStartFromDt.IsEnabled = false;
                //stFromDtPicker.IsEnabled = false;
                //entStartToDt.IsEnabled = false;
                //stToDtPicker.IsEnabled = false;
                //entMobileNumber.IsEnabled = false;
            }
        }

        private void entMobileNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (entMobileNumber.Text != "")
            //{
            //    entPolicyNumber.IsEnabled = false;
            //    entVehiNum1.IsEnabled = false;
            //    entVehiNum2.IsEnabled = false;
            //}
            //else
            //{
            //    entPolicyNumber.IsEnabled = true;
            //    entVehiNum1.IsEnabled = true;
            //    entVehiNum2.IsEnabled = true;
            //}
            if (entMobileNumber.Text!= null && entMobileNumber.Text != "")
            {
                if (BusiTypePicker.SelectedIndex == 0)
                {
                    DisplayAlert("Message", "Please select Business Type first", "OK");
                    entMobileNumber.Text = "";
                    //BusiTypePicker.Focus();
                }
            }
        }

        private void entPolicyNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (entPolicyNumber.Text != "")
            //{
            //    entMobileNumber.IsEnabled = false;
            //    entVehiNum1.IsEnabled = false;
            //    entVehiNum2.IsEnabled = false;
            //}
            //else
            //{
            //    entMobileNumber.IsEnabled = true;
            //    entVehiNum1.IsEnabled = true;
            //    entVehiNum2.IsEnabled = true;
            //}
            if (entPolicyNumber.Text != null && entPolicyNumber.Text != "")
            {
                if (BusiTypePicker.SelectedIndex == 0)
                {
                    DisplayAlert("Message", "Please select Business Type first", "OK");
                    entPolicyNumber.Text = "";
                    //BusiTypePicker.Focus();
                }
            }
        }

        private void entVehiNum1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (entVehiNum1.Text != null && entVehiNum1.Text != "")
            //{
            //    entMobileNumber.IsEnabled = false;
            //    entPolicyNumber.IsEnabled = false;                
            //}
            //else
            //{
            //    entMobileNumber.IsEnabled = true;
            //    entPolicyNumber.IsEnabled = true;                
            //}
            if (entVehiNum1.Text != "")
            {
                //if (BusiTypePicker.SelectedIndex == 0)
                //{
                //    DisplayAlert("Message", "Please select Business Type first", "OK");
                //    entVehiNum1.Text = "";
                //}
                //else
                //{
                    if (e.NewTextValue != null)
                    {
                        if (e.NewTextValue.Length > 2)
                        {
                            //entVehiNum1.Text = entVehiNum1.Text.Remove(3);
                            entVehiNum2.Focus();
                        }
                    }
                //}
            }
        }

        private void entVehiNum2_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (entVehiNum2.Text != "")
            //{
            //    entMobileNumber.IsEnabled = false;
            //    entPolicyNumber.IsEnabled = false;
            //}
            //else
            //{
            //    entMobileNumber.IsEnabled = true;
            //    entPolicyNumber.IsEnabled = true;
            //}
            //if (entVehiNum2.Text != "")
            //{
            //    if (BusiTypePicker.SelectedIndex == 0)
            //    {
            //        DisplayAlert("Message", "Please select Business Type first", "OK");
            //        entVehiNum2.Text = "";
            //    }
            //}
        }

        private void entStartFromDt_Focused(object sender, FocusEventArgs e)
        {
            if (BusiTypePicker.SelectedIndex == 0)
            {
                DisplayAlert("Message", "Please select Business Type first", "OK");
                entStartFromDt.Text = "";
                //BusiTypePicker.Focus();
            }
            else
            {
                entStartFromDt.Text = DateTime.Now.ToString("yyyy/MM/dd");
                stFromDtPicker.Focus();
            }
        }

        private void stFromDtPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            entStartFromDt.Text = stFromDtPicker.Date.ToString("yyyy/MM/dd");
        }

        private void entStartToDt_Focused(object sender, FocusEventArgs e)
        {
            if (BusiTypePicker.SelectedIndex == 0)
            {
                DisplayAlert("Message", "Please select Business Type first", "OK");
                entStartToDt.Text = "";
                //BusiTypePicker.Focus();
            }
            else
            {
                entStartToDt.Text = DateTime.Now.ToString("yyyy/MM/dd");
                stToDtPicker.Focus();
            }
        }

        private void stToDtPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            entStartToDt.Text = stToDtPicker.Date.ToString("yyyy/MM/dd");
        }

        private void entStartFromDt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entStartFromDt.Text != null && entStartFromDt.Text != "")
            {
                if (BusiTypePicker.SelectedIndex == 0)
                {
                    DisplayAlert("Message", "Please select Business Type first", "OK");
                    entStartFromDt.Text = "";
                    //BusiTypePicker.Focus();
                }
            }
        }

        private void entStartToDt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entStartToDt.Text != null && entStartToDt.Text != "")
            {
                if (BusiTypePicker.SelectedIndex == 0)
                {
                    DisplayAlert("Message", "Please select a specific Business Type", "OK");
                    entStartToDt.Text = "";
                    //BusiTypePicker.Focus();
                }
            }
        }

        private void entPolicyNumber_Focused(object sender, FocusEventArgs e)
        {
            //if (BusiTypePicker.SelectedIndex == 0)
            //{
            //    DisplayAlert("Message", "Please select Business Type first7", "OK");
            //    entPolicyNumber.Text = "";
            //    //BusiTypePicker.Focus();
            //}
            
        }

        private void entMobileNumber_Focused(object sender, FocusEventArgs e)
        {
            if (BusiTypePicker.SelectedIndex == 0)
            {
                DisplayAlert("Message", "Please select Business Type first", "OK");
                entMobileNumber.Text = "";
                //BusiTypePicker.Focus();
            }
           
        }
    }
}