using System.Collections.ObjectModel;
using System.Windows.Input;
using NotificationCentre.SideBar.Models;

namespace NotificationCentre.SideBar.ViewModels
{
    internal interface ISideBarViewModel
    {
        bool IsOpen { get; set; }

        string SearchString { get; set; }

        ICommand ExitApplication { get; set; }

        ICommand ClearSearch { get; set; }

        ICommand SwitchTheme { get; set; }

        ObservableCollection<INotificationModel> Notifications { get; }
    }
}