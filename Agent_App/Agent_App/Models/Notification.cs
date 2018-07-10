using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class Notification
    {
        public string PolicyNumber { get; set; } = "";
        public string Department { get; set; } = "";
        public string BusinessType { get; set; } = "";
        public string InsuredName { get; set; } = "";
        public string ContactNumber { get; set; } = "";
        public string EventName { get; set; } = "";
        public string EventDescription { get; set; } = "";
        public string EventImage { get; set; } = "";
        public bool IsSelected { get; set; }
        public string NotifiedDate { get; set; } = "";
    }
}
