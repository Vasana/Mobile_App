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
	public partial class LifeMenuPage : ContentPage
	{
		public LifeMenuPage ()
		{
			InitializeComponent ();

            if (Device.Idiom == TargetIdiom.Phone)
            {
                HeadingRow1.Height = 0;
                ourGrid.RowSpacing = 10;
                veryFirst.Height = firstRow.Height = 50;
                breaker.Height = 0;

                BtnCusprof.Text = "Policy Details";
                //BtnQuote.Text = "Sales Performance";

                BtnCusprof.Image = null; /*BtnQuote.Image*/
                BtnCusprof.HeightRequest = 50; /*BtnQuote.HeightRequest*/
                BtnCusprof.WidthRequest = 200; /*BtnQuote.WidthRequest*/
                BtnCusprof.BackgroundColor = Color.SkyBlue; /*BtnQuote.BackgroundColor*/
                BtnCusprof.BorderColor = Color.FromHex("#C89400"); /*BtnQuote.BorderColor*/
                BtnCusprof.CornerRadius = 10; /*BtnQuote.CornerRadius*/
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.Idiom == TargetIdiom.Phone)
            {
                MessagingCenter.Send(this, "preventPortrait");
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                var vm = BindingContext as LifeMenuViewModel;
                SearchCriteriaLife.Instance.NewSearch = true;
                SearchCriteriaLife.Instance.TodayReminders = true;
                vm.DownloadPoliciesAsync();
            }
            
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as LifeMenuViewModel;
            var policy = e.Item as CustPolicyLife;

            vm.HideOrShowPolicy(policy);

        }

        private void btnNotif_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotificationsList());
        }

        private void btnRefresh_Clicked(object sender, EventArgs e)
        {
            //var vm = new LifeMenuViewModel();
            //this.BindingContext = vm;


            var vm = BindingContext as LifeMenuViewModel;
            SearchCriteriaLife.Instance.NewSearch = true;
            SearchCriteriaLife.Instance.TodayReminders = true;
            vm.DownloadPoliciesAsync();
        }
    }
}