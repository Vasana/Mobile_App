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
	public partial class PolicyViewTemplate : ContentView
	{
        public PolicyViewTemplate()
        {
            InitializeComponent();

            //Creating TapGestureRecognizer
            var tapImage = new TapGestureRecognizer();
            //Binding events
            tapImage.Tapped += TapImage_Tapped;
            //Associating tap events to the image buttons
            btnCall.GestureRecognizers.Add(tapImage);
        }

        // Use like click button event
        void TapImage_Tapped(object sender, EventArgs e)
        {
            string mobileNumber = lblMobileNo.Text;
            try
            {
                if (mobileNumber != "")
                {
                    Device.OpenUri(new Uri("tel:" + mobileNumber));
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "No information found", "OK");
                }
                
            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Error occured while performing function", "OK");
            }
        }
    }
}