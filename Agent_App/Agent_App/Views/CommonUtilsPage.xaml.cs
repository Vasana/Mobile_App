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
            Navigation.PushAsync(new PortfolioLanding());
        }

        private void btnSettings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePwd());
        }
    }
}