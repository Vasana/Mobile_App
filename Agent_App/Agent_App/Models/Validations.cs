using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Agent_App.Models
{
    public class Validations
    {
        public bool password_validations(string password)
        {
            bool result = false;

            if (String.IsNullOrEmpty(password))
                return result;

            
            Regex len = new Regex("^.{8,20}$");
            Regex num = new Regex("\\d");
            Regex chara1 = new Regex("[a-z]");
            Regex chara2 = new Regex("[A-Z]");
            Regex special = new Regex("[><%#@]"); // Put  here more special characters

            if (len.IsMatch(password) && num.IsMatch(password) && chara1.IsMatch(password) && chara2.IsMatch(password) && special.IsMatch(password))
            {
                result = true;
            }

            return result;
        }
    }
}
