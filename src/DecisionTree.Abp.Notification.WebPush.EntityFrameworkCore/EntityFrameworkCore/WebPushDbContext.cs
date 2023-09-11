using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;

[ConnectionStringName(WebPushDbProperties.ConnectionStringName)]
public class WebPushDbContext : AbpDbContext<WebPushDbContext>, IWebPushDbContext
{
    public DbSet<WebPushSubscription> WebPushSubscriptions { get; set; }
    public DbSet<WebPushNotification> WebPushNotifications { get; set; }
    public DbSet<WebPushContent> WebPushContents { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public WebPushDbContext(DbContextOptions<WebPushDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureWebPush();
    }
}