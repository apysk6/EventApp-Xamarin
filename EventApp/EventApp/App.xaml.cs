using EventApp.Views;
using System;
using Plugin.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EventApp
{
    public partial class App : Application
    {
        public static NavigationPage NavigationPage { get; set; }
        public static RootPage RootPage;
        public static bool MenuIsPresented
        {
            get
            {
                return RootPage.IsPresented;
            }
            set
            {
                RootPage.IsPresented = value;
            }
        }


        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SignInWindow());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            bool isAutologin = CrossSettings.Current.GetValueOrDefault("Autologin", true);

            if (!isAutologin)
            {
                CrossSettings.Current.AddOrUpdateValue("Token", string.Empty);
            }
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
