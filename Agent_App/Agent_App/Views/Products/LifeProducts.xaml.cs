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
	public partial class LifeProducts : ContentPage
	{
		public LifeProducts ()
		{
			InitializeComponent ();
            if (Device.Idiom == TargetIdiom.Phone)
            {
                firstRow.Height = secondRow.Height = 0;
            }
        }


        private void listofCommon_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as ProductsLifeViewModel;
            var product = e.Item as Agent_App.Models.Products;
            vm.ShowDetailsGen(product);

            var keyword = txtGenproduct.Text;
            if (!String.IsNullOrEmpty(keyword))
            {
                listofGeneral.ItemsSource = vm.LifeproductList.Where(x => (x.productName.ToLower()).Contains(keyword.ToLower()) || (x.shortDesc.ToLower()).Contains(keyword.ToLower()));
            }
        }
        private void txtGenproduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = txtGenproduct.Text;
            var vm = BindingContext as ProductsLifeViewModel;

            if (String.IsNullOrEmpty(keyword))
            {
                listofGeneral.ItemsSource = vm.LifeproductList;

            }
            else
            {
                listofGeneral.ItemsSource = vm.LifeproductList.Where(x => (x.productName.ToLower()).Contains(keyword.ToLower()) || (x.shortDesc.ToLower()).Contains(keyword.ToLower()));
            }


        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            var vm = BindingContext as ProductsLifeViewModel;

            try
            {
                Button button = (Button)sender;
                Grid grid = (Grid)button.Parent;
                StackLayout listViewItem = (StackLayout)grid.Parent;
                Label lbl_product = (Label)grid.Children[3];
                Models.Products pro = vm.LifeproductList.FirstOrDefault(X => X.productID == lbl_product.Text);
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