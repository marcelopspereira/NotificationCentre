using Presentation.Interfaces;

namespace NotificationCentre.SideBar.Views
{
    internal interface ISideBarWindow : IWindow
    {
        object DataContext { get; set; }
    }
}