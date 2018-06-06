using System;
using System.Collections.Generic;
using System.Text;
using Agent_App.Models;
using System.Windows.Input;
using Xamarin.Forms;
using Agent_App.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Agent_App.Helpers;

namespace Agent_App.ViewModels
{
    public class IdeasViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();
        //public string AccessToken { get; set; }
       
        public List<Idea> Ideas
        {
            get { return _ideas; }
            set
            {
                _ideas = value;
                OnPropertyChanged();
            }           
        }       

        
        public List<Idea> _ideas;

        public ICommand GetIdeasCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var accessToken = Settings.AccessToken;
                    Ideas = await _apiServices.GetIdeasAsync(accessToken);                      
                });
            }
        }        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
