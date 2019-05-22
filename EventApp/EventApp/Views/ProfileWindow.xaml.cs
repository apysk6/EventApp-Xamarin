using EventApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileWindow : ContentPage
    {
        public ProfileWindow()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel(this);
        }
    }
}