using DecisionTree.Abp.Notification.WebPush.Permissions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using DecisionTree.Abp.Notification.WebPush.Localization;
using Volo.Abp.UI.Navigation;

namespace DecisionTree.Abp.Notification.WebPush.Blazor.Menus;

public class WebPushMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }

        var moduleMenu = AddModuleMenuItem(context);
        AddMenuItemWebPushSubscriptions(context, moduleMenu);
    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        var l = context.GetLocalizer<WebPushResource>();

        context.Menu.AddItem(new ApplicationMenuItem(WebPushMenus.Prefix, displayName: "Sample Page", "/WebPush", icon: "fa fa-globe"));

        await Task.CompletedTask;
    }
    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            WebPushMenus.Prefix,
            context.GetLocalizer<WebPushResource>()["Menu:WebPush"],
            icon: "fa fa-folder"
        );

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