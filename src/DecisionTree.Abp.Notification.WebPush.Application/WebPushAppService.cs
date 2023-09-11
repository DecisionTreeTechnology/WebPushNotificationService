using DecisionTree.Abp.Notification.WebPush.Localization;
using Volo.Abp.Application.Services;

namespace DecisionTree.Abp.Notification.WebPush;

public abstract class WebPushAppService : ApplicationService
{
    protected WebPushAppService()
    {
        LocalizationResource = typeof(WebPushResource);
        ObjectMapperContext = typeof(WebPushApplicationModule);
    }
}
