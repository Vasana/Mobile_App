using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agent_App.Views;
using Agent_App.Helpers;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Agent_App
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            Settings.Username = "chandranathr@slic.lk";
            Settings.Password = "Slic@123";

            //setMainPage(); 
			MainPage = new NavigationPage(new LoginPage());
		}

        private void setMainPage()
        {
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                MainPage = new NavigationPage(new LandingPage());
            }
            else if (!string.IsNullOrEmpty(Settings.Username) && !string.IsNullOrEmpty(Settings.Password))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new RegisterPage());
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
