using System;
using System.Collections.Generic;
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
        private ValidatableObject<string> _firstName;
        private string _secondName;
        private string _email;
        private string _password;
        private string _confirmedPassword;

        public ValidatableObject<string> FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyPropertyChange(nameof(FirstName));
            }
        }

        public string SecondName
        {
            get => _secondName;
            set
            {
                _secondName = value;
                NotifyPropertyChange(nameof(SecondName));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                NotifyPropertyChange(nameof(Email));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChange(nameof(Password));
            }
        }

        public string ConfirmedPassword
        {
            get => _confirmedPassword;
            set
            {
                _confirmedPassword = value;
                NotifyPropertyChange(nameof(ConfirmedPassword));
            }
        }

        public bool CanLogin =>
            !string.IsNullOrEmpty(_email) && !string.IsNullOrEmpty(_password) &&
            !string.IsNullOrEmpty(_confirmedPassword) &&
            !string.IsNullOrEmpty(_firstName.Value) &&
            !string.IsNullOrEmpty(_secondName);

        public ICommand Register => new Command(RegisterPressed);

        public ICommand Back => new Command(BackPressed);

        public SignUpViewModel(SignUpWindow signUpWindow)
        {
            _signUpWindow = signUpWindow;

            _signUpWindow.FindByName<Entry>("emailEntry").TextChanged += FormTextChanged;
            _signUpWindow.FindByName<Entry>("firstNameEntry").TextChanged += FormTextChanged;
            _signUpWindow.FindByName<Entry>("secondNameEntry").TextChanged += FormTextChanged;
            _signUpWindow.FindByName<Entry>("passwordEntry").TextChanged += FormTextChanged;
            _signUpWindow.FindByName<Entry>("confirmedPasswordEntry").TextChanged += FormTextChanged;

            AddValidationRules();
        }

        private void AddValidationRules()
        {
            _firstName.Rules.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "First name is required."
            });
        }

        private void FormTextChanged(object sender, TextChangedEventArgs e)
        {
            NotifyPropertyChange(nameof(CanLogin));
        }

        private void BackPressed(object obj)
        {
            _signUpWindow.Navigation.PopAsync();
        }

        private void RegisterPressed(object obj)
        {
            //Client.
        }
    }
}
