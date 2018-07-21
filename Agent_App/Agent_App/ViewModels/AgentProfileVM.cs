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
    public class AgentProfileVM : INotifyPropertyChanged
    {
        private bool _isBusy;
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

        }


    }
}
