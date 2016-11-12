namespace NotificationCentre.Transports
{
    internal static class TransportConstants
    {
        public static class Topics
        {
            public const string Post = "/notificationcentre/post";
            public const string Activated = "/notificationcentre/activated";
            public const string Dismissed = "/notificationcentre/dismissed";
            public const string TimedOut = "/notificationcentre/timedout";
        }
    }
}