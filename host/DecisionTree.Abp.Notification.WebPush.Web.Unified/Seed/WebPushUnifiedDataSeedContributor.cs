using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace DecisionTree.Abp.Notification.WebPush.Seed;

public class WebPushUnifiedDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly WebPushSampleIdentityDataSeeder _sampleIdentityDataSeeder;
    private readonly WebPushSampleDataSeeder _webPushSampleDataSeeder;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ICurrentTenant _currentTenant;

    public WebPushUnifiedDataSeedContributor(
        WebPushSampleIdentityDataSeeder sampleIdentityDataSeeder,
        IUnitOfWorkManager unitOfWorkManager,
        WebPushSampleDataSeeder webPushSampleDataSeeder,
        ICurrentTenant currentTenant)
    {
        _sampleIdentityDataSeeder = sampleIdentityDataSeeder;
        _unitOfWorkManager = unitOfWorkManager;
        _webPushSampleDataSeeder = webPushSampleDataSeeder;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await _unitOfWorkManager.Current.SaveChangesAsync();

        using (_currentTenant.Change(context?.TenantId))
        {
            await _sampleIdentityDataSeeder.SeedAsync(context);
            await _unitOfWorkManager.Current.SaveChangesAsync();
            await _webPushSampleDataSeeder.SeedAsync(context);
        }
    }
}
