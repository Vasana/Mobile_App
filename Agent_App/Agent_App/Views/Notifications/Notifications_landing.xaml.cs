using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agent_App.Views.Notifications
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Notifications_landing : TabbedPage
    {
		public Notifications_landing ()
		{
			InitializeComponent ();
            BarBackgroundColor = Color.FromHex("#00adbb");
            Title = "Notifications";
        }
	}
}