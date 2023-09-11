using Localization.Resources.AbpUi;
using DecisionTree.Abp.Notification.WebPush.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace DecisionTree.Abp.Notification.WebPush;

[DependsOn(
    typeof(WebPushApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class WebPushHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(WebPushHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<WebPushResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
