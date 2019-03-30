using System.Linq;
using System.Threading.Tasks;
using EventApp.Views;
using System.Windows.Input;
using EventApp.Models;
using EventApp.Models.Validation;
using Plugin.Settings;
using Xamarin.Forms;

namespace EventApp.ViewModels
{
    public class SignInViewModel : NotifyBase
    {
        private readonly SignInWindow _signInWindow;
        private ValidatableObject<string> _email = new ValidatableObject<string>();
        private ValidatableObject<string> _password = new ValidatableObject<string>();
        private ValidatableObject<string> _confirmedPassword = new ValidatableObject<string>();

        public ICommand Login => new Command(LoginPressed);

        public ICommand Register => new Command(RegisterPressed);

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


        public SignInViewModel(SignInWindow signInWindow)
        {
            _signInWindow = signInWindow;
            AddValidationRules();
        }

        private void AddValidationRules()
        {
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
        }

        private void LoginPressed(object obj)
        {
            ValidateEntries();

            var isError = _email.Errors.Any() || _password.Errors.Any() || _confirmedPassword.Errors.Any() ;

            if (isError)
                return;
        }

        private void ValidateEntries()
        {
            _email.Validate();
            _password.Validate();
            _confirmedPassword.Validate();

            if (string.IsNullOrEmpty(_password.Value) || string.IsNullOrEmpty(_confirmedPassword.Value))
                return;

            if (!_password.Value.Equals(_confirmedPassword.Value))
                _confirmedPassword.Errors.Add("Passwords must match.");

            var currentToken = CrossSettings.Current.GetValueOrDefault("Token", string.Empty);

            if (string.IsNullOrEmpty(currentToken))
            {
                bool loginSuccess = Client.Instance.LoginAsync(_email.Value, _password.Value).Result;
            }
            else
            {

            }
        }

        private void RegisterPressed(object obj)
        {
            _signInWindow.Navigation.PushAsync(new SignUpWindow());
        }

    }
}
