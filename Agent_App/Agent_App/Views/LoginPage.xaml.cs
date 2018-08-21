using Agent_App.Helpers;
using Agent_App.Interfaces;
using Agent_App.Models;
using Agent_App.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agent_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{            
			InitializeComponent ();
            Title = "Login";
                  
           
            //ProfileImage.BackgroundColor = Color.AntiqueWhite;
            LoadProfilePic();
           
        }  
        
        public void LoadProfilePic()
        {
            if (Settings.ProfileImageSet)
            {
                string filePath = DependencyService.Get<IPhoto>().GetPhotoPath();
                ProfileImage.Source = ImageSource.FromFile(filePath);
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                string buildNum = DependencyService.Get<IScreen>().Version;

                var vm = BindingContext as LoginViewModel;
                AppVersions release = await vm.CheckVersion(buildNum);

                string message = "";

                if (release.BuildNo != int.Parse(buildNum))
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (release.IsMajorUpdate == "Y")
                        {
                            message = "Current version is no longer supported. Please get the latest version (" + release.VersionNo + "). Do you want to install latest version now?.";
                            var answer = await DisplayAlert("Alert", message, "Yes", "No");
                            if (answer)
                            {
                                Device.OpenUri(new System.Uri("http://www.srilankainsurance.lk/apk/english.pdf"));
                            }
                            var closer = DependencyService.Get<ICloseApplication>();
                            if (closer != null)
                                closer.CloseApp();
                        }
                        else if (release.IsMajorUpdate == "N")
                        {
                            message = "A new update is available. Please get the latest version (" + release.VersionNo + ") for improved functionality. Do you want to install latest version now?";
                            var answer = await DisplayAlert("Alert", message, "Yes", "No");
                            if (answer)
                            {
                                Device.OpenUri(new System.Uri("http://www.srilankainsurance.lk/apk/english.pdf"));
                                var closer = DependencyService.Get<ICloseApplication>();
                                if (closer != null)
                                    closer.CloseApp();
                            }

                        }

                    });
                }                
            }
            else
            {
                //DisplayAlert("Error", "Application does not support mobile phones.", "OK");
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Alert", "Application does not support mobile phones.", "Ok");
                    var closer = DependencyService.Get<ICloseApplication>();
                    if (closer != null)
                        closer.CloseApp();
                });
            }

        }

    }
}