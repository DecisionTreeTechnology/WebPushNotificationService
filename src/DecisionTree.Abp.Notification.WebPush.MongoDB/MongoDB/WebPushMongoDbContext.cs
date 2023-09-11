using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace DecisionTree.Abp.Notification.WebPush.MongoDB;

[ConnectionStringName(WebPushDbProperties.ConnectionStringName)]
public class WebPushMongoDbContext : AbpMongoDbContext, IWebPushMongoDbContext
{
    public IMongoCollection<WebPushSubscription> WebPushSubscriptions => Collection<WebPushSubscription>();
    public IMongoCollection<WebPushNotification> WebPushNotifications => Collection<WebPushNotification>();
    public IMongoCollection<WebPushContent> WebPushContents => Collection<WebPushContent>();
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureWebPush();

        modelBuilder.Entity<WebPushContent>(b => { b.CollectionName = WebPushDbProperties.DbTablePrefix + "WebPushContents"; });

        modelBuilder.Entity<WebPushNotification>(b => { b.CollectionName = WebPushDbProperties.DbTablePrefix + "WebPushNotifications"; });

        modelBuilder.Entity<WebPushSubscription>(b => { b.CollectionName = WebPushDbProperties.DbTablePrefix + "WebPushSubscriptions"; });
    }
}