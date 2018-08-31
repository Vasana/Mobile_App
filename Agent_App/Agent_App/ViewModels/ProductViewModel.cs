using Agent_App.Helpers;
using Agent_App.Models;
using Agent_App.Services;
using Agent_App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Agent_App.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
       // public ICommand onProductSelect { get; private set; }
        
        private ApiServices _apiServices = new ApiServices();
        public ObservableCollection<Products> _productList;
        public ObservableCollection <Products> productList
        {
            get { return _productList; }
            set
            {
                _productList = value;
                OnPropertyChanged();
            }
        }

        
        public int _commonHeight;
        public int commonHeight
        {
            get { return _commonHeight; }
            set
            {
                _commonHeight = value;
                OnPropertyChanged();
            }
        }
       

        public Products _oldProduct;
        


        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        private bool _isBusy;

        public ProductViewModel()
        {
          //  onProductSelect = new Command<Products>(openUrl);
            DataLoad();
            
        }

        void openUrl(Products value)
        {
            Products i = value;
        }

        private async Task DataLoad()
        {
            var clist = await getCommonProductListAsync();
            foreach (Products item in clist)
            {
                item.shortDesc = item.shortDesc.Replace("\\n", "\n");
            }
            commonHeight = (clist.Count * 180) + 40;
            productList = new ObservableCollection<Products>(clist);
            
           
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand onProductSelect
        {
            get
            {
                return new Command(async () =>
                {
                    //Application.Current.MainPage = new NavigationPage(new ExampleList());

                    await Application.Current.MainPage.Navigation.PushAsync(new PolicyList());
                });
            }
        }




        public void ShowDetails(Products product)
        {

            if (_oldProduct == product)
            {
                product.IsSelected = !product.IsSelected;
                UpdateProducts(product);
            }
            else
            {
                if (_oldProduct != null)
                {
                    _oldProduct.IsSelected = false;
                    UpdateProducts(_oldProduct);
                }
                product.IsSelected = true;
                UpdateProducts(product);
            }
            _oldProduct = product;
        }
        private void UpdateProducts(Products product)
        {
            var index = productList.IndexOf(product);
            productList.Remove(product);
            productList.Insert(index, product);
        }
        
        public async Task<IList<Products>> getCommonProductListAsync()
        {
            IsBusy = true;
            var commonList = await _apiServices.GetProducts("Common", Settings.AccessToken);
            IsBusy = false;
            return commonList;
        }
        
    }
}
