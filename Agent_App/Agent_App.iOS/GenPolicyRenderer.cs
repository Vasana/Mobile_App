using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agent_App.iOS;
using Agent_App.Views;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GenPolicyDetails), typeof(GenPolicyRenderer))]
namespace Agent_App.iOS
{
    public class GenPolicyRenderer : PageRenderer
    {
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromNInt((int)(UIInterfaceOrientation.Unknown)), new NSString("orientation"));
        }
    }
}