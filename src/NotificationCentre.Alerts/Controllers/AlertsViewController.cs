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
        private readonly IHideFromPeekService _hideFromPeekService;

        [ImportingConstructor]
        public AlertsViewController(IHideFromAppSwitchService hideFromAppSwitchService, IHideFromPeekService hideFromPeekService)
        {
            _hideFromAppSwitchService = hideFromAppSwitchService;
            _hideFromPeekService = hideFromPeekService;
        }

        [Export]
        public IAlertsWindow View { get; } = new AlertsWindow();

        public void OnImportsSatisfied()
        {
            try
            {
                _hideFromAppSwitchService.HideFromAppSwitch(View);
                _hideFromPeekService.HideFromPeek(View);

                View.Left = SystemParameters.WorkArea.Right - View.Width;
            }
            catch
            {}
        }
    }
}