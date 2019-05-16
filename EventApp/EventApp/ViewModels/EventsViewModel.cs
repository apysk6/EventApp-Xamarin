using EventApp.Models;
using EventApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.ViewModels
{
    public class EventsViewModel : NotifyBase
    {
        private EventsWindow _eventsWindow;
        private ObservableCollection<Event> _events = new ObservableCollection<Event>();

        public ObservableCollection<Event> Events
        {
            get => _events;
            set
            {
                _events = value;
                NotifyPropertyChange(nameof(Events));
            }
        }

        public EventsViewModel(EventsWindow eventsWindow)
        {
            _eventsWindow = eventsWindow;

            Task.Run(async () => { await GetAllEvents(); });
        }

        private async Task GetAllEvents()
        {
            var events = await Client.Instance.GetEvents();
            Events = new ObservableCollection<Event>(events.ToList());
        }
    }
}
