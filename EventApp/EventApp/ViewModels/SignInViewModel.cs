using EventApp.Views;
using System.Windows.Input;
using EventApp.Models;
using Xamarin.Forms;

namespace EventApp.ViewModels
{
    public class SignInViewModel : NotifyBase
    {
        private readonly SignInWindow _signInWindow;
        private string _email;
        private string _password;
        private string _confirmedPassword;

        public ICommand Login => new Command(LoginPressed);

        public ICommand Register => new Command(RegisterPressed);

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
            !string.IsNullOrEmpty(_confirmedPassword);

        public SignInViewModel(SignInWindow signInWindow)
        {
            _signInWindow = signInWindow;

            _signInWindow.FindByName<Entry>("emailEntry").TextChanged += FormTextChanged;
            _signInWindow.FindByName<Entry>("passwordEntry").TextChanged += FormTextChanged;
            _signInWindow.FindByName<Entry>("confirmedPasswordEntry").TextChanged += FormTextChanged;
        }

        private void FormTextChanged(object sender, TextChangedEventArgs e)
        {
            NotifyPropertyChange(nameof(CanLogin));   
        }

        private void LoginPressed(object obj)
        {
            
        }

        private void RegisterPressed(object obj)
        {

        }

    }
}
