using Volo.Abp;
using Volo.Abp.MongoDB;

namespace DecisionTree.Abp.Notification.WebPush.MongoDB;

public static class WebPushMongoDbContextExtensions
{
    public static void ConfigureWebPush(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
