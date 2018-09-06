using Agent_App.Models;
using Agent_App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;
using Agent_App.Helpers;

namespace Agent_App.ViewModels
{
    public class PoliciesViewModel : INotifyPropertyChanged
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
        
        public InfiniteScrollCollection<CustPolicy> _policies;

        public object SelectedItem { get; set; }
                
        public PoliciesViewModel()
        {
            PolicyListDesc = "Latest 10";
            SearchCriteria.Instance.NewSearch = true;
            SearchCriteria.Instance.TopTen = true;
            DownloadPoliciesAsync();
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
            IsBusy2 = true;
            var items2 = await _apiServices.GetPoliciesAsync(accessToken: Settings.AccessToken, pageIndex: 0, pageSize: PageSize);
            IsBusy2 = false;
            PoliciesCollection.AddRange(items2);
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SearchPolicies()
        {     

            if (SearchCriteria.Instance.PremiumsPending)
            {
                //CustPolicy policy1 = PoliciesCollection.First(p => p.PolicyNumber == "G/010/PA/37241");
                _previousPolicy = null;  // this should be done whenever policy collection regenerated.
                
                PoliciesCollection = new InfiniteScrollCollection<CustPolicy>
                {                    
                    new CustPolicy
                    {
                        PolicyNumber = "G/010/PA/37241",
                        AgentCode="111558" ,
                        InsuredName = "H.K.K.T.DUMINDA",
                        StartDate = "22-JUN-17",
                        EndDate = "22-JUN-18",
                        Department = "G",
                        PolicyType = "PA",
                        PolTypeDesc = "Personal Accident",
                        VehicleNumber = "",
                        PolTypeImage = "life.png",
                        PolStatusImage = "alert_yellow.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                    },

                     new CustPolicy
                     {
                        PolicyNumber = "GHC170101000031",
                        AgentCode="111558" ,
                        InsuredName = "W.A.D.N PERERA",
                        StartDate = "02-JUN-18",
                        EndDate = "02-JUN-19",
                        Department = "G",
                        PolicyType = "HC",
                        PolTypeDesc = "Home Protect",
                        VehicleNumber = "",
                        PolTypeImage = "home.png",
                        PolStatusImage = "alert_yellow.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        FlagImage = "starFrame.png",
                     },

                     new CustPolicy
                     {
                        PolicyNumber = "VM1115003410000519",
                        AgentCode="111558" ,
                        InsuredName = "Mr. S.L.S.GUNARATHNA",
                        StartDate = "16-JUN-18",
                        EndDate = "15-JUN-19",
                        Department = "M",
                        PolicyType = "M11",
                        PolTypeDesc = "Motor - Comprehensive",
                        VehicleNumber = "CAH 0945",
                        PolTypeImage = "car.png",
                        PolStatusImage = "alert_yellow.png",
                        ClaimStatusImage = "tick.png",
                        MobileNumber = "0766980982",
                        MotorPolicy = true,
                        FlagImage = "starFrame.png",
                     },

                    //   AlertColor =  Color.Green : Color.Blue,    This can be added to set alert dialog inside card data model
                };
            }
            else
            {
                DownloadPoliciesAsync();
            }

        }

       
    }
}
