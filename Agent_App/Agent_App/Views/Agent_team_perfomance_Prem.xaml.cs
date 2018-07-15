using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agent_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Agent_team_perfomance_Prem : ContentPage
	{
		public Agent_team_perfomance_Prem ()
		{
			InitializeComponent ();
		}

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}