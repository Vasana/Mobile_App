using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Agent_App.Services;
using Agent_App.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Agent_App.Views;
using Agent_App.Models;
using System.Threading.Tasks;

namespace Agent_App.ViewModels
{
    public class LoginViewModel:INotifyPropertyChanged
    {
        private ApiServices _apiServices = new ApiServices();

        public string Username { get; set; }
        public string Password { get; set; }

        public bool LoginSuccess
        {
            get { return _loginSuccess; }
            set
            {
                _loginSuccess = value;
                OnPropertyChanged();
            }
        }
        public bool _loginSuccess;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        public bool _isBusy;

        public bool LoginEnabled
        {
            get { return _loginEnabled; }
            set
            {
                _loginEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool _loginEnabled;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public string _message;
       
        //public ICommand LoginCommand
        //{
        //    get
        //    {
        //        return new Command(async() =>
        //       {
        //           IsBusy = true;
        //           LoginEnabled = false; 
        //           var accessToken =  await _apiServices.LoginAsync(Username, Password);

        //           LoginEnabled = true;
        //           if (accessToken == null)
        //           {                       
        //               IsBusy = false;
        //               LoginSuccess = false;
        //               Message = "Login failed";                       
        //           }
        //           else
        //           {
        //               Settings.AccessToken = accessToken;
        //               //if (Settings.Username ==  "" && Settings.Password == "")
        //               //{
        //               //    Settings.Username = Username;
        //               //    Settings.Password = Password;                          
        //               //}
                       
        //               Message = "Logged in Successfully";
        //               LoginSuccess = true;
                       
        //               var nav = new NavigationPage(new LandingPage());
        //               nav.BarBackgroundColor = Color.FromHex("#00adbb");
        //               Application.Current.MainPage = nav;
        //               AgentProfile agentProfile = new AgentProfile();
        //               agentProfile = await _apiServices.GetAgentProfile(accessToken);
        //               Settings.jobRole = agentProfile.Role;
        //               Settings.agentCode = (agentProfile.Role == "Organizer" ? agentProfile.Organizer_code.ToString() : agentProfile.Agent_code.ToString());
        //               Settings.orgTeamCode = (agentProfile.Role == "Organizer" ? agentProfile.Organizer_codeTeam.ToString():"");
        //               IsBusy = false;

        //           }
        //       });
        //    }

        //}

        public async Task Login()
        {
            IsBusy = true;
            LoginEnabled = false;
            var accessToken = await _apiServices.LoginAsync(Username, Password);
                        
            if (accessToken == null)
            {
                IsBusy = false;
                LoginSuccess = false;
                Message = "Login failed";
                LoginEnabled = true;
            }
            else
            {
                Settings.AccessToken = accessToken;                

                //Message = "Logged in Successfully";
                LoginSuccess = true;
                
                AgentProfile agentProfile = new AgentProfile();
                agentProfile = await _apiServices.GetAgentProfile(accessToken);
                Settings.jobRole = agentProfile.Role;
                Settings.agentCode = (agentProfile.Role == "Organizer" ? agentProfile.Organizer_code.ToString() : agentProfile.Agent_code.ToString());
                Settings.orgTeamCode = (agentProfile.Role == "Organizer" ? agentProfile.Organizer_codeTeam.ToString() : "");

                Audit_trail au = new Audit_trail
                {
                    Action = "Log in",
                    Log_date = DateTime.Now,
                    Stream = "Common",
                    App_Id = "B Connect",
                    User_Id = Username
                };

               bool successCode = await _apiServices.WritetoAuditLog(au, accessToken);

                IsBusy = false;
            }
        }

        public LoginViewModel()
        {
            Username = Settings.Username;
            Password = Settings.Password;            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<AppVersions> CheckVersion (string build_number)
        {            
            AppVersions release = await _apiServices.GetAppVersionAsync(build_number);
            return release;
        }
    }
}
