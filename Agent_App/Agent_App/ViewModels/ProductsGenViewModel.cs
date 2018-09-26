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
    public class ProductsGenViewModel : INotifyPropertyChanged
    {
        private ApiServices _apiServices = new ApiServices();
        public ObservableCollection<Products> _GeneralproductList;
        public ObservableCollection<Products> GeneralproductList
        {
            get { return _GeneralproductList; }
            set
            {
                _GeneralproductList = value;
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

        public ProductsGenViewModel()
        {
            DataLoad();
           // DownloadPoliciesAsync();
        }
        /*
        public async Task DownloadPoliciesAsync()
        {
            GeneralproductList = new InfiniteScrollCollection<Products>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    //var page = GeneralproductList.Count / PageSize;

                    var items = await _apiServices.GetProducts("General", Settings.AccessToken);
                    GeneralHeight = (items.Count * 180) + 40;

                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return GeneralproductList.Count < _apiServices.policyCount;
                }
            };
            _oldGenProduct = null;
            //IsBusy2 = true;
            var items2 = await _apiServices.GetProducts("General", Settings.AccessToken);
           // IsBusy2 = false;
            GeneralproductList.AddRange(items2);
            GeneralHeight = (items2.Count * 180) + 40;
            //PoliciesCollection = new InfiniteScrollCollection<CustPolicy>(items);

        }*/



        private async Task DataLoad()
        {

            var genlist = await getGenerealProductListAsync();
            foreach (Products item in genlist)
            {
                item.shortDesc = item.shortDesc.Replace("\\n", "\n");
            }
            GeneralHeight = (genlist.Count * 180) + 40;
            GeneralproductList = new ObservableCollection<Products>(genlist);
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

        public async Task<IList<Products>> getGenerealProductListAsync()
        {
            IsBusy = true;
            var GeneralList = await _apiServices.GetProducts("General", Settings.AccessToken);
            IsBusy = false;
            return GeneralList;
        }
    }
}
