using System;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

[Serializable]
public class WebPushSubscriptionExcelDownloadTokenCacheItem
{
    public string Token { get; set; }
}