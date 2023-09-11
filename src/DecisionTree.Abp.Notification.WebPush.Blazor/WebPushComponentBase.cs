using DecisionTree.Abp.Notification.WebPush.Localization;
using Volo.Abp.AspNetCore.Components;

namespace DecisionTree.Abp.Notification.WebPush.Blazor;

public abstract class WebPushComponentBase : AbpComponentBase
{
    protected WebPushComponentBase()
    {
        LocalizationResource = typeof(WebPushResource);
    }
}
