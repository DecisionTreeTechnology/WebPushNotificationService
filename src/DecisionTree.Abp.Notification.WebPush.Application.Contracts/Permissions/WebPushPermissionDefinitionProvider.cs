using DecisionTree.Abp.Notification.WebPush.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DecisionTree.Abp.Notification.WebPush.Permissions;

public class WebPushPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WebPushPermissions.GroupName, L("Permission:WebPush"));

        var webPushContentPermission = myGroup.AddPermission(WebPushPermissions.WebPushContents.Default, L("Permission:WebPushContents"));
        webPushContentPermission.AddChild(WebPushPermissions.WebPushContents.Create, L("Permission:Create"));
        webPushContentPermission.AddChild(WebPushPermissions.WebPushContents.Edit, L("Permission:Edit"));
        webPushContentPermission.AddChild(WebPushPermissions.WebPushContents.Delete, L("Permission:Delete"));

        var webPushNotificationPermission = myGroup.AddPermission(WebPushPermissions.WebPushNotifications.Default, L("Permission:WebPushNotifications"));
        webPushNotificationPermission.AddChild(WebPushPermissions.WebPushNotifications.Create, L("Permission:Create"));
        webPushNotificationPermission.AddChild(WebPushPermissions.WebPushNotifications.Edit, L("Permission:Edit"));
        webPushNotificationPermission.AddChild(WebPushPermissions.WebPushNotifications.Delete, L("Permission:Delete"));

        var webPushSubscriptionPermission = myGroup.AddPermission(WebPushPermissions.WebPushSubscriptions.Default, L("Permission:WebPushSubscriptions"));
        webPushSubscriptionPermission.AddChild(WebPushPermissions.WebPushSubscriptions.Create, L("Permission:Create"));
        webPushSubscriptionPermission.AddChild(WebPushPermissions.WebPushSubscriptions.Edit, L("Permission:Edit"));
        webPushSubscriptionPermission.AddChild(WebPushPermissions.WebPushSubscriptions.Delete, L("Permission:Delete"));
        webPushSubscriptionPermission.AddChild(WebPushPermissions.WebPushSubscriptions.Subscribe, L("Permission:Subscribe"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WebPushResource>(name);
    }
}