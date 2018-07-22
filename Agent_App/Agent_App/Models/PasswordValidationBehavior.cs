using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Agent_App.Models
{
    public class PasswordValidationBehavior:Behavior<Entry>
    {

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += BindableOnTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= BindableOnTextChanged;
        }
        private void BindableOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textboks = sender as Entry;
            string password = e.NewTextValue as string;
            Validations val = new Validations();

            if (val.password_validations(password))
            { 
                textboks.BackgroundColor = Color.Transparent;
            }
            else
            {
                textboks.BackgroundColor = Color.Pink;
            }
        }
    }

    
}
