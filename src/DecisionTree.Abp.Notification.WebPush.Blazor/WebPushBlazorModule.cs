using Microsoft.Extensions.DependencyInjection;
using DecisionTree.Abp.Notification.WebPush.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace DecisionTree.Abp.Notification.WebPush.Blazor;

[DependsOn(
    typeof(WebPushApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class WebPushBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<WebPushBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<WebPushBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new WebPushMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(WebPushBlazorModule).Assembly);
        });
    }
}
