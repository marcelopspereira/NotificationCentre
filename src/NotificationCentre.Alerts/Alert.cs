namespace NotificationCentre.Alerts
{
    internal sealed class Alert : IAlert
    {
        public Alert(string id, string title, string content)
        {
            Id = id;
            Title = title;
            Content = content;
        }

        public string Id { get; }

        public string Title { get; }

        public string Content { get; }
    }
}