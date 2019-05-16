using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventAppApi.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string ImageString { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
