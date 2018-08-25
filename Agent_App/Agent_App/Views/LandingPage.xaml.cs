using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace Agent_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LandingPage : Xamarin.Forms.TabbedPage
    {
		public LandingPage ()
		{
            InitializeComponent();
            BarBackgroundColor = Color.FromHex("#00adbb");
            
            //On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            //On<Android>().SetBarSelectedItemColor(Color.YellowGreen); //--> To change the selected color tabitem
            //On<Android>().SetBarItemColor(Color.Gray); //--->Default one but you can also change this 

        }

       
    }
}