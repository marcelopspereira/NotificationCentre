using System.Collections.ObjectModel;
using System.ComponentModel;
using NotificationCentre.Alerts.Models;

namespace NotificationCentre.Alerts.ViewModels
{
    internal sealed class AlertsViewModel : INotifyPropertyChanged, IAlertsViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<IAlertModel> Alerts { get; } = new ObservableCollection<IAlertModel>();
    }
}