using Agent_App.Helpers;
using Agent_App.Interfaces;
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
                DisplayAlert("Error", "Application does not support mobile phones.", "OK");
                var closer = DependencyService.Get<ICloseApplication>();
                if (closer != null)
                    closer.CloseApp();
            }
            else
            {
                //ProfileImage.BackgroundColor = Color.AntiqueWhite;
                LoadProfilePic();
            }
        }  
        
        public void LoadProfilePic()
        {
            if (Settings.ProfileImageSet)
            {
                string filePath = DependencyService.Get<IPhoto>().GetPhotoPath();
                ProfileImage.Source = ImageSource.FromFile(filePath);
            }
        }

       
    }
}