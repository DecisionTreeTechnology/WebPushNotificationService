using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using Volo.Abp.Content;
using DecisionTree.Abp.Notification.WebPush.Shared;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions
{
    [RemoteService(Name = "WebPush")]
    [Area("webPush")]
    [ControllerName("WebPushSubscription")]
    [Route("api/web-push/web-push-subscriptions")]
    public class WebPushSubscriptionController : AbpController, IWebPushSubscriptionsAppService
    {
        private readonly IWebPushSubscriptionsAppService _webPushSubscriptionsAppService;

        public WebPushSubscriptionController(IWebPushSubscriptionsAppService webPushSubscriptionsAppService)
        {
            _webPushSubscriptionsAppService = webPushSubscriptionsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<WebPushSubscriptionDto>> GetListAsync(GetWebPushSubscriptionsInput input)
        {
            return _webPushSubscriptionsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<WebPushSubscriptionDto> GetAsync(Guid id)
        {
            return _webPushSubscriptionsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<WebPushSubscriptionDto> CreateAsync(WebPushSubscriptionCreateDto input)
        {
            return _webPushSubscriptionsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<WebPushSubscriptionDto> UpdateAsync(Guid id, WebPushSubscriptionUpdateDto input)
        {
            return _webPushSubscriptionsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _webPushSubscriptionsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(WebPushSubscriptionExcelDownloadDto input)
        {
            return _webPushSubscriptionsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _webPushSubscriptionsAppService.GetDownloadTokenAsync();
        }
        
        [HttpPost]
        [Route("subscribe")]
        public virtual Task<WebPushSubscriptionDto> Subscribe(WebPushSubscriptionCreateDto input)
        {
            return _webPushSubscriptionsAppService.Subscribe(input);
        }

        [HttpPost]
        [Route("unsubscribe")]
        public Task Unsubscribe(WebPushSubscriptionUnsubscribeDto webPushSubscriptionUnsubscribeDto)
        {
            return _webPushSubscriptionsAppService.Unsubscribe(webPushSubscriptionUnsubscribeDto);
        }

        [HttpPost]
        [Route("unsubscribe-by-user")]
        public Task UnsubscribeByUserAsync(Guid userId)
        {
            return _webPushSubscriptionsAppService.UnsubscribeByUserAsync(userId);
        }
        
        [HttpGet]
        [Route(("vapid-public-key"))]
        public Task<string> GetVapidPublicKeyAsync()
        {
            return _webPushSubscriptionsAppService.GetVapidPublicKeyAsync();
        }
    }
}