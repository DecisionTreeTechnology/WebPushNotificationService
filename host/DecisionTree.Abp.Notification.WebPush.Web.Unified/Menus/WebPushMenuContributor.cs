using System.Threading.Tasks;
using DecisionTree.Abp.Notification.WebPush.Web.Menus;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Saas.Host.Navigation;

namespace DecisionTree.Abp.Notification.WebPush.Menus;

public class WebPushMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.User)
        {
            return Task.CompletedTask;
        }

        context.Menu.SetSubItemOrder(WebPushMenus.Prefix, 1);

        context.Menu.SetSubItemOrder(SaasHostMenuNames.GroupName, 2);

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 3;

        //Administration -> Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        //Administration -> Audit Logs
        administration.SetSubItemOrder(AbpAuditLoggingMainMenuNames.GroupName, 2);

        //Administration -> Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
