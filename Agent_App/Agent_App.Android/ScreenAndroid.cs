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

[assembly: Dependency(typeof(ScreenAndroid))]
namespace Agent_App.Droid
{
    class ScreenAndroid : Java.Lang.Object, IScreen
    {
        public string Version
        {
            get
            {
                var context = Forms.Context;
                return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionCode.ToString();
            }
        }
    }
}