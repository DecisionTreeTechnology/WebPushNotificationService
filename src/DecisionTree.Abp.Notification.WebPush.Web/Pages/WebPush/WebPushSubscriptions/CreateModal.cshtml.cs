using DecisionTree.Abp.Notification.WebPush.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

namespace DecisionTree.Abp.Notification.WebPush.Web.Pages.WebPush.WebPushSubscriptions
{
    public class CreateModalModel : WebPushPageModel
    {
        [BindProperty]
        public WebPushSubscriptionCreateViewModel WebPushSubscription { get; set; }

        private readonly IWebPushSubscriptionsAppService _webPushSubscriptionsAppService;

        public CreateModalModel(IWebPushSubscriptionsAppService webPushSubscriptionsAppService)
        {
            _webPushSubscriptionsAppService = webPushSubscriptionsAppService;

            WebPushSubscription = new();
        }

        public async Task OnGetAsync()
        {
            WebPushSubscription = new WebPushSubscriptionCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _webPushSubscriptionsAppService.CreateAsync(ObjectMapper.Map<WebPushSubscriptionCreateViewModel, WebPushSubscriptionCreateDto>(WebPushSubscription));
            return NoContent();
        }
    }

    public class WebPushSubscriptionCreateViewModel : WebPushSubscriptionCreateDto
    {
    }
}