using System.ComponentModel;
using System.Windows.Input;
using Presentation.Core;

namespace NotificationCentre.Alerts.Models
{
    internal sealed class AlertModel : INotifyPropertyChanged, IAlertModel
    {
        private bool _isFree;
        private string _icon;
        private string _title;
        private string _content;
        private ICommand _timeout;
        private ICommand _dismiss;
        private ICommand _action;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsFree
        {
            get { return _isFree; }
            set
            {
                if (_isFree == value)
                    return;

                _isFree = value;
                PropertyChanged.Raise(this);
            }
        }

        public string Icon
        {
            get { return _icon; }
            set
            {
                if (_icon == value)
                    return;

                _icon = value;
                PropertyChanged.Raise(this);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                    return;

                _title = value;
                PropertyChanged.Raise(this);
            }
        }

        public string Content
        {
            get { return _content; }
            set
            {
                if (_content == value)
                    return;

                _content = value;
                PropertyChanged.Raise(this);
            }
        }

        public ICommand Timeout
        {
            get { return _timeout; }
            set
            {
                if (_timeout == value)
                    return;

                _timeout = value;
                PropertyChanged.Raise(this);
            }
        }

        public ICommand Dismiss
        {
            get { return _dismiss; }
            set
            {
                if (_dismiss == value)
                    return;
                
                _dismiss = value;
                PropertyChanged.Raise(this);
            }
        }

        public ICommand Action
        {
            get { return _action; }
            set
            {
                if (_action == value)
                    return;

                _action = value;
                PropertyChanged.Raise(this);
            }
        }
    }
}