using System;
using NotificationCentre.Interfaces;

namespace NotificationCentre.Alerts
{
    internal static class AlertExtensions
    {
        public static IAlert ToAlert(this INotification notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));
            
            return new Alert(notification.Id, notification.Title, notification.Content);
        }
    }
}