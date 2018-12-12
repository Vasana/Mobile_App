using Agent_App.Helpers;
using Agent_App.Models;
using Agent_App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;

namespace Agent_App.ViewModels
{
    public class ProductsLifeViewModel : INotifyPropertyChanged
    {
        private ApiServices _apiServices = new ApiServices();
        public ObservableCollection<Products> _LifeProductList;
        public ObservableCollection<Products> LifeproductList
        {
            get { return _LifeProductList; }
            set
            {
                _LifeProductList = value;
                OnPropertyChanged();
            }
        }
        public int _GeneralHeight;
        public int GeneralHeight
        {
            get { return _GeneralHeight; }
            set
            {
                _GeneralHeight = value;
                OnPropertyChanged();
            }
        }
        public Products _oldLifeProduct;

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

        public ProductsLifeViewModel()
        {
            DataLoad();
           // DownloadPoliciesAsync();
        }
        


        private async Task DataLoad()
        {

            var genlist = await getGenerealProductListAsync();
            foreach (Products item in genlist)
            {
                item.shortDesc = item.shortDesc.Replace("\\n", "\n");
            }
            GeneralHeight = (genlist.Count * 180) + 40;
            LifeproductList = new ObservableCollection<Products>(genlist);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ShowDetailsGen(Products product)
        {

            if (_oldLifeProduct == product)
            {
                product.IsSelected = !product.IsSelected;
                UpdateProductsGen(product);
            }
            else
            {
                if (_oldLifeProduct != null)
                {
                    _oldLifeProduct.IsSelected = false;
                    UpdateProductsGen(_oldLifeProduct);
                }
                product.IsSelected = true;
                UpdateProductsGen(product);
            }
            _oldLifeProduct = product;
        }

        private void UpdateProductsGen(Products product)
        {
            var index = LifeproductList.IndexOf(product);
            LifeproductList.Remove(product);
            LifeproductList.Insert(index, product);
        }

        public async Task<IList<Products>> getGenerealProductListAsync()
        {
            IsBusy = true;
            var LifeList = await _apiServices.GetProducts("Life", Settings.AccessToken);
            IsBusy = false;
            return LifeList;
        }
    }
}
