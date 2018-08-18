using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agent_App.Droid;
using Agent_App.Interfaces;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace Agent_App.Droid
{
    public class CloseApplication : ICloseApplication
    {
        public void CloseApp()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}