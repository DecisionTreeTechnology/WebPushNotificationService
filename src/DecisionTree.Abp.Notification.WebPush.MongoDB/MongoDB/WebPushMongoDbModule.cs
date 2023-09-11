using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace DecisionTree.Abp.Notification.WebPush.MongoDB;

[DependsOn(
    typeof(WebPushDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class WebPushMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<WebPushMongoDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, MongoQuestionRepository>();
             */
            options.AddRepository<WebPushContent, WebPushContents.MongoWebPushContentRepository>();

            options.AddRepository<WebPushNotification, WebPushNotifications.MongoWebPushNotificationRepository>();

            options.AddRepository<WebPushSubscription, WebPushSubscriptions.MongoWebPushSubscriptionRepository>();

        });
    }
}