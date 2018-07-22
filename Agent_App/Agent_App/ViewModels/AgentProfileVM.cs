using Agent_App.Helpers;
using Agent_App.Interfaces;
using Agent_App.Models;
using Agent_App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Agent_App.ViewModels
{
    public class AgentProfileVM : INotifyPropertyChanged
    {
        private bool _isBusy;
        private string _imagePath;
        ApiServices _apiServices = new ApiServices();
        public AgentProfile AgentProf
        {
            get => _agentProf;
            set
            {
                _agentProf = value;
                OnPropertyChanged();
            }
        }

        private AgentProfile _agentProf;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GetAgentProfileAsync()
        {
            IsBusy = true;
            AgentProf = await _apiServices.GetAgentProfile(accessToken: Settings.AccessToken);
            AgentProf.Is_org = (AgentProf.Role == "Organizer");
            IsBusy = false;
        }

        public AgentProfileVM()
        {
            GetAgentProfileAsync();
            if (Settings.ProfileImageSet)
            {
                string filePath = DependencyService.Get<IPhoto>().GetPhotoPath();
                ImagePath = filePath;
            }
            else
            {
                ImagePath = "ProfileImage.png";
            }
        }


    }
}
