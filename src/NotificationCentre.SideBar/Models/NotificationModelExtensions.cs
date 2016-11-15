using System.Windows.Input;
using NotificationCentre.Interfaces;

namespace NotificationCentre.SideBar.Models
{
    internal static class NotificationModelExtensions
    {
        public static INotificationModel ToModel(this INotification notification, ICommand action)
        {
            return new NotificationModel(notification.Id, notification.Title, notification.Content, notification.Timestamp, action);
        }
    }
}