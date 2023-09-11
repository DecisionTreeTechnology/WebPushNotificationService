using DecisionTree.Abp.Notification.WebPush.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace DecisionTree.Abp.Notification.WebPush.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class WebPushPageModel : AbpPageModel
{
    protected WebPushPageModel()
    {
        LocalizationResourceType = typeof(WebPushResource);
        ObjectMapperContext = typeof(WebPushWebModule);
    }
}
