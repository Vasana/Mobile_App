using Agent_App.Models;
using Agent_App.ViewModels;
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
	public partial class ExampleList : ContentPage
	{
		public ExampleList ()
		{
			InitializeComponent ();
		}

        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as CardDataViewModel;
            var card = e.Item as CardDataModel;

            vm.HideOrShowCard(card);
        }
    }
}