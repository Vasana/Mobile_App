using Agent_App.Models;
using Agent_App.ViewModels;
using Rg.Plugins.Popup.Services;
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
	public partial class LifeMenuPage : ContentPage
	{
		public LifeMenuPage ()
		{
			InitializeComponent ();
            
		}
        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as LifeMenuViewModel;
            var policy = e.Item as CustPolicy;

            vm.HideOrShowPolicy(policy);

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopUp_Perform());
        }
    }
}