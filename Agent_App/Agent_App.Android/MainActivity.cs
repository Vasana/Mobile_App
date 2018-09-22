using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Agent_App.Droid;
using System.Threading.Tasks;
using System.IO;
using Android.Content;
using Xamarin.Forms;
using Agent_App.Views;

namespace Agent_App.Droid
{
    [Activity(Label = "SLICAgent", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {     

            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Instance = this;
            Rg.Plugins.Popup.Popup.Init(this, bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            //allowing the device to change the screen orientation based on the rotation
            MessagingCenter.Subscribe<GenPolicyDetails>(this, "allowLandScapePortrait", sender =>
            {

                RequestedOrientation = ScreenOrientation.Unspecified;
            });

            //allowing the device to change the screen orientation based on the rotation
            MessagingCenter.Subscribe<GeneralMenuPage>(this, "preventPortrait", sender =>
            {

                RequestedOrientation = ScreenOrientation.Landscape;
            });

            //during page close setting back to portrait
            MessagingCenter.Subscribe<GenPolicyDetails>(this, "preventLandScape", sender =>
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            });
        }       
       

        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }
    }
}

