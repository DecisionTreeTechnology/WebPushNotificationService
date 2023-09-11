using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DecisionTree.Abp.Notification.WebPush.MongoDB;

[ConnectionStringName(WebPushDbProperties.ConnectionStringName)]
public interface IWebPushMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<WebPushSubscription> WebPushSubscriptions { get; }
    IMongoCollection<WebPushNotification> WebPushNotifications { get; }
    IMongoCollection<WebPushContent> WebPushContents { get; }
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}