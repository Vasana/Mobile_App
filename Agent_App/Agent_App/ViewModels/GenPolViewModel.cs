using Agent_App.Models;
using Agent_App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Agent_App.Helpers;

namespace Agent_App.ViewModels
{
    public class GenPolViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();
        public GeneralPolicy GenPolicy
        {
            get => _genPolicy;
            set
            {
                _genPolicy = value;
                OnPropertyChanged();
            }
        }

        private GeneralPolicy _genPolicy;

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

        public GenPolViewModel(string dept, string policyNum)
        {
            GetPolicyDetailsAsync(dept, policyNum);
        }

        public async Task GetPolicyDetailsAsync(string dept, string policyNumber)
        {
            IsBusy = true;
            GenPolicy = await _apiServices.GetGenPolicyAsync(accessToken: Settings.AccessToken, dept: dept, policyNumber: policyNumber);
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
