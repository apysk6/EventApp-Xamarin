using EventApp.Models;
using EventApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EventApp.ViewModels
{
    public class EventsViewModel : NotifyBase
    {
        private EventsWindow _eventsWindow;
        private string _selectedFilter;
        private ObservableCollection<Event> _events = new ObservableCollection<Event>();
        private List<string> _filters = new List<string>();
        private Event _selectedEvent;

        public ObservableCollection<Event> Events
        {
            get => _events;
            set
            {
                _events = value;
                NotifyPropertyChange(nameof(Events));
            }
        }

        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                NotifyPropertyChange(nameof(SelectedEvent));
                EventDetails();
            }
        }

        public List<string> Filters
        {
            get => _filters;
            set
            {
                _filters = value;
                NotifyPropertyChange(nameof(Filters));
            }
        }

        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                NotifyPropertyChange(nameof(SelectedFilter));

                if (_selectedFilter != null)
                {
                    switch (_selectedFilter)
                    {
                        case "All":
                            Task.Run(async () => { await GetAllEvents(); });
                            break;
                        case "In your city":
                            Task.Run(async () => { await GetEventsInYourCity(); });
                            break;
                    }
                }
            }
        }

        public ICommand AddEvent => new Command(AddEventPressed);

        public EventsViewModel(EventsWindow eventsWindow)
        {
            _eventsWindow = eventsWindow;
            Filters.Add("All");
            Filters.Add("In your city");
            SelectedFilter = Filters.First();

            Task.Run(async () => { await GetAllEvents(); });
        }
        private void AddEventPressed()
        {
            _eventsWindow.Navigation.PushAsync(new AddEventWindow(this));
        }

        private void EventDetails()
        {
            _eventsWindow.Navigation.PushAsync(new EventDetailsWindow(_selectedEvent));
        }

        public async Task GetAllEvents()
        {
            var events = await Client.Instance.GetEvents();
            Events = new ObservableCollection<Event>(events.ToList());
        }

        public async Task GetEventsInYourCity()
        {
            var events = await Client.Instance.GetEventsInYourCity();
            Events = new ObservableCollection<Event>(events.ToList());
        }
    }
}
