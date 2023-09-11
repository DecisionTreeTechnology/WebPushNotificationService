using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace DecisionTree.Abp.Notification.WebPush;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(WebPushHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class WebPushConsoleApiClientModule : AbpModule
{

}
