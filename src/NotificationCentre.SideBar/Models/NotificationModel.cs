using System;
using System.ComponentModel;
using System.Windows.Input;
using Presentation.Core;

namespace NotificationCentre.SideBar.Models
{
    internal sealed class NotificationModel : INotificationModel, INotifyPropertyChanged
    {
        private string _id;
        private string _title;
        private string _content;
        private DateTime _timestamp;
        private ICommand _action;

        public NotificationModel(string id, string title, string content, DateTime timestamp, ICommand action)
        {
            _id = id;
            _title = title;
            _content = content;
            _timestamp = timestamp;
            _action = action;
        }

        public string Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                    return;

                _id = value;
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}