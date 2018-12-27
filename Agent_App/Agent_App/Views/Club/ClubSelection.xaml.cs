using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agent_App.Views.Club
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClubSelection : ContentPage
    {
        public ClubSelection()
        {
            Title = "Club Information";
            InitializeComponent();
            

        }
        protected async override void OnAppearing()
        {

            var answer = await DisplayAlert("Alert", "Your club selection displayed in this page is only a forecast based on primitive data. They could be different from the final club entitlement which is released by \"Sales Support Division\", after processing these data further.", "Accept", "Decline");
            if (answer)
            {
            }
            else
            {
                Navigation.PushAsync(new CommonUtilsPage());
            }
        }
    }
}