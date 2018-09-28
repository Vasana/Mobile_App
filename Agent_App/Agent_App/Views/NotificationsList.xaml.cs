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
	public partial class NotificationsList : ContentPage
	{
        public NotificationsList()
        {
            InitializeComponent();
        }

        // Use like click button event
        void CallImage_Tapped(object sender, EventArgs e)
        {
            //string mobileNumber = lblMobileNo.Text;
            TapGestureRecognizer tapGesture = sender as TapGestureRecognizer;

            var notific = tapGesture.BindingContext as Notification;

            var mobileNumber = notific.ContactNumber;
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

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as NotificsViewModel;
            var notif = e.Item as Notification;

            vm.HideOrShowNotif(notif);

        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as NotificsViewModel;

            await vm.ClearNotifsAsync();

            //if (!ret)
            //{
            //    await DisplayAlert("Delete Error", "Error occured while removing.", "OK");
            //}
        }

        private async void btnClear_Clicked(object sender, EventArgs e)
        {
            var rst = await DisplayAlert("Notification", "Are you sure you want to Delete ???", "Yes", "No");
            if (rst)
            {
                var button = sender as Button;
                var notif = button.BindingContext as Notification;
                var vm = BindingContext as NotificsViewModel;
                await vm.ClearNotifsAsync();

                //if (!ret)
                //{
                //    await DisplayAlert("Delete Error", "Error occured while removing.", "OK");
                //}
            }
        }

        private async void btnNotif_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var notif = button.BindingContext as Notification;

            if (notif.BusinessType == "G")
            {
                var genPolVM = new GenPolViewModel(notif.Department, notif.PolicyNumber);

                var genPolicyPage = new GenPolicyDetails
                {
                    BindingContext = genPolVM
                };

                if (App.Current.MainPage is NavigationPage)
                {
                    await(App.Current.MainPage as NavigationPage).PushAsync(genPolicyPage);
                }
            }
        }

        private void btnCall_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var notific = button.BindingContext as Notification;

            var mobileNumber = notific.ContactNumber;
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

        private void btnEmail_Clicked(object sender, EventArgs e)
        {

        }

        private void btnMark_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var notif = button.BindingContext as Notification;

            notif.IsMarked = !notif.IsMarked;

            if (notif.IsMarked)
            {
                //button.BackgroundColor = Color.Green;
                notif.MarkedImage = "selected.png";
                button.Image = "selected.png";
            }
            else
            {
                //button.BackgroundColor = Color.White;
                notif.MarkedImage = "";
                button.Image = "";
            }        
           
        }

        private void btnMarkAll_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as NotificsViewModel;
            vm.MarkAllNotifsAsync();
        }
    }


}