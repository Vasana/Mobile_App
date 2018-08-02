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
	public partial class performance_stats : ContentPage
	{
		public performance_stats ()
		{
			InitializeComponent ();
            PMonth.SelectedIndex = Convert.ToInt32( DateTime.Today.ToString("MM"))-1;
            PYear.SelectedIndex = 0;
        }

    }

}