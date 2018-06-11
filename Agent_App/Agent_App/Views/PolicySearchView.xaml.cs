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
            if (selectedIndex == 1)
            {
                SearchCriteria.Instance.PremiumsPending = true;
            }
            else
            {
                SearchCriteria.Instance.PremiumsPending = false;
            }

            SearchCriteria.Instance.NewSearch = true;

            //MessagingCenter.Send<MainPage, string>(, "SearchPolicy", "John");
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}