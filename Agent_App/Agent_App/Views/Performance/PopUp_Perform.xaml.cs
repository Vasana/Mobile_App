﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agent_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUp_Perform : PopupPage
    {
		public PopUp_Perform()
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

        /*
        private async Task btnInd_Clicked(object sender, EventArgs e)
        {
            btnIndvidual.IsEnabled = false;
            btnTeam.IsEnabled = false;
            indicator.IsRunning = true;            
            await Application.Current.MainPage.Navigation.PushAsync(new Agent_performance());
            indicator.IsRunning = false;
            await PopupNavigation.Instance.PopAsync(true);
        }*/

        async void btnTeam_Clicked(object sender, EventArgs e)
        {
            btnIndvidual.IsEnabled = false;
            btnTeam.IsEnabled = false;
            indicator.IsRunning = true;
            await Application.Current.MainPage.Navigation.PushAsync(new Org_Perform_landing());
            indicator.IsRunning = false;
            await PopupNavigation.Instance.PopAsync(true);
        }

        async void btnInd_Clicked(object sender, System.EventArgs e)
        {
            btnIndvidual.IsEnabled = false;
            btnTeam.IsEnabled = false;
            indicator.IsRunning = true;
            await Application.Current.MainPage.Navigation.PushAsync(new Agent_performance());
            indicator.IsRunning = false;
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}