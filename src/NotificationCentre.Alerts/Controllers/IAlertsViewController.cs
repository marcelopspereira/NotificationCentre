using NotificationCentre.Alerts.Views;

namespace NotificationCentre.Alerts.Controllers
{
    internal interface IAlertsViewController
    {
        IAlertsWindow View { get; }
    }
}