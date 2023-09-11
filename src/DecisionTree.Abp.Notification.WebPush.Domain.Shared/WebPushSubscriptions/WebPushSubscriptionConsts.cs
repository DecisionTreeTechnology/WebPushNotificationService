namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions
{
    public static class WebPushSubscriptionConsts
    {
        private const string DefaultSorting = "{0}EndPoint asc,{0}P256dh asc,{0}Auth asc,{0}UserId asc,{0}DeviceName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "WebPushSubscription." : string.Empty);
        }

    }
}