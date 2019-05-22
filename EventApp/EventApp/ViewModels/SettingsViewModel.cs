using System;
using System.Collections.Generic;
using System.Text;
using EventApp.Models;
using Plugin.Settings;

namespace EventApp.ViewModels
{
   public class SettingsViewModel : NotifyBase
   {
       private bool _isAutologinEnabled = CrossSettings.Current.GetValueOrDefault("Autologin", true);

        public bool IsAutologinEnabled
        {
            get => _isAutologinEnabled;
            set
            {
                _isAutologinEnabled = value;
                CrossSettings.Current.AddOrUpdateValue("Autologin", _isAutologinEnabled);
            }
        }
    }
}
