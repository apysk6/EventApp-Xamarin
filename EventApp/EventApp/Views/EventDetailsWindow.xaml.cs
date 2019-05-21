using EventApp.Models;
using EventApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailsWindow : ContentPage
    {
        public EventDetailsWindow(Event selectedEvent)
        {
            InitializeComponent();
            BindingContext = new EventDetailsViewModel(this, selectedEvent);
        }
    }
}