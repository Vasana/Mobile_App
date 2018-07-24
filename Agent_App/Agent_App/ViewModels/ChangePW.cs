using Agent_App.Helpers;
using Agent_App.Models;
using Agent_App.Services;
using Agent_App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Agent_App.ViewModels
{
    public class ChangePW : INotifyPropertyChanged
    {
        private ApiServices _apiServices = new ApiServices();
        private ChangePasswordBindingModel changepwd_mdl = new ChangePasswordBindingModel();


        public Color textcolor { get; set; }
        public string old_password { get; set; }
        public string new_password { get; set; }
        public string confirm_password { get; set; }
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

        public bool PwdChanged
        {
            get { return _pwdChanged; }
            set
            {
                _pwdChanged = value;
                OnPropertyChanged();
            }
        }
        public bool _pwdChanged = true;

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

        public ChangePW()
        {
            PwdChanged = false;
            textcolor = Color.Black;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand loginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                });
            }
        }

        public ICommand changePWDCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    PwdChanged = false;

                    Validations val = new Validations();

                    if (val.password_validations(old_password) && val.password_validations(new_password) && val.password_validations(confirm_password))
                    {
                        if (new_password == confirm_password)
                        {
                            if (new_password != old_password)
                            {
                                changepwd_mdl.OldPassword = old_password;
                                changepwd_mdl.NewPassword = new_password;
                                changepwd_mdl.ConfirmPassword = confirm_password;

                                string response = await _apiServices.changePassword(changepwd_mdl, Settings.AccessToken);

                                
                                if (response == "Successful")
                                {
                                    textcolor = Color.Green;
                                    IsBusy = false;
                                    PwdChanged = true;
                                    Message = "Password Successfully Updated.";
                                    
                                }
                                else
                                {
                                    textcolor = Color.Red;
                                    IsBusy = false;
                                    Message = "Failed to Change Password. Please retry.";
                                    

                                    // Application.Current.MainPage = new NavigationPage(new LandingPage());


                                }
                            }
                            else
                            {
                                textcolor = Color.Red;
                                IsBusy = false;
                                Message = "New password cannot be the current password.";
                                textcolor = Color.Red;
                            }
                        }
                        else
                        {
                            textcolor = Color.Red;
                            IsBusy = false;
                            Message = "New Password didn't match with confirmation";

                        }
                    }
                    else
                    {
                        textcolor = Color.Red;
                        IsBusy = false;
                        Message = "Please Enter Correct Values";
                    }
                });
            }

        }

    }
}
