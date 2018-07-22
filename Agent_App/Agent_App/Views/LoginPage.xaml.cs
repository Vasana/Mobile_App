﻿using Agent_App.Interfaces;
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
            string filePath =  DependencyService.Get<IPhoto>().GetPhotoPath();
            ProfileImage.Source = ImageSource.FromFile(filePath);
        }

       
    }
}