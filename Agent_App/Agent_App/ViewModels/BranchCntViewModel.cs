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
    class BranchCntViewModel: INotifyPropertyChanged
    {
        public List<BranchContact> BranchesList
        {
            get { return _branches; }
            set
            {
                _branches = value;
                OnPropertyChanged();
            }
        }

        public List<BranchContact> _branches;

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

        public BranchCntViewModel()
        {
            getBranchesListAsync();
        }

        public async Task getBranchesListAsync()
        {
            IsBusy = true;
            BranchesList = await _apiServices.GetBranchContactsAsync(Settings.AccessToken);
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
