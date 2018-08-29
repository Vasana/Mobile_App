using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agent_App.Views.Products
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Product_Landing : TabbedPage
    {
        public Product_Landing ()
        {
            InitializeComponent();
            BarBackgroundColor = Color.FromHex("#00adbb");
            Title = "Company & Product Details";
        }
    }
}