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

namespace Agent_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LifeRemindsTemplate : ContentView
	{
		public LifeRemindsTemplate ()
		{
			InitializeComponent ();

            //Creating TapGestureRecognizer
            var callImage = new TapGestureRecognizer();
            //Binding events
            callImage.Tapped += CallImage_Tapped;
            //Associating tap events to the image buttons
            btnCall.GestureRecognizers.Add(callImage);

            var flagImage = new TapGestureRecognizer();
            flagImage.Tapped += flagImage_Tapped;
            btnReminder.GestureRecognizers.Add(flagImage);

            var smsImage = new TapGestureRecognizer();
            smsImage.Tapped += SmsImage_Tapped;
            btnSMS.GestureRecognizers.Add(smsImage);
        }

        private void SmsImage_Tapped(object sender, EventArgs e)
        {
            string mobileNumber = lblMobileNo.Text;
            try
            {
                if (mobileNumber != "")
                {
                    Device.OpenUri(new Uri("sms:" + mobileNumber));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "No information found", "OK");
                }

            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Error occured while performing function", "OK");
            }
        }

        private void flagImage_Tapped(object sender, EventArgs e)
        {
            var policy = BindingContext as CustPolicyLife;
            PolicyFlagView flagView = new PolicyFlagView(policy.PolicyNumber, policy.AgentComment, policy.RemindOnDate);
            flagView.Disappearing += FlagView_Disappearing;
            PopupNavigation.Instance.PushAsync(flagView);

            //Device.BeginInvokeOnMainThread(async () => await PopupNavigation.Instance.PushAsync(flagView));
        }

        private void FlagView_Disappearing(object sender, EventArgs e)
        {
            var policy = BindingContext as CustPolicyLife;
            policy.SetFlag();
            if (policy.Flagged)
            {
                btnReminder.Source = "filledStar.jpg";
            }
            else
            {
                btnReminder.Source = "starFrame.png";
            }
            ((PolicyFlagView)sender).Disappearing -= FlagView_Disappearing;
        }

        // Use like click button event
        void CallImage_Tapped(object sender, EventArgs e)
        {
            string mobileNumber = lblMobileNo.Text;
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

        private async void btnPolicy_Clicked(object sender, EventArgs e)
        {
            var policy = BindingContext as CustPolicyLife;
            var genPolVM = new LifePolViewModel(policy);

            var genPolicyPage = new GenPolicyDetails
            {
                BindingContext = genPolVM
            };

            if (App.Current.MainPage is NavigationPage)
            {
                await (App.Current.MainPage as NavigationPage).PushAsync(genPolicyPage);
            }

        }
    }
}