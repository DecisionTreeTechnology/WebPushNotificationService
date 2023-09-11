namespace DecisionTree.Abp.Notification.WebPush;

public static class WebPushDbProperties
{
    public static string DbTablePrefix { get; set; } = "WebPush";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "WebPush";
}
