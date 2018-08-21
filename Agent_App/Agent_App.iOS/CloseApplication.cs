using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Agent_App.Interfaces;
using Agent_App.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace Agent_App.iOS
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApp()
        {
            Thread.CurrentThread.Abort();

            //Alternative method ---------------------------------
            //Process.GetCurrentProcess().CloseMainWindow();
            //Process.GetCurrentProcess().Close();
        }
    }
}