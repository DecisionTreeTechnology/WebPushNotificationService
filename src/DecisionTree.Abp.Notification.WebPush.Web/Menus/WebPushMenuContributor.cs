using DecisionTree.Abp.Notification.WebPush.Permissions;
using DecisionTree.Abp.Notification.WebPush.Localization;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Volo.Abp.Authorization.Permissions;

namespace DecisionTree.Abp.Notification.WebPush.Web.Menus;

public class WebPushMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return;
        }

        var moduleMenu = AddModuleMenuItem(context); //Do not delete `moduleMenu` variable as it will be used by ABP Suite!

        AddMenuItemWebPushSubscriptions(context, moduleMenu);
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            WebPushMenus.Prefix,
            displayName: "WebPush",
            "~/WebPush",
            icon: "fa fa-globe");

        //Add main menu items.
        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
    private static void AddMenuItemWebPushSubscriptions(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.WebPushMenus.WebPushSubscriptions,
                context.GetLocalizer<WebPushResource>()["Menu:WebPushSubscriptions"],
                "/WebPush/WebPushSubscriptions",
                icon: "fa fa-file-alt",
                requiredPermissionName: WebPushPermissions.WebPushSubscriptions.Default
            )
        );
    }
}