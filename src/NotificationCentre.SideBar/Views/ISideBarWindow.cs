using Presentation.Interfaces;

namespace NotificationCentre.SideBar.Views
{
    internal interface ISideBarWindow : IWindowWithHandle
    {
        bool Activate();
    }
}