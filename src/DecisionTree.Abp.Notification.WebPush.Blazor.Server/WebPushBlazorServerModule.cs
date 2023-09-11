using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace DecisionTree.Abp.Notification.WebPush.Blazor.Server;

[DependsOn(
    typeof(WebPushBlazorModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
    )]
public class WebPushBlazorServerModule : AbpModule
{

}
