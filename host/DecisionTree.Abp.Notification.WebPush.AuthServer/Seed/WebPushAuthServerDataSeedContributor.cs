using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace DecisionTree.Abp.Notification.WebPush.Seed;

public class WebPushAuthServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly WebPushSampleIdentityDataSeeder _webPushSampleIdentityDataSeeder;
    private readonly WebPushAuthServerDataSeeder _webPushAuthServerDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public WebPushAuthServerDataSeedContributor(
        WebPushAuthServerDataSeeder webPushAuthServerDataSeeder,
        WebPushSampleIdentityDataSeeder webPushSampleIdentityDataSeeder,
        ICurrentTenant currentTenant)
    {
        _webPushAuthServerDataSeeder = webPushAuthServerDataSeeder;
        _webPushSampleIdentityDataSeeder = webPushSampleIdentityDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _webPushSampleIdentityDataSeeder.SeedAsync(context!);
            await _webPushAuthServerDataSeeder.SeedAsync(context!);
        }
    }
}
