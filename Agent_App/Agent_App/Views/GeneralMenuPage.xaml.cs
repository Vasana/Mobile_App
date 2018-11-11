using Agent_App.Models;
using Agent_App.ViewModels;
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
	public partial class GeneralMenuPage : ContentPage
	{
        public GeneralMenuPage()
        {
            InitializeComponent();
            if (Device.Idiom == TargetIdiom.Phone)
            {
                HeadingRow1.Height = 0;
                ourGrid.RowSpacing = 10;
                veryFirst.Height = firstRow.Height = 50;
                breaker.Height = 0;

                BtnCusprof.Text = "Policy Details";
                BtnQuote.Text = "Sales Performance";

                BtnCusprof.Image = BtnQuote.Image = null;
                BtnCusprof.HeightRequest = BtnQuote.HeightRequest = 50;
                BtnCusprof.WidthRequest = BtnQuote.WidthRequest = 200;
                BtnCusprof.BackgroundColor = BtnQuote.BackgroundColor = Color.SkyBlue;
                BtnCusprof.BorderColor = BtnQuote.BorderColor = Color.FromHex("#C89400");
                BtnCusprof.CornerRadius = BtnQuote.CornerRadius = 10;
               
            }
        }
            

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    if (Device.Idiom == TargetIdiom.Phone)
        //    {
        //        MessagingCenter.Send(this, "preventPortrait");
        //    }
        
        //}
        
        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as GeneralMenuViewModel;
            var policy = e.Item as CustPolicy;             
            
            vm.HideOrShowPolicy(policy);          
            
        }

        private void btnNotif_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotificationsList());
        }

        private void btnRefresh_Clicked(object sender, EventArgs e)
        {
            var vm = new GeneralMenuViewModel();
            this.BindingContext = vm;
        }
    }
}