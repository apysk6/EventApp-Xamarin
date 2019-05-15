using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Models.Validation
{
    public class TimeRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var time = (TimeSpan)(object)value;
            var hourNow = DateTime.Now.TimeOfDay;
            hourNow.Add(TimeSpan.FromHours(2));
            int timeCompare = TimeSpan.Compare(time, hourNow);

            if (timeCompare < 0)
                return false;

            return true;
        }
    }
}
