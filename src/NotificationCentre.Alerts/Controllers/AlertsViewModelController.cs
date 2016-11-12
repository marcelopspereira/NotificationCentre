using System.ComponentModel.Composition;
using NotificationCentre.Alerts.ViewModels;
using NotificationCentre.Interfaces;

namespace NotificationCentre.Alerts.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class AlertsViewModelController : IPartImportsSatisfiedNotification, IAlertsViewModelController
    {
        private readonly INotificationManager _notificationService;

        [ImportingConstructor]
        public AlertsViewModelController(INotificationManager notificationService)
        {
            _notificationService = notificationService;
        }

        [Export]
        public IAlertsViewModel ViewModel { get; } = new AlertsViewModel();

        public void OnImportsSatisfied()
        {
            
        }
    }
}