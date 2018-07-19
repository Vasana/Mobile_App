using Agent_App.Helpers;
using Agent_App.Models;
using Agent_App.Services;
using Agent_App.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace Agent_App.ViewModels
{
    class GeneralMenuViewModel: INotifyPropertyChanged
    {
        private const int PageSize = 10;
        ApiServices _apiServices = new ApiServices();
        public CustPolicy _previousPolicy;        

        public InfiniteScrollCollection<CustPolicy> PoliciesCollection
        {
            get { return _policies; }
            set
            {
                _policies = value;
                OnPropertyChanged();
            }
        }
        public InfiniteScrollCollection<CustPolicy> _policies;

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

        public string NotifExist
        {
            get => _notifExist;
            set
            {
                _notifExist = value;
                OnPropertyChanged();
            }
        }
        private string _notifExist;

        public GeneralMenuViewModel()
        {
            SearchCriteria.Instance.NewSearch = true;
            SearchCriteria.Instance.TodayReminders = true;
            DownloadPoliciesAsync();
            GetNotifExistAsync();
        }

        public async Task DownloadPoliciesAsync()
        {
            PoliciesCollection = new InfiniteScrollCollection<CustPolicy>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;

                    // load the next page
                    var page = PoliciesCollection.Count / PageSize;

                    var items = await _apiServices.GetPoliciesAsync(Settings.AccessToken, page, PageSize);

                    IsBusy = false;

                    // return the items that need to be added
                    return items;
                },
                OnCanLoadMore = () =>
                {
                    return PoliciesCollection.Count < _apiServices.policyCount;
                }
            };
            _previousPolicy = null;
            IsBusy = true;
            var items2 = await _apiServices.GetPoliciesAsync(accessToken: Settings.AccessToken, pageIndex: 0, pageSize: PageSize);
            IsBusy = false;
            PoliciesCollection.AddRange(items2);

            //PoliciesCollection = new InfiniteScrollCollection<CustPolicy>(items);
        }

        public async Task GetNotifExistAsync()
        {
            bool ret = await _apiServices.NotificsExistAsync(Settings.AccessToken);
            if (ret)
            {
                NotifExist = "notifAlert.png";
            }
            else
            {
                NotifExist = "";
            }
        }

        public void HideOrShowPolicy(CustPolicy policy)
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

        private void UpdatePolicies(CustPolicy policy)
        {
            var index = PoliciesCollection.IndexOf(policy);
            PoliciesCollection.Remove(policy);
            PoliciesCollection.Insert(index, policy);
        }

        public ICommand CustomersProfilesCommand
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

        public ICommand GetQuotationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //Application.Current.MainPage = new NavigationPage(new ExampleList());

                    if (Settings.jobRole == "Organizer")
                        await PopupNavigation.Instance.PushAsync(new PopUp_Perform());
                    else
                        await Application.Current.MainPage.Navigation.PushAsync(new Agent_performance());
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
