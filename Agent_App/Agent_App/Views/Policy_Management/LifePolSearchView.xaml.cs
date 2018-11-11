using Agent_App.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agent_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LifePolSearchView
	{
		public LifePolSearchView ()
		{
			InitializeComponent ();

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
                int policyStatus = typePicker.SelectedIndex;
                if (policyStatus == 0)
                {
                    SearchCriteriaLife.Instance.inforce_pol = true;
                }
                else if (policyStatus == 1)
                {
                    SearchCriteriaLife.Instance.lapsed_pol = true;
                }
                else if (policyStatus == 2)
                {
                    SearchCriteriaLife.Instance.templapse_pol = true;
                }
                else if (policyStatus == 3)
                {
                    SearchCriteriaLife.Instance.Flagged = true;
                }
                
                if (entPolicyNumber.Text != null)
                {
                    SearchCriteriaLife.Instance.PolicyNumber = entPolicyNumber.Text.ToUpper().Trim();
                }                
                if (entNicNumber.Text != null)
                {
                    SearchCriteriaLife.Instance.NicNumber = entNicNumber.Text.Trim();
                }
                //if (entTable.Text != null)
                //{
                //    SearchCriteriaLife.Instance.TableId = entTable.Text.Trim();
                //}
                if (policyStatus == -1)
                {
                    SearchCriteriaLife.Instance.AllPolicies = true;
                }
                SearchCriteriaLife.Instance.NewSearch = true;
                SearchCriteriaLife.Instance.ListDesc = "Search Result List";

                //MessagingCenter.Send<MainPage, string>(, "SearchPolicy", "John");
                PopupNavigation.Instance.PopAsync(true);
            }
            catch (Exception e2)
            {
                DisplayAlert("Search Error", "Invalid Search Options." + e2.ToString(), "OK");
                SearchCriteriaLife.Instance.NewSearch = false;
                SearchCriteriaLife.Instance.inforce_pol = false;
                SearchCriteriaLife.Instance.lapsed_pol = false;
                SearchCriteriaLife.Instance.templapse_pol = false;
                SearchCriteriaLife.Instance.Flagged = false;
                SearchCriteriaLife.Instance.AllPolicies = false;
                SearchCriteriaLife.Instance.PolicyNumber = "";
                SearchCriteriaLife.Instance.TopTen = false;
                SearchCriteriaLife.Instance.TodayReminders = false;
                SearchCriteriaLife.Instance.NicNumber = "";
                SearchCriteriaLife.Instance.TableId = "";
            }
        }

        private void typePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = typePicker.SelectedIndex;
            if (selectedIndex != -1)
            {
                entPolicyNumber.Text = null;
                entNicNumber.Text = null;
                //entTable.Text = null;
                
                entPolicyNumber.IsEnabled = false;                
                entNicNumber.IsEnabled = false;
               // entTable.IsEnabled = false;
                
            }
            else
            {
                entPolicyNumber.IsEnabled = true;
                entNicNumber.IsEnabled = true;
               // entTable.IsEnabled = true;
            }
        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {
            typePicker.SelectedIndex = -1;
            entPolicyNumber.Text = null;
            entPolicyNumber.IsEnabled = true;
            entNicNumber.Text = null;
            entNicNumber.IsEnabled = true;
            //entTable.Text = null;
            //entTable.IsEnabled = true;
        }

        private void entPolicyNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entPolicyNumber.Text != "")
            {
                entNicNumber.IsEnabled = false;
                //entTable.IsEnabled = false;
            }
            else
            {
                entNicNumber.IsEnabled = true;
                //entTable.IsEnabled = true;
            }
        }

        private void entNicNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (entNicNumber.Text != "")
            {
                entPolicyNumber.IsEnabled = false;
                //entTable.IsEnabled = false;
            }
            else
            {
                entPolicyNumber.IsEnabled = true;
                //entTable.IsEnabled = true;
            }
        }

        //private void entTable_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (entTable.Text != "")
        //    {
        //        entPolicyNumber.IsEnabled = false;
        //        entNicNumber.IsEnabled = false;
        //    }
        //    else
        //    {
        //        entPolicyNumber.IsEnabled = true;
        //        entNicNumber.IsEnabled = true;
        //    }
        //}
    }
}