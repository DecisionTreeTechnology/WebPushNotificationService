using DecisionTree.Abp.Notification.WebPush.WebPushContents;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

public class WebPushNotificationWithNavigationPropertiesDto
{
    public WebPushNotificationDto WebPushNotification { get; set; }

    public WebPushContentDto WebPushContent { get; set; }

}