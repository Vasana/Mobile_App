using System;
using System.Collections.Generic;
using System.Text;

namespace Agent_App.Models
{
    public class ChangePasswordBindingModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
        
        public string ConfirmPassword { get; set; }
    }
}
