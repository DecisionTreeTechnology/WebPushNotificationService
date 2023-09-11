using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;

public class AuthServerHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<AuthServerHostMigrationsDbContext>
{
    public AuthServerHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<AuthServerHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new AuthServerHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
