using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            InitializeComponent();     
        }
    }
}