using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public sealed class PolicyFlag
    {
        private static readonly PolicyFlag instance = new PolicyFlag();

        public string PolicyNumber { get; set; }
        public bool Flagged { get; set; }
        public string Comment { get; set; }

        private PolicyFlag() { }

        public static PolicyFlag Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
