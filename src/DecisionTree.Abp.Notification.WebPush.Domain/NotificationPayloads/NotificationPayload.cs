using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using Newtonsoft.Json;

namespace DecisionTree.Abp.Notification.WebPush.NotificationPayloads;

public class NotificationPayload
{
    [JsonProperty("notification")]
    public WebPushContent Notification { get; set; }
    
    public NotificationPayload (WebPushContent webPushContent)
    {
        Notification = webPushContent;
    }
}