using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agent_App.Interfaces;
using Agent_App.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Screen_iOS))]
namespace Agent_App.iOS
{
    public class Screen_iOS : IScreen
    {
        public string Version
        {
            get
            {
                NSObject ver = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"];
                return ver.ToString();
            }
        }
    }
}