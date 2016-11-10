using System;

namespace NotificationCentre.SideBar.Models
{
    internal interface INotificationModel
    {
        string Title { get; set; }

        string Content { get; set; }

        DateTime Timestamp { get; set; }
    }
}