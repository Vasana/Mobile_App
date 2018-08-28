using Agent_App.Models;
using Agent_App.ViewModels;
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
	public partial class GenPolicyDetails : ContentPage
	{
		public GenPolicyDetails ()
		{
			InitializeComponent();
            Title = "Policy Details";              
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "preventLandScape");
        }
        //during page close setting back to portrait
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send(this, "allowLandScapePortrait");
        }

        private async void btnPayHist_Clicked(object sender, EventArgs e)
        {
            var genPolVM = BindingContext as GenPolViewModel;

            var premiumHistVM = new PremiumHistVM(genPolVM.GenPolicy.Department, genPolVM.GenPolicy.PolicyNumber);
            var premiumHistPage = new PremiumHistPage
            {
                BindingContext = premiumHistVM
            };

            if (App.Current.MainPage is NavigationPage)
            {
                await(App.Current.MainPage as NavigationPage).PushAsync(premiumHistPage);
            }
        }

        private async void btnClaimHist_Clicked(object sender, EventArgs e)
        {
            var genPolVM = BindingContext as GenPolViewModel;

            var claimHistVM = new ClaimHistViewModel(genPolVM.GenPolicy.PolicyNumber, genPolVM.GenPolicy.VehicleNumber);
            var claimHistPage = new ClaimHistoryPage
            {
                BindingContext = claimHistVM
            };

            if (App.Current.MainPage is NavigationPage)
            {
                await(App.Current.MainPage as NavigationPage).PushAsync(claimHistPage);
            }
        }
    }
}