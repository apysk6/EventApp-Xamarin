using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EventApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsWindow : ContentPage
	{
		public SettingsWindow ()
		{
			InitializeComponent ();
		    BindingContext = new SettingsViewModel();
		}
	}
}