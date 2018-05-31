using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agent_App.Models;
using Agent_App.ViewModels;

namespace Agent_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PolicyList : ContentPage
	{
		public PolicyList ()
		{
			InitializeComponent ();
		}

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as PoliciesViewModel;
            var policy = e.Item as Cust_Policy;

            vm.HideOrShowPolicy(policy);
        }
    }
}