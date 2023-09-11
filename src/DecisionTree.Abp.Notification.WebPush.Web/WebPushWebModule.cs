using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using DecisionTree.Abp.Notification.WebPush.Localization;
using DecisionTree.Abp.Notification.WebPush.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using DecisionTree.Abp.Notification.WebPush.Permissions;

namespace DecisionTree.Abp.Notification.WebPush.Web;

[DependsOn(
    typeof(WebPushApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class WebPushWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(WebPushResource), typeof(WebPushWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(WebPushWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new WebPushMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WebPushWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<WebPushWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<WebPushWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            //Configure authorization.
            options.Conventions.AuthorizePage("/WebPushSubscriptions/Index", WebPushPermissions.WebPushSubscriptions.Default);
        });
    }
}