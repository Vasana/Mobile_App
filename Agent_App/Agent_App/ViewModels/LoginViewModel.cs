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
       
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async() =>
               {
                   IsBusy = true;
                   var accessToken =  await _apiServices.LoginAsync(Username, Password);

                   if (accessToken == null)
                   {
                       IsBusy = false;
                       LoginSuccess = false;
                       Message = "Login failed";                       
                   }
                   else
                   {                   
                       Settings.AccessToken = accessToken;
                       IsBusy = false;
                       Message = "Logged in Successfully";
                       LoginSuccess = true;
                       //Application.Current.MainPage = new NavigationPage(new ExampleList());
                       await Application.Current.MainPage.Navigation.PushAsync(new LandingPage());

                   }
               });
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
    }
}
