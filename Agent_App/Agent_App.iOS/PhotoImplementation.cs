using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agent_App.Interfaces;
using Agent_App.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoImplementation))]
namespace Agent_App.iOS
{
    public class PhotoImplementation : IPhoto
    {
        public string GetPhotoPath()
        {
            var dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var picturesPath = Path.Combine(dir, "Agent_App_Images");  

            try
            {
                Directory.CreateDirectory(picturesPath);
            }
            catch (Exception e)
            {

            }
            //adding a time stamp time file name to allow saving more than one image... otherwise it overwrites the previous saved image of the same name
            string name = "ProfilePhoto.png";
            string filePath = Path.Combine(picturesPath, name);

            return filePath;
        }
    }
}