using System;
using System.Windows.Input;

namespace NotificationCentre.SideBar.Models
{
    internal interface INotificationModel
    {
        string Id { get; set; }

        string Title { get; set; }

        string Content { get; set; }

        DateTime Timestamp { get; set; }

        ICommand Action { get; set; }
    }
}