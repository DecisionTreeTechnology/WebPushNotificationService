using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;

public class WebPushHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<WebPushHttpApiHostMigrationsDbContext>
{
    public WebPushHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<WebPushHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("WebPush"));

        return new WebPushHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
