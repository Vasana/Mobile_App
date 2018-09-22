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
		public GeneralMenuPage ()
		{
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.Idiom == TargetIdiom.Phone)
            {
                MessagingCenter.Send(this, "preventPortrait");
            }
        
        }
        
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
    }
}