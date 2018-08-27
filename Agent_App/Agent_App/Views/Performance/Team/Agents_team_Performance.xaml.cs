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
	public partial class Agents_team_Performance : ContentPage
	{
		public Agents_team_Performance ()
		{
			InitializeComponent ();
		}

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}