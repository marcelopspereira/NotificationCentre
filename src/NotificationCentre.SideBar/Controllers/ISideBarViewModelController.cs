using NotificationCentre.SideBar.ViewModels;

namespace NotificationCentre.SideBar.Controllers
{
    internal interface ISideBarViewModelController
    {
        ISideBarViewModel ViewModel { get; }
    }
}