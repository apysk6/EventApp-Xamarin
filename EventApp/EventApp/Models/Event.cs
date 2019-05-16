using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;
using Xamarin.Forms;

namespace EventApp.Models
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

        [JsonIgnore]
        public string DateText => Date.ToShortDateString();

        [JsonIgnore]
        public string TimeText => Time.ToString();

        public string ImageString { get; set; }

        [JsonIgnore]
        public ImageSource DisplayImage
        {
            get
            {
                var test = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(ImageString)));
                return test;
            }
        }

        public int AccountId { get; set; }
    }
}
