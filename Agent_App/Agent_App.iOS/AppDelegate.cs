﻿using System;
using System.Collections.Generic;
using System.Linq;
using Agent_App.Views;
using Foundation;
using UIKit;

namespace Agent_App.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();
            OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            var mainPage = Xamarin.Forms.Application.Current.MainPage;
            if (mainPage.Navigation.NavigationStack.Last() is LoginPage)
            {
                return UIInterfaceOrientationMask.Portrait;
            }
            else if (UIDevice.CurrentDevice.Model == "iPhone")
            {
                return UIInterfaceOrientationMask.Landscape;
            }
            else
            {
                return UIInterfaceOrientationMask.Portrait;
            }

        }
    }
}
