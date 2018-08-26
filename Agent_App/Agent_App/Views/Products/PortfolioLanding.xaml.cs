using Agent_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agent_App.Views.Products
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PortfolioLanding : ContentPage
	{
		public PortfolioLanding ()
		{
			InitializeComponent ();
		}

        private void listofCommon_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as ProductViewModel;
            var product = e.Item as Agent_App.Models.Products;
            vm.ShowDetails(product);
        }
    }
}