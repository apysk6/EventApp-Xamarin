using EventApp.Models;
using EventApp.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsWindow : ContentPage
    {
        public EventsWindow()
        {
            InitializeComponent();
            BindingContext = new EventsViewModel(this);
        }
    }
}