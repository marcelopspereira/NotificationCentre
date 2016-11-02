using System.Collections.ObjectModel;
using NotificationCentre.Alerts.Models;

namespace NotificationCentre.Alerts.ViewModels
{
    internal interface IAlertsViewModel
    {
        ObservableCollection<IAlertModel> Alerts { get; }
    }
}