using System.ComponentModel;

namespace EventApp.Models
{
    public class NotifyBase : INotifyPropertyChanged
    {
        protected void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
