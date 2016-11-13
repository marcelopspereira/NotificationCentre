using System.Windows.Input;

namespace NotificationCentre.Alerts.Models
{
    internal interface IAlertModel
    {
        bool HasAlert { get; set; }

        string Id { get; set; }

        string Content { get; set; }

        string Title { get; set; }

        ICommand Timeout { get; set; }

        ICommand Dismiss { get; set; }

        ICommand Action { get; set; }
    }
}