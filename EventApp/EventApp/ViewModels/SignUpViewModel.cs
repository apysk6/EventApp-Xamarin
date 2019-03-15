using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using EventApp.Models;
using EventApp.Models.Validation;
using EventApp.Views;
using Xamarin.Forms;

namespace EventApp.ViewModels
{
    public class SignUpViewModel : NotifyBase
    {
        private readonly SignUpWindow _signUpWindow;
        private ValidatableObject<string> _firstName = new ValidatableObject<string>();
        private ValidatableObject<string> _secondName = new ValidatableObject<string>();
        private ValidatableObject<string> _email = new ValidatableObject<string>();
        private ValidatableObject<string> _password = new ValidatableObject<string>();
        private ValidatableObject<string> _confirmedPassword = new ValidatableObject<string>();

        public ValidatableObject<string> FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyPropertyChange(nameof(FirstName));
            }
        }

        public ValidatableObject<string> SecondName
        {
            get => _secondName;
            set
            {
                _secondName = value;
                NotifyPropertyChange(nameof(SecondName));
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

        public ICommand Register => new Command(RegisterPressed);

        public ICommand Back => new Command(BackPressed);

        public SignUpViewModel(SignUpWindow signUpWindow)
        {
            _signUpWindow = signUpWindow;
            AddValidationRules();
        }

        private void AddValidationRules()
        {
            _firstName.Rules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "First name is required."
            });

            _secondName.Rules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Second name is required."
            });

            _email.Rules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Email is required."
            });

            _email.Rules.Add(new EmailRule<string>
            {
                ValidationMessage = "Email is not valid."
            });
        }

        private void BackPressed(object obj)
        {
            _signUpWindow.Navigation.PopAsync();
        }

        private void RegisterPressed(object obj)
        {
             ValidateEntries();

             var isError = _firstName.Errors.Any() || _secondName.Errors.Any() || _email.Errors.Any();

             if (isError)
                 return;

             // Client service.
        }

        private void ValidateEntries()
        {
            _firstName.Validate();
            _secondName.Validate();
            _email.Validate();
        }
    }
}
