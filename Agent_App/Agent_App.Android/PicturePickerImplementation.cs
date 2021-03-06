﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agent_App.Droid;
using Agent_App.Interfaces;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

[assembly: Dependency(typeof(PicturePickerImplementation))]
namespace Agent_App.Droid
{
    public class PicturePickerImplementation : IPicturePicker
    {
        public Task<Stream> GetImageStreamAsync()
        {
            try
            {
                // Define the Intent for getting images
                Intent intent = new Intent();
                intent.SetType("image/*");
                intent.SetAction(Intent.ActionGetContent);

                // Start the picture-picker activity (resumes in MainActivity.cs)
                MainActivity.Instance.StartActivityForResult(
                    Intent.CreateChooser(intent, "Select Picture"),
                    MainActivity.PickImageId);

                // Save the TaskCompletionSource object as a MainActivity property
                MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

                
                
            }
            catch (Exception e)
            {

            }
            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}