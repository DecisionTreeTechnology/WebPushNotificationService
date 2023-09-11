using System;
using Volo.Abp.MultiTenancy;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

public class WebPushNotificationSendingJobArgs : IMultiTenant
{
    public WebPushNotificationSendingJobArgs(Guid? tenantId, Guid webPushNotificationId)
    {
        TenantId = tenantId;
        WebPushNotificationId = webPushNotificationId;
    }

    public Guid? TenantId { get; }
    
    public Guid WebPushNotificationId { get; set; }
    
}