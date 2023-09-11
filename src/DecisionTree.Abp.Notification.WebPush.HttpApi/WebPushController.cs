using DecisionTree.Abp.Notification.WebPush.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace DecisionTree.Abp.Notification.WebPush;

public abstract class WebPushController : AbpControllerBase
{
    protected WebPushController()
    {
        LocalizationResource = typeof(WebPushResource);
    }
}
