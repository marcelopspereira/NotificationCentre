using System.ComponentModel;
using Presentation.Core;

namespace NotificationCentre.SideBar.ViewModels
{
    internal sealed class SideBarViewModel : INotifyPropertyChanged, ISideBarViewModel
    {
        private bool _isOpen;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen == value)
                    return;

                _isOpen = value;
                PropertyChanged.Raise(this);
            }
        }
    }
}