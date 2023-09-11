using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace DecisionTree.Abp.Notification.WebPush;

[Dependency(ReplaceServices = true)]
public class WebPushBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "WebPush";
}
