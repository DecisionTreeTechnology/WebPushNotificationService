using DecisionTree.Abp.Notification.WebPush.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

namespace DecisionTree.Abp.Notification.WebPush.Web.Pages.WebPush.WebPushSubscriptions
{
    public class EditModalModel : WebPushPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public WebPushSubscriptionUpdateViewModel WebPushSubscription { get; set; }

        private readonly IWebPushSubscriptionsAppService _webPushSubscriptionsAppService;

        public EditModalModel(IWebPushSubscriptionsAppService webPushSubscriptionsAppService)
        {
            _webPushSubscriptionsAppService = webPushSubscriptionsAppService;

            WebPushSubscription = new();
        }

        public async Task OnGetAsync()
        {
            var webPushSubscription = await _webPushSubscriptionsAppService.GetAsync(Id);
            WebPushSubscription = ObjectMapper.Map<WebPushSubscriptionDto, WebPushSubscriptionUpdateViewModel>(webPushSubscription);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _webPushSubscriptionsAppService.UpdateAsync(Id, ObjectMapper.Map<WebPushSubscriptionUpdateViewModel, WebPushSubscriptionUpdateDto>(WebPushSubscription));
            return NoContent();
        }
    }

    public class WebPushSubscriptionUpdateViewModel : WebPushSubscriptionUpdateDto
    {
    }
}