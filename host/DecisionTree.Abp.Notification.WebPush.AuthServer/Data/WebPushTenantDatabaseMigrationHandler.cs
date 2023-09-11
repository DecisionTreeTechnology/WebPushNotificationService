using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace DecisionTree.Abp.Notification.WebPush.Data;

public class WebPushTenantDatabaseMigrationHandler :
    IDistributedEventHandler<TenantCreatedEto>,
    IDistributedEventHandler<TenantConnectionStringUpdatedEto>,
    IDistributedEventHandler<ApplyDatabaseMigrationsEto>,
    ITransientDependency
{
    private readonly IDbContextProvider<AuthServerHostMigrationsDbContext> _dbContextProvider;
    private readonly ICurrentTenant _currentTenant;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IDataSeeder _dataSeeder;
    private readonly ITenantStore _tenantStore;
    private readonly ILogger<WebPushTenantDatabaseMigrationHandler> _logger;

    public WebPushTenantDatabaseMigrationHandler(
        IDbContextProvider<AuthServerHostMigrationsDbContext> dbContextProvider,
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        IDataSeeder dataSeeder,
        ITenantStore tenantStore,
        ILogger<WebPushTenantDatabaseMigrationHandler> logger)
    {
        _dbContextProvider = dbContextProvider;
        _currentTenant = currentTenant;
        _unitOfWorkManager = unitOfWorkManager;
        _dataSeeder = dataSeeder;
        _tenantStore = tenantStore;
        _logger = logger;
    }

    public async Task HandleEventAsync(TenantCreatedEto eventData)
    {
        await MigrateAndSeedForTenantAsync(eventData.Id);
    }

    public async Task HandleEventAsync(TenantConnectionStringUpdatedEto eventData)
    {
        if (eventData.ConnectionStringName != ConnectionStrings.DefaultConnectionStringName ||
            eventData.NewValue.IsNullOrWhiteSpace())
        {
            return;
        }

        await MigrateAndSeedForTenantAsync(eventData.Id);

        /* You may want to move your data from the old database to the new database!
         * It is up to you. If you don't make it, new database will be empty
         * (and tenant's admin password is reset to 1q2w3E*).
         */
    }

    public async Task HandleEventAsync(ApplyDatabaseMigrationsEto eventData)
    {
        if (eventData.TenantId == null)
        {
            return;
        }

        await MigrateAndSeedForTenantAsync(eventData.TenantId.Value);
    }

    private async Task MigrateAndSeedForTenantAsync(Guid tenantId)
    {
        try
        {
            using (_currentTenant.Change(tenantId))
            {
                // Create database tables if needed
                using (var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: false))
                {
                    var tenantConfiguration = await _tenantStore.FindAsync(tenantId);
                    if (tenantConfiguration?.ConnectionStrings != null &&
                        !tenantConfiguration.ConnectionStrings.Default.IsNullOrWhiteSpace())
                    {

                        await (await _dbContextProvider.GetDbContextAsync()).Database.MigrateAsync();
                    }

                    await uow.CompleteAsync();
                }

                // Seed data
                using (var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
                {
                    await _dataSeeder.SeedAsync(new DataSeedContext(tenantId));

                    await uow.CompleteAsync();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogException(ex);
        }
    }
}
