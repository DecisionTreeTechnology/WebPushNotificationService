using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DecisionTree.Abp.Notification.WebPush.EventHandlers.Etos;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;
using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace DecisionTree.Abp.Notification.WebPush.EventHandlers;

public class CreateWebPushNotificationEventHandler: IDistributedEventHandler<CreateWebPushNotificationEto>, ITransientDependency
{
    private readonly WebPushNotificationManager _notificationManager;
    private readonly WebPushContentManager _contentManager;
    private readonly WebPushSubscriptionManager _webPushSubscriptionManager;
    private readonly IBackgroundJobManager _backgroundJobManager;

    public CreateWebPushNotificationEventHandler( 
        WebPushNotificationManager notificationManager,
        WebPushContentManager contentManager,
        WebPushSubscriptionManager webPushSubscriptionManager,
        IBackgroundJobManager backgroundJobManager)
    {
        _notificationManager = notificationManager;
        _contentManager = contentManager;
        _webPushSubscriptionManager = webPushSubscriptionManager;
        _backgroundJobManager = backgroundJobManager;
    }
    
    public async Task HandleEventAsync(CreateWebPushNotificationEto eventData)
    {
        var extraProperties = new Dictionary<string, string>();
        var content = await _contentManager.CreateAsync(eventData.Title, eventData.Body ?? "", extraProperties);
        var notifications = new List<WebPushNotification>();

        foreach (var userId in eventData.UserIds)
        {
            var subscriptions = await _webPushSubscriptionManager.GetByUserIdAsync(userId);
            if (subscriptions.Any())
            {
                notifications.Add(await _notificationManager.CreateAsync(userId, content.Id));
                
                foreach (var notification in notifications)
                {
                    await _backgroundJobManager.EnqueueAsync(
                        new WebPushNotificationSendingJobArgs(notification.TenantId, notification.Id));
                }
            }
        }

    }
}