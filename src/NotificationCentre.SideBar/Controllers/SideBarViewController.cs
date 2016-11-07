using System.ComponentModel.Composition;
using System.Windows;
using NotificationCentre.SideBar.Views;

namespace NotificationCentre.SideBar.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class SideBarViewController : ISideBarViewController, IPartImportsSatisfiedNotification
    {
        [Export]
        public ISideBarWindow View { get; } = new SideBarWindow();

        public void OnImportsSatisfied()
        {
            View.Height = SystemParameters.WorkArea.Height;
            View.Left = SystemParameters.WorkArea.Right - 320;
        }
    }
}
