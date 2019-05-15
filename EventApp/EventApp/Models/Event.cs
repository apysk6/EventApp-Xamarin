using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Models
{
    public class Event
    {
        public string Name { get; set; }

        public string Place { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public byte[] Image { get; set; }

        public int AccountId { get; set; }
    }
}
