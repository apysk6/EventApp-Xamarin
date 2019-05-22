using EventApp.Models;
using EventApp.Models.Validation;
using EventApp.Views;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EventApp.ViewModels
{
    public class AddEventViewModel : NotifyBase
    {
        private AddEventWindow _addEventWindow;
        private byte[] _image;
        private EventsViewModel _eventsViewModel;
        private bool _isTicketToggled = false;
        private ValidatableObject<string> _name = new ValidatableObject<string>();
        private ValidatableObject<string> _place = new ValidatableObject<string>();
        private ValidatableObject<string> _description = new ValidatableObject<string>();
        private ValidatableObject<double> _price = new ValidatableObject<double>();
        private ValidatableObject<DateTime> _date = new ValidatableObject<DateTime>();
        private ValidatableObject<TimeSpan> _time = new ValidatableObject<TimeSpan>();

        public ValidatableObject<string> Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChange(nameof(Name));
            }
        }

        public ValidatableObject<double> Price
        {
            get => _price;
            set
            {
                _price = value;
                NotifyPropertyChange(nameof(Price));
            }
        }

        public ValidatableObject<DateTime> Date
        {
            get => _date;
            set
            {
                _date = value;
                NotifyPropertyChange(nameof(Date));
            }
        }

        public ValidatableObject<TimeSpan> Time
        {
            get => _time;
            set
            {
                _time = value;
                NotifyPropertyChange(nameof(Time));
            }
        }

        public ValidatableObject<string> Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChange(nameof(Description));
            }
        }

        public ValidatableObject<string> Place
        {
            get => _place;
            set
            {
                _place = value;
                NotifyPropertyChange(nameof(Place));
            }
        }

        public bool IsTicketToggled
        {
            get => _isTicketToggled;
            set
            {
                _isTicketToggled = value;
                NotifyPropertyChange(nameof(IsTicketToggled));

                if (_isTicketToggled)
                {
                    _price.Rules.Add(new IsNotNullOrEmptyRule<double>
                    {
                        ValidationMessage = "Price of the event is required."
                    });
                }
                else
                {
                    _price.Rules.Clear();
                }
            }
        }

        public ICommand AddImage => new Command(async () => await AddImagePressedAsync());

        public ICommand AddEvent => new Command(AddEventPressed);

        public ICommand Back => new Command(BackPressed);

        public AddEventViewModel(AddEventWindow addEventWindow, EventsViewModel eventsViewModel)
        {
            _addEventWindow = addEventWindow;
            _date.Value = DateTime.Now;
            _eventsViewModel = eventsViewModel;

            AddValidationRules();
        }

        private void AddEventPressed()
        {
            bool isError = ValidateEntries();

            if (isError)
                return;

            var newEvent = new Event
            {
                Name = _name.Value.Trim(),
                Place = _place.Value.Trim(),
                Description = _description.Value.Trim(),
                Price = _price.Value,
                Date = _date.Value,
                Time = _time.Value,
                ImageString = Convert.ToBase64String(_image),
            };

            var result = Client.Instance.AddEventAsync(newEvent);

            if (result.Result)
            {
                _addEventWindow.DisplayAlert("Information", "Adding an event was successful.", "OK");
                Task.Run(async () => { await _eventsViewModel.GetAllEvents(); });
                BackPressed();
            }
            else
            {
                _addEventWindow.DisplayAlert("Information", "There was a problem with adding a new event. Try again.", "OK");
            }
        }

        private bool ValidateEntries()
        {
            _name.Validate();
            _place.Validate();
            _description.Validate();
            _date.Validate();
            //_time.Validate();

            var isError = _name.Errors.Any() || _place.Errors.Any() || _description.Errors.Any() || _date.Errors.Any() || _time.Errors.Any();

            return isError;
        }

        private void AddValidationRules()
        {
            _name.Rules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Event name is required."
            });

            _place.Rules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Place of the event is required."
            });

            _description.Rules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Description of the event is required."
            });

            _date.Rules.Add(new DateRule<DateTime>
            {
                ValidationMessage = "Date must be at least today."
            });

            _time.Rules.Add(new TimeRule<TimeSpan>
            {
                ValidationMessage = "Time of the event must be at least two hours ahead."
            });
        }

        private void BackPressed()
        {
            _addEventWindow.Navigation.PopAsync();
        }

        private async Task AddImagePressedAsync()
        {
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                _image = StreamToByte(stream);
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
