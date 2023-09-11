using Volo.Abp.Reflection;

namespace DecisionTree.Abp.Notification.WebPush.Permissions;

public class WebPushPermissions
{
    public const string GroupName = "WebPush";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(WebPushPermissions));
    }

    public static class WebPushContents
    {
        public const string Default = GroupName + ".WebPushContents";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class WebPushNotifications
    {
        public const string Default = GroupName + ".WebPushNotifications";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class WebPushSubscriptions
    {
        public const string Default = GroupName + ".WebPushSubscriptions";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Subscribe = Default + ".Subscribe";
    }
}