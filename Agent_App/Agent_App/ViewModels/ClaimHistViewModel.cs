using System;
using System.Collections.Generic;
using System.Text;
using Agent_App.Models;
using Agent_App.Services;
using Agent_App.Helpers;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Agent_App.ViewModels
{
    class ClaimHistViewModel: INotifyPropertyChanged
    {
        public List<ClaimHistory> ClaimHistList
        {
            get { return _claims; }
            set
            {
                _claims = value;
                OnPropertyChanged();
            }
        }

        public List<ClaimHistory> _claims;

        ApiServices _apiServices = new ApiServices();

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

        public string VehiNum
        {
            get => _vehiNum;
            set
            {
                _vehiNum = value;
                OnPropertyChanged();
            }
        }
        private string _vehiNum;

        public ClaimHistViewModel(string polNumber, string vehiNumber)
        {
            getClaimHistoryAsync(polNumber);
            if (vehiNumber != null)
            {
                VehiNum = vehiNumber;
            }
            else
            {
                VehiNum = polNumber;
            }
        }

        public async Task getClaimHistoryAsync(string policyNum)
        {
            IsBusy = true;
            ClaimHistList = await _apiServices.GetClaimHistoryAsync(Settings.AccessToken, policyNum);                  
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
