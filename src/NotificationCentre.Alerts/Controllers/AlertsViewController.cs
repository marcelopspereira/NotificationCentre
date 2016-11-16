using System.ComponentModel.Composition;
using System.Windows;
using NotificationCentre.Alerts.Views;
using Presentation.Interfaces;

namespace NotificationCentre.Alerts.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class AlertsViewController : IPartImportsSatisfiedNotification, IAlertsViewController
    {
        private readonly IHideFromAppSwitchService _hideFromAppSwitchService;

        [ImportingConstructor]
        public AlertsViewController(IHideFromAppSwitchService hideFromAppSwitchService)
        {
            _hideFromAppSwitchService = hideFromAppSwitchService;
        }

        [Export]
        public IAlertsWindow View { get; } = new AlertsWindow();

        public void OnImportsSatisfied()
        {
            _hideFromAppSwitchService.HideFromAppSwitch(View);

            View.Left = SystemParameters.WorkArea.Right - View.Width;
        }
    }
}