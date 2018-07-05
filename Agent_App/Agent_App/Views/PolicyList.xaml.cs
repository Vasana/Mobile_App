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
	public partial class PolicyList : ContentPage
	{
        public PolicyList()
        {
            InitializeComponent();
            Title = "Policy List";

            NavigationPage.SetHasBackButton(this, true);

            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#1464F4");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.LightGray;
            
        }

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as PoliciesViewModel;
            var policy = e.Item as CustPolicy;

            vm.HideOrShowPolicy(policy);
        }
        
        private void btnSearch_Clicked(object sender1, EventArgs e)
        {
            //MessagingCenter.Subscribe<MainPage, SearchCriteria>(this, "SearchPolicy", (sender, arg) => {
            //    var vm = BindingContext as PoliciesViewModel;
            //    vm.SearchPolicies(arg);
            //});

            PolicySearchView searchView = new PolicySearchView();
            searchView.Disappearing += SearchView_Disappearing;
            PopupNavigation.Instance.PushAsync(searchView);
        }

        private void btnPayPend_Clicked(object sender1, EventArgs e)
        {
            SearchCriteria.Instance.NewSearch = true;
            SearchCriteria.Instance.PremiumsPending = true;
            var vm = BindingContext as PoliciesViewModel;
            vm.DownloadPoliciesAsync();
        }

        private void btnClaimPend_Clicked(object sender1, EventArgs e)
        {
            SearchCriteria.Instance.NewSearch = true;
            SearchCriteria.Instance.ClaimPending = true;
            var vm = BindingContext as PoliciesViewModel;
            vm.DownloadPoliciesAsync();
        }

        private void btnFlagged_Clicked(object sender1, EventArgs e)
        {
            SearchCriteria.Instance.NewSearch = true;
            SearchCriteria.Instance.Flagged = true;
            var vm = BindingContext as PoliciesViewModel;
            vm.DownloadPoliciesAsync();
        }

        private void SearchView_Disappearing(object sender, EventArgs e)
        {
            var vm = BindingContext as PoliciesViewModel;
            vm.DownloadPoliciesAsync();
            ((PolicySearchView)sender).Disappearing -= SearchView_Disappearing;
        }
                
        private void btnDbitOuts_Clicked(object sender, EventArgs e)
        {
            SearchCriteria.Instance.NewSearch = true;
            SearchCriteria.Instance.BusinessType = "M";
            SearchCriteria.Instance.DebitOutstanding = true;
            var vm = BindingContext as PoliciesViewModel;
            vm.DownloadPoliciesAsync();
        }

        private void btnBadClaims_Clicked(object sender, EventArgs e)
        {
            SearchCriteria.Instance.NewSearch = true;
            SearchCriteria.Instance.BadClaims = true;
            var vm = BindingContext as PoliciesViewModel;
            vm.DownloadPoliciesAsync();
        }

        private void btnMotor_Clicked(object sender, EventArgs e)
        {
            SearchCriteria.Instance.NewSearch = true;
            SearchCriteria.Instance.AllPolicies = true;
            SearchCriteria.Instance.BusinessType = "M";
            var vm = BindingContext as PoliciesViewModel;
            vm.DownloadPoliciesAsync();
        }

        private void btnNonMotor_Clicked(object sender, EventArgs e)
        {
            SearchCriteria.Instance.NewSearch = true;
            SearchCriteria.Instance.AllPolicies = true; //Policies in any policy status
            SearchCriteria.Instance.BusinessType = "G";
            var vm = BindingContext as PoliciesViewModel;
            vm.DownloadPoliciesAsync();
        }
    }
}