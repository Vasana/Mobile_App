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

namespace Agent_App.ViewModels
{
    class LifeMenuViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();
        public CustPolicy _previousPolicy;

        public ObservableCollection<CustPolicy> PoliciesCollection
        {
            get { return _policies; }
            set
            {
                _policies = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<CustPolicy> _policies;

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

        public LifeMenuViewModel()
        {
            DownloadPoliciesAsync();
        }

        public async Task DownloadPoliciesAsync()
        {

            _previousPolicy = null;
            IsBusy = true;
            PoliciesCollection = await _apiServices.GetTodaysLifeRemindsAsync(accessToken: Settings.AccessToken);
            IsBusy = false;

            //PoliciesCollection = new InfiniteScrollCollection<CustPolicy>(items);

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

                    await Application.Current.MainPage.Navigation.PushAsync(new performance_stats());
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

                    //await Application.Current.MainPage.Navigation.PushAsync(new Org_Perform_landing());
                    
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
