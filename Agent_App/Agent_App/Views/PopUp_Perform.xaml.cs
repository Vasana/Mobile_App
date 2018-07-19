using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agent_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUp_Perform : PopupPage
    {
		public PopUp_Perform()
		{
			InitializeComponent ();
		}
        private async Task btnInd_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
            await Application.Current.MainPage.Navigation.PushAsync(new Agent_performance());
        }
        private async Task btnTeam_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
            await Application.Current.MainPage.Navigation.PushAsync(new Org_Perform_landing());
            
        }
    }
}