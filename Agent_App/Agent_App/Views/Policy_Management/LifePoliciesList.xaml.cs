using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agent_App.Models;
using Agent_App.ViewModels;
using Rg.Plugins.Popup.Services;

namespace Agent_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LifePoliciesList : ContentPage
	{
		public LifePoliciesList ()
		{
			InitializeComponent ();
            Title = "Policy List";

            NavigationPage.SetHasBackButton(this, true);

            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1464F4");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.LightGray;
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as LifePoliciesVwModel;
            var policy = e.Item as CustPolicyLife;

            vm.HideOrShowPolicy(policy);
        }

        private void btnSearch_Clicked(object sender1, EventArgs e)
        {
            //MessagingCenter.Subscribe<MainPage, SearchCriteria>(this, "SearchPolicy", (sender, arg) => {
            //    var vm = BindingContext as PoliciesViewModel;
            //    vm.SearchPolicies(arg);
            //});

            LifePolSearchView searchView = new LifePolSearchView();
            searchView.Disappearing += SearchView_Disappearing;
            var vm = BindingContext as LifePoliciesVwModel;
            if (!String.IsNullOrEmpty(SearchCriteriaLife.Instance.ListDesc))
                vm.PolicyListDesc = SearchCriteriaLife.Instance.ListDesc;
            PopupNavigation.Instance.PushAsync(searchView);

        }

        private void btnInforce_Clicked(object sender1, EventArgs e)
        {
            SearchCriteriaLife.Instance.NewSearch = true;
            SearchCriteriaLife.Instance.inforce_pol = true;
            SearchCriteriaLife.Instance.AllPolicies = false;
            var vm = BindingContext as LifePoliciesVwModel;
            vm.PolicyListDesc = "Inforce Policy List";
            SearchCriteriaLife.Instance.ListDesc = "Inforce Policy List";
            vm.DownloadPoliciesAsync();
        }

        private void btnLapsed_Clicked(object sender1, EventArgs e)
        {
            SearchCriteriaLife.Instance.NewSearch = true;
            SearchCriteriaLife.Instance.lapsed_pol = true;
            SearchCriteriaLife.Instance.AllPolicies = false;
            var vm = BindingContext as LifePoliciesVwModel;
            SearchCriteriaLife.Instance.ListDesc = "Lapsed Policy List";
            vm.PolicyListDesc = "Lapsed Policy List";
            vm.DownloadPoliciesAsync();
        }

        private void btnFlagged_Clicked(object sender1, EventArgs e)
        {
            SearchCriteriaLife.Instance.NewSearch = true;
            SearchCriteriaLife.Instance.Flagged = true;
            SearchCriteriaLife.Instance.AllPolicies = false;
            var vm = BindingContext as LifePoliciesVwModel;
            SearchCriteriaLife.Instance.ListDesc = "Reminders Set Policy List";
            vm.PolicyListDesc = "Reminders Set Policy List";
            vm.DownloadPoliciesAsync();
        }

        private void SearchView_Disappearing(object sender, EventArgs e)
        {
            var vm = BindingContext as LifePoliciesVwModel;
            vm.DownloadPoliciesAsync();
            ((LifePolSearchView)sender).Disappearing -= SearchView_Disappearing;
        }

        private void btnTempLapsed_Clicked(object sender, EventArgs e)
        {
            SearchCriteriaLife.Instance.NewSearch = true;
            SearchCriteriaLife.Instance.templapse_pol =true;
            SearchCriteriaLife.Instance.AllPolicies = false;
            var vm = BindingContext as LifePoliciesVwModel;
            SearchCriteriaLife.Instance.ListDesc = "Temp. Lapsed Policy List";
            vm.PolicyListDesc = "Temp. Lapsed Policy List";
            vm.DownloadPoliciesAsync();
        }  
                
        private void listView_Refreshing(object sender, EventArgs e)
        {
            var vm = BindingContext as LifePoliciesVwModel;
            vm.PolicyListDesc = SearchCriteriaLife.Instance.ListDesc;
        }

        private void listView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var vm = BindingContext as LifePoliciesVwModel;
            vm.PolicyListDesc = SearchCriteriaLife.Instance.ListDesc;
        }
    }
}