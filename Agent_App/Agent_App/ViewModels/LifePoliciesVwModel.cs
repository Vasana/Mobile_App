using Agent_App.Helpers;
using Agent_App.Models;
using Agent_App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;

namespace Agent_App.ViewModels
{
    class LifePoliciesVwModel : INotifyPropertyChanged
    {
        private const int PageSize = 10;
        ApiServicesLife _apiServices = new ApiServicesLife();
        public LifePolicy _previousPolicy;
        public InfiniteScrollCollection<LifePolicy> PoliciesCollection
        {
            get { return _policies; }
            set
            {
                _policies = value;
                OnPropertyChanged();
            }
        }

        public string PolicyListDesc
        {
            get => _PolicyListDesc;
            set
            {
                _PolicyListDesc = value;
                OnPropertyChanged();
            }
        }
        private string _PolicyListDesc;

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

        public bool IsBusy2
        {
            get { return _isBusy2; }
            set
            {
                _isBusy2 = value;
                OnPropertyChanged();
            }
        }
        public bool _isBusy2;

        public InfiniteScrollCollection<LifePolicy> _policies;

        public object SelectedItem { get; set; }

        public LifePoliciesVwModel()
        {
            PolicyListDesc = "Latest 10";
            SearchCriteriaLife.Instance.NewSearch = true;
            SearchCriteriaLife.Instance.TopTen = true;
            SearchCriteriaLife.Instance.ListDesc = "Latest 10";
            DownloadPoliciesAsync();
        }

        public async Task DownloadPoliciesAsync()
        {
            PoliciesCollection = new InfiniteScrollCollection<LifePolicy>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = PoliciesCollection.Count / PageSize;

                    var items = await _apiServices.GetLifePoliciesAsync(Settings.AccessToken, page, PageSize);
                    
                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return PoliciesCollection.Count < _apiServices.lifePolCount;
                }
            };
            _previousPolicy = null;
            IsBusy2 = true;
            var items2 = await _apiServices.GetLifePoliciesAsync(accessToken: Settings.AccessToken, pageIndex: 0, pageSize: PageSize);
            IsBusy2 = false;
            PoliciesCollection.AddRange(items2);
            //PoliciesCollection = new InfiniteScrollCollection<CustPolicy>(items);

        }

        public void HideOrShowPolicy(LifePolicy policy)
        {
            if (_previousPolicy == policy)
            {
                //clicking twice on same item will hide it
                policy.IsSelected = !policy.IsSelected;
                UpdatePolicies(policy);

            }
            else
            {
                if (_previousPolicy != null)
                {
                    //hide previous selected item
                    _previousPolicy.IsSelected = false;
                    UpdatePolicies(_previousPolicy);
                }
                //show selected item
                policy.IsSelected = true;
                UpdatePolicies(policy);
            }

            _previousPolicy = policy;
        }

        private void UpdatePolicies(LifePolicy policy)
        {
            var index = PoliciesCollection.IndexOf(policy);
            PoliciesCollection.Remove(policy);
            PoliciesCollection.Insert(index, policy);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
