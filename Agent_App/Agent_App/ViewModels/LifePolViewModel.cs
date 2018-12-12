using Agent_App.Helpers;
using Agent_App.Models;
using Agent_App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Agent_App.ViewModels
{
    class LifePolViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();
        public CustPolicyLife GenPolicy
        {
            get => _lifePolicy;
            set
            {
                _lifePolicy = value;
                OnPropertyChanged();
            }
        }

        private CustPolicyLife _lifePolicy;

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

        public LifePolViewModel(CustPolicyLife policy)
        {
            GetPolicyDetailsAsync(policy.PolicyNumber);
        }

        public async Task GetPolicyDetailsAsync(string policyNumber)
        {
            IsBusy = true;
         //   _lifePolicy = await _apiServices.GetLifePoliciesAsync(accessToken: Settings.AccessToken, dept: dept, policyNumber: policyNumber);
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
