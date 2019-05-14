using EventApp.Models;
using EventApp.Views;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EventApp.ViewModels
{
    public class AddEventViewModel : NotifyBase
    {
        private AddEventWindow _addEventWindow;
        private byte[] _eventImage;

        public ICommand AddImage => new Command(async () => await AddImagePressedAsync());

        public AddEventViewModel(AddEventWindow addEventWindow)
        {
            _addEventWindow = addEventWindow;
        }

        private async Task AddImagePressedAsync()
        {
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                _eventImage = StreamToByte(stream);
            }
        }

        private byte[] StreamToByte(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
