using NotificationCentre.Alerts.ViewModels;

namespace NotificationCentre.Alerts.Controllers
{
    internal interface IAlertsViewModelController
    {
        IAlertsViewModel ViewModel { get; }
    }
}