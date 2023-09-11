using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace DecisionTree.Abp.Notification.WebPush;

[DependsOn(
    typeof(WebPushApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class WebPushHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(WebPushApplicationContractsModule).Assembly,
            WebPushRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WebPushHttpApiClientModule>();
        });
    }
}
