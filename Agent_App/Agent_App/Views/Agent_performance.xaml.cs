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
    public partial class Agent_performance : TabbedPage
    {
        public Agent_performance ()
        {
            InitializeComponent();
            Title = "Sales Performance";
        }
    }
}