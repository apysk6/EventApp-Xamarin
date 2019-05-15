using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Models.Validation
{
    public class DateRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var time = (DateTime)(object)value;
            int timeCompare = DateTime.Compare(time.Date, DateTime.Now.Date);

            if (timeCompare < 0)
                return false;


            return true;
        }
    }
}
