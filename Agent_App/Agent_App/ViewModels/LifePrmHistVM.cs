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
    public class LifePrmHistVM : INotifyPropertyChanged
    {

        public List<LifePrmHistory> PremiumHistList
        {
            get { return _premiums; }
            set
            {
                _premiums = value;
                OnPropertyChanged();
            }
        }

        public List<LifePrmHistory> _premiums;

        ApiServicesLife _apiServices = new ApiServicesLife();

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

        public LifePrmHistVM(string polNumber)
        {
            getPremiumHistoryAsync(polNumber);
        }

        public async Task getPremiumHistoryAsync(string policyNum)
        {
            IsBusy = true;
            PremiumHistList = await _apiServices.GetPremiumHistory(Settings.AccessToken, policyNum);
            IsBusy = false;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
