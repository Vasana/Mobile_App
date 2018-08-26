using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Agent_App.ViewModels;
using Agent_App.Models;

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

        //private async void BtnLoad_Clicked(object sender, EventArgs e)
        //{
        //    indicator.IsRunning = true;
        //    var vm = BindingContext as PerformanceStats;
        //    await vm.FilterPerformanceDate();
        //    // IsBusy = false;
        //    indicator.IsRunning = false;
        //}
    }

}