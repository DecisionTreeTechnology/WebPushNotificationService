namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications
{
    public static class WebPushNotificationConsts
    {
        private const string DefaultSorting = "{0}UserId asc,{0}Sent asc,{0}SentTime asc,{0}FailureReason asc,{0}RetryCount asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "WebPushNotification." : string.Empty);
        }

    }
}