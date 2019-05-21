using EventApp.Models;
using EventApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EventApp.ViewModels
{
    public class EventDetailsViewModel : NotifyBase
    {
        private EventDetailsWindow _eventDetailsWindow;
        private Event _selectedEvent;
        private string _name = string.Empty;
        private string _place = string.Empty;
        private string _description = string.Empty;
        private string _date = string.Empty;
        private string _time = string.Empty;
        private string _tickets = string.Empty;
        private bool _priceVisibility = false;

        public string Tickets
        {
            get => _tickets;
            set
            {
                _tickets = value;
                NotifyPropertyChange(nameof(Tickets));
            }
        }

        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                NotifyPropertyChange(nameof(Time));
            }
        }

        public string Date
        {
            get => _date;
            set
            {
                _date = value;
                NotifyPropertyChange(nameof(Date));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChange(nameof(Description));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChange(nameof(Name));
            }
        }

        public string Place
        {
            get => _place;
            set
            {
                _place = value;
                NotifyPropertyChange(nameof(Place));
            }
        }

        public bool PriceVisibility
        {
            get => _priceVisibility;
            set
            {
                _priceVisibility = value;
                NotifyPropertyChange(nameof(PriceVisibility));
            }
        }

        public ImageSource DisplayImage { get; set; }

        public ICommand Back => new Command(BackPressed);

        public EventDetailsViewModel(EventDetailsWindow eventDetailsWindow, Event selectedEvent)
        {
            _eventDetailsWindow = eventDetailsWindow;

            Time = selectedEvent.TimeText;
            Date = selectedEvent.DateText;
            Description = selectedEvent.Description;
            Name = selectedEvent.Name;
            Place = selectedEvent.Place;
            DisplayImage = selectedEvent.DisplayImage;

            if (selectedEvent.Price > 0)
            {
                PriceVisibility = true;
                Tickets = "Tickets price: " + selectedEvent.Price;
            }
        }

        private void BackPressed()
        {
            _eventDetailsWindow.Navigation.PopAsync();
        }
    }
}
