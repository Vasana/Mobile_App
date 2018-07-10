using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agent_App.Models;
using Agent_App.ViewModels;

namespace Agent_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificViewTemplate : ContentView
	{
		public NotificViewTemplate ()
		{
			InitializeComponent ();

            //Creating TapGestureRecognizer
            var callImage = new TapGestureRecognizer();
            //Binding events
            callImage.Tapped += CallImage_Tapped;
            //Associating tap events to the image buttons
            btnCall.GestureRecognizers.Add(callImage);            
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

        private async void btnNotif_Clicked(object sender, EventArgs e)
        {
            var notif = BindingContext as Notification;
            if (notif.BusinessType == "G")
            {
                var genPolVM = new GenPolViewModel(notif.Department, notif.PolicyNumber);

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

        private async void btnClear_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as NotificsViewModel;
            var notif = BindingContext as Notification;

            bool ret = await vm.ClearNotifAsync(notif);
        }
    }
}