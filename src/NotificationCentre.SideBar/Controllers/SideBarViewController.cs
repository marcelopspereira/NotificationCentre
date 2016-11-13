using System.ComponentModel.Composition;
using System.Windows;
using NotificationCentre.SideBar.Views;
using Presentation.Interfaces;

namespace NotificationCentre.SideBar.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class SideBarViewController : ISideBarViewController, IPartImportsSatisfiedNotification
    {
        private readonly IHideFromAppSwitchService _hideFromAppSwitchService;
        private readonly IHideFromPeekService _hideFromPeekService;

        [ImportingConstructor]
        public SideBarViewController(IHideFromAppSwitchService hideFromAppSwitchService, IHideFromPeekService hideFromPeekService)
        {
            _hideFromAppSwitchService = hideFromAppSwitchService;
            _hideFromPeekService = hideFromPeekService;
        }

        [Export]
        public ISideBarWindow View { get; } = new SideBarWindow();

        public void OnImportsSatisfied()
        {
            try
            {
                _hideFromAppSwitchService.HideFromAppSwitch(View);
                _hideFromPeekService.HideFromPeek(View);

                View.Height = SystemParameters.WorkArea.Height;
                View.Left = SystemParameters.WorkArea.Right - View.Width;
            }
            catch
            {}
        }
    }
}
