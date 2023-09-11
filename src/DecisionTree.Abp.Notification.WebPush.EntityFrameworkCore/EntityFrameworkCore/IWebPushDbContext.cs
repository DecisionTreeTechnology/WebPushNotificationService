using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;

[ConnectionStringName(WebPushDbProperties.ConnectionStringName)]
public interface IWebPushDbContext : IEfCoreDbContext
{
    DbSet<WebPushSubscription> WebPushSubscriptions { get; set; }
    DbSet<WebPushNotification> WebPushNotifications { get; set; }
    DbSet<WebPushContent> WebPushContents { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}