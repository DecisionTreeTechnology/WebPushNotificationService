using Volo.Abp.Modularity;

namespace DecisionTree.Abp.Notification.WebPush;

[DependsOn(
    typeof(WebPushApplicationModule),
    typeof(WebPushDomainTestModule)
    )]
public class WebPushApplicationTestModule : AbpModule
{

}
