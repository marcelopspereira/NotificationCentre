using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using NotificationCentre.SideBar.Models;
using Presentation.Core;

namespace NotificationCentre.SideBar.ViewModels
{
    internal sealed class SideBarViewModel : INotifyPropertyChanged, ISideBarViewModel
    {
        private bool _isOpen;
        private ICommand _switchTheme;

        public event PropertyChangedEventHandler PropertyChanged;        

        public ICommand SwitchTheme
        {
            get { return _switchTheme; }
            set
            {
                if (_switchTheme == value)
                    return;

                _switchTheme = value;
                PropertyChanged.Raise(this);
            }
        }

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

        public ObservableCollection<IAlertModel> Alerts { get; } = new ObservableCollection<IAlertModel>();
    }
}