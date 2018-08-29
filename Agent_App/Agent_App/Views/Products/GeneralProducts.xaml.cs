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
        public PortfolioLanding()
        {
            InitializeComponent();
        }

        private void listofCommon_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as ProductsGenViewModel;
            var product = e.Item as Agent_App.Models.Products;
            vm.ShowDetailsGen(product);
        }
        private void txtGenproduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = txtGenproduct.Text;
            var vm = BindingContext as ProductsGenViewModel;

            if (String.IsNullOrEmpty(keyword))
            {
                listofGeneral.ItemsSource = vm.GeneralproductList;
                
            }
            else
            {
                listofGeneral.ItemsSource = vm.GeneralproductList.Where(x => (x.productName.ToLower()).Contains(keyword.ToLower()) || (x.shortDesc.ToLower()).Contains(keyword.ToLower()));
            }
            

        }

        //private void listofGeneral_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var vm = BindingContext as ProductsGenViewModel;
        //    var product = e.Item as Agent_App.Models.Products;
        //    vm.ShowDetailsGen(product);
        //}


        protected override async void OnAppearing()
        {
            //base.OnAppearing();
            //var vm = BindingContext as ProductViewModel;
          //await vm.getProductListAsync();

            //listofCommon.ItemsSource = vm.productList;

        }

    }
}