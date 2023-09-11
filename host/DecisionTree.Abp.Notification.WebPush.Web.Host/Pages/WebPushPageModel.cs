using DecisionTree.Abp.Notification.WebPush.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace DecisionTree.Abp.Notification.WebPush.Pages;

public abstract class WebPushPageModel : AbpPageModel
{
    protected WebPushPageModel()
    {
        LocalizationResourceType = typeof(WebPushResource);
    }
}
