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

namespace Agent_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PolicySearchView
	{
		public PolicySearchView ()
		{
			InitializeComponent ();
		}

        private void btnSubmit_Clicked(object sender, EventArgs e)
        {
            int selectedIndex = typePicker.SelectedIndex;
            if (selectedIndex == 0)
            {
                SearchCriteria.Instance.PremiumsPending = true;
            }
            else if (selectedIndex == 1)
            {
                SearchCriteria.Instance.DebitOutstanding = true;
            }
            else if (selectedIndex == 2)
            {
                SearchCriteria.Instance.ClaimPending = true;
            }
            else if (selectedIndex == 3)
            {
                SearchCriteria.Instance.Flagged = true;
            }
            else if (selectedIndex == 4)
            {
                SearchCriteria.Instance.BadClaims = true;
            }
            else if (selectedIndex == 5)
            {
                SearchCriteria.Instance.AllPolicies = true;
            }

            SearchCriteria.Instance.NewSearch = true;

            //MessagingCenter.Send<MainPage, string>(, "SearchPolicy", "John");
            PopupNavigation.Instance.PopAsync(true);
        }

        private void typePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = typePicker.SelectedIndex;
            if (selectedIndex == 4)
            {
                DisplayAlert("Search Alert", "Choosing All Policies may take a long time to process depending on the number of policies.", "OK");
            }
        }
    }
}