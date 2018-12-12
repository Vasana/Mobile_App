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
        ApiServicesLife _apiServices = new ApiServicesLife();
        public LifePolicy LifPolicy
        {
            get => _lifePolicy;
            set
            {
                _lifePolicy = value;
                OnPropertyChanged();
            }
        }

        private LifePolicy _lifePolicy;

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

        public int AddressWidth
        {
            get => _addressWidth;
            set
            {
                _addressWidth = value;
                OnPropertyChanged();
            }
        }

        public int _addressWidth;

        private List<LifeMember> membersList;

        public List<LifeMember> MembersList
        {
            get { return membersList; }
            set
            {
                membersList = value;
                OnPropertyChanged(nameof(MembersList));
            }
        }

        public LifePolViewModel(CustPolicyLife policy)
        {
            GetPolicyDetailsAsync(policy.PolicyNumber);
        }

        public async Task GetPolicyDetailsAsync(string policyNumber)
        {
            IsBusy = true;
            LifPolicy = await _apiServices.GetLifePolicyAsync(accessToken: Settings.AccessToken, policyNumber: policyNumber);

            if (LifPolicy.Address != null)
            {
                AddressWidth = LifPolicy.Address.Count * 30;
            }
            else
            {
                AddressWidth = 0;
            }

            MembersList = await _apiServices.GetMemberDetailsAsync(accessToken: Settings.AccessToken, polNumber: policyNumber);

            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
