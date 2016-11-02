using System.ComponentModel;
using System.Windows.Input;

namespace NotificationCentre.Alerts.Models
{
    internal sealed class AlertModel : INotifyPropertyChanged, IAlertModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Icon { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public ICommand Timeout { get; set; }

        public ICommand Dismiss { get; set; }

        public ICommand Action { get; set; }
    }
}