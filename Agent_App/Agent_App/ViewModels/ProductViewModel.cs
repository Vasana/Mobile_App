using Agent_App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Agent_App.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public ObservableCollection <Products> productList { get; set; }
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
            productList = GetProductList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                    imageName = "logo.png",
                    imageUrl = "logo.png",
                    stream = "Common",
                    IsSelected = false

                },
                 new Products()
                {
                    productID = "C002",
                    productName = "Company Profile",
                    shortDesc = "Company Profile",
                    longDesc = "longDesc",
                    imageName = "logo.png",
                    imageUrl = "logo.png",
                    stream = "Common",
                    IsSelected = false

                }
            };

            return prodcuts;
        }
    }
}
