using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Agent_App.Models
{
    public class BranchContact
    {
        public string Name { get; set; } = "";
        public string District { get; set; } = "";
        public string Address { get; set; } = "";
        public string ContactNumber1 { get; set; } = "";
        public string ContactNumber2 { get; set; } = "";
        public string ContactNumber3 { get; set; } = "";
        public string ContactNumber4 { get; set; } = "";
        public string SMContactNumber { get; set; } = "";
        public string RSMContactNumber { get; set; } = "";
        public string BrnAdminContactNo { get; set; } = "";
        public bool IsSelected { get; set; }

        public Color BackgroundColor
        {
            get
            {
                if (IsSelected)
                    return Color.FromHex("#FAEBD7");
                else
                    return Color.Transparent;
            }
        }
    }
}
