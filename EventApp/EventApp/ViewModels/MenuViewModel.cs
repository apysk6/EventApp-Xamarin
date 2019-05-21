using EventApp.Models;
using EventApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EventApp.ViewModels
{
    public class MenuViewModel : NotifyBase
    {
        public ICommand EventsPage => new Command(EventsPagePressed);

        private void EventsPagePressed(object obj)
        {
            App.NavigationPage.Navigation.PushAsync(new EventsWindow()); //the content page you wanna load on this click event 
            App.MenuIsPresented = false;
        }
    }
}
