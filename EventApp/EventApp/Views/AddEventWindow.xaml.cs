﻿using EventApp.ViewModels;
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
	public partial class AddEventWindow : ContentPage
	{
		public AddEventWindow (EventsViewModel eventsViewModel)
		{
			InitializeComponent();
            BindingContext = new AddEventViewModel(this, eventsViewModel);
		}
	}
}