using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace DecisionTree.Abp.Notification.WebPush;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpCachingModule),
    typeof(WebPushDomainSharedModule)
)]
public class WebPushDomainModule : AbpModule
{

}
