using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using DecisionTree.Abp.Notification.WebPush.Shared;

namespace DecisionTree.Abp.Notification.WebPush.Web.Pages.WebPush.WebPushSubscriptions
{
    public class IndexModel : AbpPageModel
    {
        public string? EndPointFilter { get; set; }
        public string? P256dhFilter { get; set; }
        public string? AuthFilter { get; set; }
        public string? UserIdFilter { get; set; }
        public string? DeviceNameFilter { get; set; }

        private readonly IWebPushSubscriptionsAppService _webPushSubscriptionsAppService;

        public IndexModel(IWebPushSubscriptionsAppService webPushSubscriptionsAppService)
        {
            _webPushSubscriptionsAppService = webPushSubscriptionsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}