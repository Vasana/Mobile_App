using System;
using System.Collections.Generic;
using Agent_App.ViewModels;

using Xamarin.Forms;

namespace Agent_App.Views
{
    public partial class LifePolicyDetails : ContentPage
    {
        public LifePolicyDetails()
        {
            InitializeComponent();
            Title = "Policy Details";

            membersView.IsVisible = false;
            coversView.IsVisible = false;
            premiumsView.IsVisible = false;
        }

        private async void btnPayHist_Clicked(object sender, System.EventArgs e)
        {
            var lifPolVM = BindingContext as LifePolViewModel;

            var premiumHistVM = new LifePrmHistVM(lifPolVM.LifPolicy.PolicyNumber);
            var premiumHistPage = new LifePrmHistoryPage
            {
                BindingContext = premiumHistVM
            };

            if (App.Current.MainPage is NavigationPage)
            {
                await(App.Current.MainPage as NavigationPage).PushAsync(premiumHistPage);
            }
        }

        void btnMembers_Clicked(object sender, System.EventArgs e)
        {
            if (membersView.IsVisible)
            {
                membersView.IsVisible = false;
            }
            else
            {
                membersView.IsVisible = true;
            }
        }

        void btnCovers_Clicked(object sender, System.EventArgs e)
        {
            if (coversView.IsVisible)
            {
                coversView.IsVisible = false;
            }
            else
            {
                coversView.IsVisible = true;
            }
        }

        void btnPremiumDues_Clicked(object sender, System.EventArgs e)
        {
            if (premiumsView.IsVisible)
            {
                premiumsView.IsVisible = false;
            }
            else
            {
                premiumsView.IsVisible = true;
            }
        }
    }
}
