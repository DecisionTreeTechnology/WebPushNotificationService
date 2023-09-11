using Microsoft.EntityFrameworkCore;
using DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;

namespace DecisionTree.Abp.Notification.WebPush.Blazor.Server.Host.EntityFrameworkCore;

public class UnifiedDbContext : AbpDbContext<UnifiedDbContext>
{
    public UnifiedDbContext(DbContextOptions<UnifiedDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentityPro();
        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureSaas();
        modelBuilder.ConfigureBlobStoring();
        modelBuilder.ConfigureWebPush();
    }
}
