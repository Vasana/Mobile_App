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

            if (Device.Idiom == TargetIdiom.Phone)
            {
                DisplayAlert("Alert", "Application does not support mobile phones.", "Ok");
                var closer = DependencyService.Get<ICloseApplication>();
                if (closer != null)
                    closer.CloseApp();
            }
            //NavigationPage.SetHasBackButton(this, false); we are removing the bar from log in page, below line
            NavigationPage.SetHasNavigationBar(this, false);
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
                            Device.OpenUri(new System.Uri("http://www.srilankainsurance.lk/apk/AgentApp.apk"));
                            var closer = DependencyService.Get<ICloseApplication>();
                            if (closer != null)
                                closer.CloseApp();
                        }
                        else
                        {
                            BtnLogin.IsEnabled = true;
                            BtnLogin.Text = "Login";
                        }

                    }

                });
            }    
            else
            {
                BtnLogin.IsEnabled = true;
                BtnLogin.Text = "Login";
            }

        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            if (entUsername.Text != "" || entPassword.Text != "")
            {
                var vm = BindingContext as LoginViewModel;

                await vm.Login();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (vm.LoginSuccess)
                    {
                        if (Settings.Username == "")
                        {
                            var answer = await DisplayAlert("Alert", "Do you want to save your Login Credentials?", "Yes", "No");
                            if (answer)
                            {
                                if (Settings.Username == "" && Settings.Password == "")
                                {
                                    Settings.Username = entUsername.Text;
                                    Settings.Password = entPassword.Text;
                                }
                            }
                            else
                            {
                                Settings.AccessToken = null;
                                Settings.Username = null;
                                Settings.Password = null;
                            }
                        }
                    }

                });

                var nav = new NavigationPage(new LandingPage());
                nav.BarBackgroundColor = Color.FromHex("#00adbb");
                Application.Current.MainPage = nav;
            }
            else
            {
               await DisplayAlert("Alert", "Please enter both Username and Password", "OK");
            }
        }
    }
}