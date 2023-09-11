namespace DecisionTree.Abp.Notification.WebPush.WebPushContents
{
    public static class WebPushContentConsts
    {
        private const string DefaultSorting = "{0}title asc,{0}message asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "WebPushContent." : string.Empty);
        }

    }
}