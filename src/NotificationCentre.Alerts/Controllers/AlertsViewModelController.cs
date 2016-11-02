using System.ComponentModel.Composition;
using NotificationCentre.Alerts.ViewModels;

namespace NotificationCentre.Alerts.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class AlertsViewModelController : IPartImportsSatisfiedNotification, IAlertsViewModelController
    {
        [Export]
        public IAlertsViewModel ViewModel { get; } = new AlertsViewModel();

        public void OnImportsSatisfied()
        {
            
        }
    }
}