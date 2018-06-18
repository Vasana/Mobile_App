using Agent_App.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Agent_App.ViewModels
{
    public class CommonUtilsModelView
    {
        public ICommand CustomersProfilesCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //Application.Current.MainPage = new NavigationPage(new ExampleList());

                    await Application.Current.MainPage.Navigation.PushAsync(new PolicyList());
                });
            }
        }

        public ICommand GetQuotationCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //Application.Current.MainPage = new NavigationPage(new ExampleList());

                    await Application.Current.MainPage.Navigation.PushAsync(new ExampleList());
                });
            }
        }

        public ICommand GetUserProfCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //Application.Current.MainPage = new NavigationPage(new ExampleList());

                    await Application.Current.MainPage.Navigation.PushAsync(new ExampleList());
                });
            }
        }

        public ICommand GetSettingsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //Application.Current.MainPage = new NavigationPage(new ExampleList());

                    await Application.Current.MainPage.Navigation.PushAsync(new ExampleList());
                });
            }
        }
    }
}
