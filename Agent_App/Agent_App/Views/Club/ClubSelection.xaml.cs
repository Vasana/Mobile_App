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
    }
}