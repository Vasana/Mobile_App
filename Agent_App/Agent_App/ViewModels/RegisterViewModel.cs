using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Agent_App.Services;
using Agent_App.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Agent_App.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
                
        public bool RegSuccess
        {
            get { return _regSuccess; }
            set
            {
                _regSuccess = value;
                OnPropertyChanged();
            }
        }
        public bool _regSuccess;

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

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    var isSuccess = await _apiServices.RegisterAsync(Email, Password, ConfirmPassword);

                    Settings.Username = Email;
                    Settings.Password = Password;
                    Settings.AccessToken = "";


                    if (isSuccess)
                    {
                        IsBusy = false;
                        RegSuccess = true;
                        Message = "Registered Successfully";
                    }
                    else
                    {
                        IsBusy = false;
                        RegSuccess = false;
                        Message = "Retry Later";
                    }


                });
            }
        }

        /* public ICommand RegisterCommand
         {
             get
             {
                 return new Command( () =>
                 {
                     //var isSuccess = await _apiServices.RegisterAsync(Email, Password, ConfirmPassword);
                     var isSuccess = true;
                     if (isSuccess)
                     {
                         Email = "suscces";
                         Message = "Registered Successfully";  

                     }
                     else
                     {
                         Email = "fail";
                         Message = "Retry Later";
                     }


                 });
             }
         }*/

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
