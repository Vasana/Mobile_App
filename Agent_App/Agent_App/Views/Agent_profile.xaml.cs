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
	public partial class Agent_profile : ContentPage
	{
		public Agent_profile ()
		{
			InitializeComponent ();
            Title = "Profile";
            //if (Device.Idiom == TargetIdiom.Phone)
            //{
            //    //int[] marks = new int[] { 12,12,2,45 };
            //    //proFrame.
            //}
        }
       
       /* private async Task btnPickPhoto_Clicked(object sender, EventArgs e)
        {
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();
            if (stream != null)
            {
                try
                {
                    string filePath = DependencyService.Get<IPhoto>().GetPhotoPath();
                    byte[] imageData = ReadFully(stream);
                
                    System.IO.File.WriteAllBytes(filePath, imageData);
                    ProfileImage.Source = ImageSource.FromFile(filePath);
                    Settings.ProfileImageSet = true;
                    lblSuccessMsg.Text = "Successfully Uploaded.";
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                    lblSuccessMsg.Text = "Upload Failed.";
                }

                //ProfileImage.Source = ImageSource.FromStream(() => stream);
                
                
            }
        }*/

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

       async void btnPickPhoto_Clicked(object sender, System.EventArgs e)
        {
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();
            if (stream != null)
            {
                try
                {
                    string filePath = DependencyService.Get<IPhoto>().GetPhotoPath();
                    byte[] imageData = ReadFully(stream);

                    System.IO.File.WriteAllBytes(filePath, imageData);
                    ProfileImage.Source = ImageSource.FromFile(filePath);
                    Settings.ProfileImageSet = true;
                    lblSuccessMsg.Text = "Successfully Uploaded.";
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                    lblSuccessMsg.Text = "Upload Failed.";
                }

                //ProfileImage.Source = ImageSource.FromStream(() => stream);


            }
        }
    }
}