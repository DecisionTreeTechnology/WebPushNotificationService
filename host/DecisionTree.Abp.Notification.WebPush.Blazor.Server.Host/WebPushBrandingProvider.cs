using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace DecisionTree.Abp.Notification.WebPush.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class WebPushBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "WebPush";
}
