using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;

[DependsOn(
    typeof(WebPushDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class WebPushEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<WebPushDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<WebPushContent, WebPushContents.EfCoreWebPushContentRepository>();

            options.AddRepository<WebPushNotification, WebPushNotifications.EfCoreWebPushNotificationRepository>();

            options.AddRepository<WebPushSubscription, WebPushSubscriptions.EfCoreWebPushSubscriptionRepository>();

        });
    }
}