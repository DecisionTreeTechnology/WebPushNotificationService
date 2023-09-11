using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace DecisionTree.Abp.Notification.WebPush.Blazor.WebAssembly;

[DependsOn(
    typeof(WebPushBlazorModule),
    typeof(WebPushHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
)]
public class WebPushBlazorWebAssemblyModule : AbpModule
{

}
