using System;
using System.Net.Mail;

namespace EventApp.Models.Validation
{
    public class EmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            try
            {
                MailAddress email = new MailAddress(str);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
    
        }
    }
}
