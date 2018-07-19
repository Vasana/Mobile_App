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

        private void btnPayHist_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnClaimHist_Clicked(object sender, EventArgs e)
        {
            var genPolVM = BindingContext as GenPolViewModel;

            var claimHistVM = new ClaimHistViewModel(genPolVM.GenPolicy.PolicyNumber);
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