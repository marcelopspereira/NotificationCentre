using System;

namespace NotificationCentre.SideBar.Models
{
    internal interface IAlertModel
    {
        string Icon { get; set; }

        string Title { get; set; }

        string Content { get; set; }

        DateTime Timestamp { get; set; }
    }
}