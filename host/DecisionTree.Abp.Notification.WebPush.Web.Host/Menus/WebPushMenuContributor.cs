using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DecisionTree.Abp.Notification.WebPush.Localization;
using DecisionTree.Abp.Notification.WebPush.Web.Menus;
using Volo.Abp.Account.Localization;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Saas.Host.Navigation;

namespace DecisionTree.Abp.Notification.WebPush.Menus;

public class WebPushWebHostMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public WebPushWebHostMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
            return;
        }

        await ConfigureMainMenuAsync(context);
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<WebPushResource>();
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountStringLocalizer["MyAccount"], $"{authServerUrl.EnsureEndsWith('/')}Account/Manage", icon: "fa fa-cog", order: 1000, null, "_blank"));
        context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", l["Logout"], url: "~/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000));

        return Task.CompletedTask;
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        context.Menu.SetSubItemOrder(WebPushMenus.Prefix, 1);

        context.Menu.SetSubItemOrder(SaasHostMenuNames.GroupName, 2);

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 3;

        //Administration -> Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        //Administration -> Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 2);

        return Task.CompletedTask;
    }
}
