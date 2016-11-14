namespace NotificationCentre.Transports
{
    internal static class TransportConstants
    {
        public static class Topics
        {
            public const string Post = "notification-centre/post";
            public const string Activated = "notification-centre/activated";
            public const string Dismissed = "notification-centre/dismissed";
            public const string TimedOut = "notification-centre/timedout";
        }
    }
}