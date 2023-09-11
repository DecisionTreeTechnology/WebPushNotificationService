using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace DecisionTree.Abp.Notification.WebPush;

[DependsOn(
    typeof(WebPushDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class WebPushApplicationContractsModule : AbpModule
{

}
