using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agent_App.ViewModels;

namespace Agent_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BranchContactsPage : ContentPage
    {
        private readonly object cntNum1;

        public BranchContactsPage()
        {
            InitializeComponent();            
        }

        private void SearchBranch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = SearchBranch.Text;
            var vm = BindingContext as BranchCntViewModel;
            if (keyword != "")
            {
                LvBranches.ItemsSource = vm.BranchesList.Where(brList => (brList.Name.ToLower()+ " " + brList.District.ToLower()).Contains(keyword.ToLower()));
            }
            else
            {
                LvBranches.ItemsSource = vm.BranchesList;
            }

        }

        private void cntNum1_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string mobileNumber = btn.Text.Trim();
            try
            {
                if (mobileNumber != "")
                {
                    Device.OpenUri(new Uri("tel:" + mobileNumber));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "No information found", "OK");
                }

            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Error occured while performing function", "OK");
            }
        }

        private void cntNum2_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string mobileNumber = btn.Text.Trim();
            try
            {
                if (mobileNumber != "")
                {
                    Device.OpenUri(new Uri("tel:" + mobileNumber));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "No information found", "OK");
                }

            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Error occured while performing function", "OK");
            }
        }

        private void cntNum3_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string mobileNumber = btn.Text.Trim();
            try
            {
                if (mobileNumber != "")
                {
                    Device.OpenUri(new Uri("tel:" + mobileNumber));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "No information found", "OK");
                }

            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Error occured while performing function", "OK");
            }
        }

        private void cntNum4_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string mobileNumber = btn.Text.Trim();
            try
            {
                if (mobileNumber != "")
                {
                    Device.OpenUri(new Uri("tel:" + mobileNumber));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "No information found", "OK");
                }

            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Error occured while performing function", "OK");
            }
        }

        private void SMCntNum_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string mobileNumber = btn.Text.Trim();
            try
            {
                if (mobileNumber != "")
                {
                    Device.OpenUri(new Uri("tel:" + mobileNumber));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "No information found", "OK");
                }

            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Error occured while performing function", "OK");
            }
        }

        private void RSMCntNum_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string mobileNumber = btn.Text.Trim();
            try
            {
                if (mobileNumber != "")
                {
                    Device.OpenUri(new Uri("tel:" + mobileNumber));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "No information found", "OK");
                }

            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Error occured while performing function", "OK");
            }
        }

        private void BrnAdmnCntNum_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string mobileNumber = btn.Text.Trim();
            try
            {
                if (mobileNumber != "")
                {
                    Device.OpenUri(new Uri("tel:" + mobileNumber));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "No information found", "OK");
                }

            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Error occured while performing function", "OK");
            }
        }
    }
}