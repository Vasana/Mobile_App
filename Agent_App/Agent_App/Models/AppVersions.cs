using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class AppVersions
    {
        public int BuildNo { get; set; }
        public string VersionNo { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string IsMajorUpdate { get; set; }
    }
}
