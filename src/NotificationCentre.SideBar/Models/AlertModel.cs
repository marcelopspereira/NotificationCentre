using System;
using System.ComponentModel;
using Presentation.Core;

namespace NotificationCentre.SideBar.Models
{
    internal sealed class AlertModel : IAlertModel, INotifyPropertyChanged
    {
        private string _icon;
        private string _title;
        private string _content;
        private DateTime _timestamp;

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

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set
            {
                if (_timestamp == value)
                    return;

                _timestamp = value;
                PropertyChanged.Raise(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}