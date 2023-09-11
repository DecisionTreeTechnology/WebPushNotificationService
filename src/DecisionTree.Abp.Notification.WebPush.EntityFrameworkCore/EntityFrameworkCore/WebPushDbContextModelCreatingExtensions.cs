using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;
using Volo.Abp.EntityFrameworkCore.Modeling;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;

public static class WebPushDbContextModelCreatingExtensions
{
    public static void ConfigureWebPush(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(WebPushDbProperties.DbTablePrefix + "Questions", WebPushDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        builder.Entity<WebPushContent>(b =>
    {
        b.ToTable(WebPushDbProperties.DbTablePrefix + "Contents", WebPushDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(WebPushContent.TenantId));
        b.Property(x => x.Title).HasColumnName(nameof(WebPushContent.Title)).IsRequired();
        b.Property(x => x.Message).HasColumnName(nameof(WebPushContent.Message));
    });

        builder.Entity<WebPushNotification>(b =>
    {
        b.ToTable(WebPushDbProperties.DbTablePrefix + "Notifications", WebPushDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(WebPushNotification.TenantId));
        b.Property(x => x.UserId).HasColumnName(nameof(WebPushNotification.UserId));
        b.Property(x => x.Sent).HasColumnName(nameof(WebPushNotification.Sent));
        b.Property(x => x.SentTime).HasColumnName(nameof(WebPushNotification.SentTime));
        b.Property(x => x.FailureReason).HasColumnName(nameof(WebPushNotification.FailureReason));
        b.Property(x => x.RetryCount).HasColumnName(nameof(WebPushNotification.RetryCount));
        b.HasOne<WebPushContent>().WithMany().IsRequired().HasForeignKey(x => x.WebPushContentId).OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<WebPushSubscription>(b =>
    {
        b.ToTable(WebPushDbProperties.DbTablePrefix + "Subscriptions", WebPushDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(WebPushSubscription.TenantId));
        b.Property(x => x.EndPoint).HasColumnName(nameof(WebPushSubscription.EndPoint)).IsRequired();
        b.Property(x => x.P256dh).HasColumnName(nameof(WebPushSubscription.P256dh)).IsRequired();
        b.Property(x => x.Auth).HasColumnName(nameof(WebPushSubscription.Auth)).IsRequired();
        b.Property(x => x.UserId).HasColumnName(nameof(WebPushSubscription.UserId));
        b.Property(x => x.DeviceName).HasColumnName(nameof(WebPushSubscription.DeviceName));
    });
    }
}