using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class Products
    {
        public string productID { get; set; }
        public string productName { get; set; }
        public string shortDesc { get; set; }
        public string longDesc { get; set; }
        public string imageName { get; set; }
        public string imageUrl { get; set; }
        public string stream { get; set; }
        public bool IsSelected { get; set; }
        public int orderNo { get; set; }
        public string active_flag { get; set; }
    }
}
