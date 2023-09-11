using DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace DecisionTree.Abp.Notification.WebPush;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(WebPushEntityFrameworkCoreTestModule)
    )]
public class WebPushDomainTestModule : AbpModule
{

}
