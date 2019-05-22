using EventApp.Models;
using EventApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Plugin.Settings;
using Xamarin.Forms;

namespace EventApp.ViewModels
{
    public class MenuViewModel : NotifyBase
    {
        public ICommand EventsPage => new Command(EventsPagePressed);

        public ICommand LogOut => new Command(LogOutPressed);

        public ICommand Settings => new Command(SettingsPressed);

        public ICommand Profile => new Command(ProfilePressed);

        private void SettingsPressed(object obj)
        {
            App.NavigationPage.Navigation.PushAsync(new SettingsWindow()); //the content page you wanna load on this click event 
            App.MenuIsPresented = false;
        }

        private void ProfilePressed(object obj)
        {
            App.NavigationPage.Navigation.PushAsync(new ProfileWindow()); //the content page you wanna load on this click event 
            App.MenuIsPresented = false;
        }

        private void LogOutPressed(object obj)
        {
            CrossSettings.Current.AddOrUpdateValue("Token", string.Empty);
            App.RootPage.Navigation.PopToRootAsync();
        }

        private void EventsPagePressed(object obj)
        {
            App.NavigationPage.Navigation.PushAsync(new EventsWindow()); //the content page you wanna load on this click event 
            App.MenuIsPresented = false;
        }
    }
}
