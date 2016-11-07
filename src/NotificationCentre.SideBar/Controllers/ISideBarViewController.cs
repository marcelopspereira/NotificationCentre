using NotificationCentre.SideBar.Views;

namespace NotificationCentre.SideBar.Controllers
{
    internal interface ISideBarViewController
    {
        ISideBarWindow View { get; }
    }
}