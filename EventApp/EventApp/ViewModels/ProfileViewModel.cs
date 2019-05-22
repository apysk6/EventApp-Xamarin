using EventApp.Models;
using EventApp.Models.Validation;
using EventApp.Views;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace EventApp.ViewModels
{
    public class ProfileViewModel : NotifyBase
    {
        private ValidatableObject<string> _firstName = new ValidatableObject<string>();
        private ValidatableObject<string> _surname = new ValidatableObject<string>();
        private ValidatableObject<string> _email = new ValidatableObject<string>();
        private ValidatableObject<string> _password = new ValidatableObject<string>();
        private ValidatableObject<string> _confirmedPassword = new ValidatableObject<string>();
        private ValidatableObject<string> _city = new ValidatableObject<string>();
        private int _userId;
        private ProfileWindow _profileWindow;

        public ValidatableObject<string> FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyPropertyChange(nameof(FirstName));
            }
        }

        public ValidatableObject<string> Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                NotifyPropertyChange(nameof(Surname));
            }
        }

        public ValidatableObject<string> City
        {
            get => _city;
            set
            {
                _city = value;
                NotifyPropertyChange(nameof(City));
            }
        }

        public ValidatableObject<string> Email
        {
            get => _email;
            set
            {
                _email = value;
                NotifyPropertyChange(nameof(Email));
            }
        }

        public ValidatableObject<string> Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChange(nameof(Password));
            }
        }

        public ValidatableObject<string> ConfirmedPassword
        {
            get => _confirmedPassword;
            set
            {
                _confirmedPassword = value;
                NotifyPropertyChange(nameof(ConfirmedPassword));
            }
        }

        public ICommand Change => new Command(ChangePressed);

        public ProfileViewModel(ProfileWindow profileWindow)
        {
            _profileWindow = profileWindow;

            AddValidationRules();

            var currentUser = Client.Instance.GetCurrentAccount().Result;

            FirstName.Value = currentUser.FirstName;
            Password.Value = currentUser.Password;
            ConfirmedPassword.Value = currentUser.Password;
            City.Value = currentUser.City;
            Email.Value = currentUser.Email;
            Surname.Value = currentUser.Surname;

            _userId = currentUser.Id;
        }

        private void AddValidationRules()
        {
            _firstName.Rules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "First name is required."
            });

            _surname.Rules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Second name is required."
            });

            _city.Rules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "City is required."
            });

            _email.Rules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Email is required."
            });

            _email.Rules.Add(new EmailRule<string>
            {
                ValidationMessage = "Email is not valid."
            });

            _password.Rules.Add(new IsNotNullOrEmptyRule<string>()
            {
                ValidationMessage = "Password is required."
            });

            _confirmedPassword.Rules.Add(new IsNotNullOrEmptyRule<string>()
            {
                ValidationMessage = "Password confirmation is required."
            });

            _password.Rules.Add(new PasswordRule<string>()
            {
                ValidationMessage = "Password must contain one lower case, one upper case, one digit and be 8 characters long."
            });

            _confirmedPassword.Rules.Add(new PasswordRule<string>()
            {
                ValidationMessage = "Password must contain one lower case, one upper case, one digit and be 8 characters long."
            });

        }

        private void ChangePressed(object obj)
        {
            ValidateEntries();

            var isError = _firstName.Errors.Any() || _surname.Errors.Any() || _email.Errors.Any() || _city.Errors.Any() || _confirmedPassword.Errors.Any();

            if (isError)
                return;

            var result = Client.Instance.UpdateAccount(_userId, _email.Value, _password.Value, _firstName.Value, _surname.Value, _city.Value);

            if (result.Result)
            {
                _profileWindow.DisplayAlert("Information", "Changing your account informations was successful.", "OK");
            }
            else
            {
                _profileWindow.DisplayAlert("Information", "There was a problem with changing your account informations. Try again.", "OK");
            }
        }

        private void ValidateEntries()
        {
            _firstName.Validate();
            _surname.Validate();
            _email.Validate();
            _city.Validate();
            _password.Validate();
            _confirmedPassword.Validate();

            if (string.IsNullOrEmpty(_password.Value) || string.IsNullOrEmpty(_confirmedPassword.Value))
                return;

            if (!_password.Value.Equals(_confirmedPassword.Value))
                _confirmedPassword.Errors.Add("Passwords must match.");
        }
    }
}
