using Agent_App.Views.Club;
using Agent_App.Views.Notifications;
using Agent_App.Views.Products;
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
	public partial class CommonUtilsPage : ContentPage
	{
		public CommonUtilsPage ()
		{
			InitializeComponent ();
            Title = "Common Menu";
            if (Device.Idiom == TargetIdiom.Phone)
            {
                HeadingRow1.Height = 0;
                firstRow.Height = 30;
                secondRow.Height = 50;
                thirdRow.Height = 30;
                fourthRow.Height = 50;
                ourGrid.RowSpacing = 10;
                //ourGrid.ColumnSpacing = 20;

                
                BtnProfile.Text = "Profile";
                BtnPromotions.Text = "Product Portfolio";
                BtnBranches.Text = "Branch Contacts";
                btnSettings.Text = "Settings";
                BtnNotifications.Text = "Notifications";

                BtnBranches.Image = btnSettings.Image = BtnPromotions.Image = BtnProfile.Image = BtnNotifications.Image = null;
                btnSettings.HeightRequest = BtnBranches.HeightRequest = BtnPromotions.HeightRequest = BtnProfile.HeightRequest = BtnNotifications.HeightRequest = 50;
                btnSettings.WidthRequest = BtnBranches.WidthRequest = BtnPromotions.WidthRequest = BtnProfile.WidthRequest = BtnNotifications.WidthRequest = 200;
                btnSettings.BackgroundColor = BtnBranches.BackgroundColor = BtnPromotions.BackgroundColor = BtnProfile.BackgroundColor = BtnNotifications.BackgroundColor = Color.SkyBlue;
                btnSettings.BorderColor = BtnBranches.BorderColor = BtnPromotions.BorderColor = BtnProfile.BorderColor = BtnNotifications.BorderColor = Color.FromHex("#C89400");
                btnSettings.BorderRadius = BtnBranches.BorderRadius = BtnPromotions.BorderRadius = BtnProfile.BorderRadius = BtnNotifications.BorderRadius = 10;
                
            }
        }

        private void BtnProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Agent_profile());
        }

        private void BtnBranches_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BranchContactsPage());
        }

        private void BtnPromotions_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Product_Landing());
        }

        private void btnSettings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePwd());
        }

        private void BtnNotifications_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Notifications_landing());
        }

        private void BtnClubInfo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ClubSelection());
        }
    }
}