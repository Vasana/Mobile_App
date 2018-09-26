using Agent_App.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Agent_App.Helpers 
{
    public class CoversToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // return null if value is not of type List<string>
            //List<string> myModel = value as List<string>;

            // throws exception if value is not of type List<string>

            List<CoverDetails> strList = (List<CoverDetails>)value;
            if (strList != null && strList.Count > 0)
            {
                return (30 * strList.Count);
            }
            else
            {
                return 0;
            }
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

      
    }
}
