using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public sealed class SearchCriteria
    {
        private static readonly SearchCriteria instance = new SearchCriteria();

        public bool PremiumsPending { get; set; }

        private SearchCriteria() { }

        public static SearchCriteria Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
