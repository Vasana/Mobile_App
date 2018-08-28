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

namespace Agent_App.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private ApiServices _apiServices = new ApiServices();
        public ObservableCollection <Products> productList { get; set; }
        public ObservableCollection<Products> GeneralproductList { get; set; }
        public int commonHeight { get; set; }
        public int GeneralHeight { get; set; }
        public Products _oldProduct;
        public Products _oldGenProduct;


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
            getProductListAsync();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ShowDetailsGen(Products product)
        {

            if (_oldGenProduct == product)
            {
                product.IsSelected = !product.IsSelected;
                UpdateProductsGen(product);
            }
            else
            {
                if (_oldGenProduct != null)
                {
                    _oldGenProduct.IsSelected = false;
                    UpdateProductsGen(_oldGenProduct);
                }
                product.IsSelected = true;
                UpdateProductsGen(product);
            }
            _oldGenProduct = product;
        }
        private void UpdateProductsGen(Products product)
        {
            var index = GeneralproductList.IndexOf(product);
            GeneralproductList.Remove(product);
            GeneralproductList.Insert(index, product);
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

        private ObservableCollection <Products> GetProductList()
        {
            ObservableCollection<Products> prodcuts = new ObservableCollection<Products>()
            {
                new Products()
                {
                    productID = "C001",
                    productName = "Company Profile",
                    shortDesc = "Company Profile",
                    longDesc = "longDesc",
                    imageName = "customer.png",
                    imageUrl = "customer.png",
                    stream = "Common",
                    IsSelected = false

                },
                 new Products()
                {
                    productID = "C002",
                    productName = "Company Profile",
                    shortDesc = "Company\n Profile\nCompany\n Profile",
                    longDesc = "longDesc",
                    imageName = "settings.png",
                    imageUrl = "settings.png",
                    stream = "Common",
                    IsSelected = false

                }
            };

            return prodcuts;
        }

        public async Task getProductListAsync()
        {
            IsBusy = true;
            
            var commonList = await _apiServices.GetProducts("Common", Settings.AccessToken);
            productList = new ObservableCollection<Products>(commonList);

            var generalList = await _apiServices.GetProducts("General", Settings.AccessToken);
            generalList = new ObservableCollection<Products>(generalList);

            IsBusy = false;
            //return items2;

        }
    }
}
