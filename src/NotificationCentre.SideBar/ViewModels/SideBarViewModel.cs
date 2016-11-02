using System.ComponentModel;

namespace NotificationCentre.SideBar.ViewModels
{
    internal sealed class SideBarViewModel : INotifyPropertyChanged, ISideBarViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsOpen { get; set; }
    }
}