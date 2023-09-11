using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace DecisionTree.Abp.Notification.WebPush.Seed;

public class WebPushHttpApiHostDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly WebPushSampleDataSeeder _webPushSampleDataSeeder;
    private readonly ICurrentTenant _currentTenant;

    public WebPushHttpApiHostDataSeedContributor(
        WebPushSampleDataSeeder webPushSampleDataSeeder,
        ICurrentTenant currentTenant)
    {
        _webPushSampleDataSeeder = webPushSampleDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await _webPushSampleDataSeeder.SeedAsync(context!);
        }
    }
}
