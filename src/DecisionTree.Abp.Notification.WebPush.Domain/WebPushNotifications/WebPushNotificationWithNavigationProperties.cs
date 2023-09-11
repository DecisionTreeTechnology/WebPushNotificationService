using DecisionTree.Abp.Notification.WebPush.WebPushContents;

using System;
using System.Collections.Generic;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

public class WebPushNotificationWithNavigationProperties
{
    public WebPushNotification WebPushNotification { get; set; }

    public WebPushContent WebPushContent { get; set; }
    
}