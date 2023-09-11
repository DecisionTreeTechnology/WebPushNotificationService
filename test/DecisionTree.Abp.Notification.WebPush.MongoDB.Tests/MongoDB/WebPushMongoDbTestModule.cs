using System;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace DecisionTree.Abp.Notification.WebPush.MongoDB;

[DependsOn(
    typeof(WebPushTestBaseModule),
    typeof(WebPushMongoDbModule)
    )]
public class WebPushMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
