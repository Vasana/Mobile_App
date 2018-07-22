using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agent_App.Droid;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Agent_App.Interfaces;
using System.IO;
using System.Threading.Tasks;

[assembly: Dependency(typeof(PhotoImplementation))]
namespace Agent_App.Droid
{
    class PhotoImplementation : IPhoto
    {
        public string GetPhotoPath()
        {            
            var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
            var pictures = dir.AbsolutePath;
            //adding a time stamp time file name to allow saving more than one image... otherwise it overwrites the previous saved image of the same name
            string name = "images.jpeg";
            string filePath = System.IO.Path.Combine(pictures, name);
            
            return filePath;
        }
    }
}