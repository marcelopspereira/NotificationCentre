using System.ComponentModel.Composition;
using System.Windows;
using NotificationCentre.Alerts.Views;

namespace NotificationCentre.Alerts.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class AlertsViewController : IPartImportsSatisfiedNotification, IAlertsViewController
    {
        [Export]
        public IAlertsWindow View { get; } = new AlertsWindow();

        public void OnImportsSatisfied()
        {
            View.Left = SystemParameters.WorkArea.Right - View.Width;
        }
    }
}