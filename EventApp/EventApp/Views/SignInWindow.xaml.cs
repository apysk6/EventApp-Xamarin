using EventApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignInWindow : ContentPage
	{
		public SignInWindow ()
		{
			InitializeComponent ();
            var signUpViewModel = new SignInViewModel(this);
            BindingContext = signUpViewModel;
		}
	}
}