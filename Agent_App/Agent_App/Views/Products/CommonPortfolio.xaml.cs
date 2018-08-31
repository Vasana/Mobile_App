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
	public partial class CommonPortfolio : ContentPage
	{
		public CommonPortfolio ()
		{
			InitializeComponent ();
		}
        private void listofCommon_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as ProductViewModel;
            var product = e.Item as Agent_App.Models.Products;
            vm.ShowDetails(product);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            var vm = BindingContext as ProductViewModel;

            try
            {
                Button button = (Button)sender;
                Grid grid = (Grid)button.Parent;
                StackLayout listViewItem = (StackLayout)grid.Parent;
                Label lbl_product = (Label)grid.Children[3];
                Models.Products pro = vm.productList.FirstOrDefault(X => X.productID == lbl_product.Text);
                if (pro != null)
                {
                    if (!String.IsNullOrEmpty(pro.documentUrl))
                    {
                        String url = pro.documentUrl;
                        Device.OpenUri(new Uri(url));
                    }
                }
            }
            catch (Exception ex)
            {
                string i = ex.ToString();
            }

        }
    }
}