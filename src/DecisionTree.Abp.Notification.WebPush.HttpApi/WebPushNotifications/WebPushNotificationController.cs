using DecisionTree.Abp.Notification.WebPush.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications
{
    [RemoteService(Name = "WebPush")]
    [Area("webPush")]
    [ControllerName("WebPushNotification")]
    [Route("api/web-push/web-push-notifications")]
    public class WebPushNotificationController : AbpController, IWebPushNotificationsAppService
    {
        private readonly IWebPushNotificationsAppService _webPushNotificationsAppService;

        public WebPushNotificationController(IWebPushNotificationsAppService webPushNotificationsAppService)
        {
            _webPushNotificationsAppService = webPushNotificationsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<WebPushNotificationWithNavigationPropertiesDto>> GetListAsync(GetWebPushNotificationsInput input)
        {
            return _webPushNotificationsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<WebPushNotificationWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _webPushNotificationsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<WebPushNotificationDto> GetAsync(Guid id)
        {
            return _webPushNotificationsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("web-push-content-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetWebPushContentLookupAsync(LookupRequestDto input)
        {
            return _webPushNotificationsAppService.GetWebPushContentLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<WebPushNotificationDto> CreateAsync(WebPushNotificationCreateDto input)
        {
            return _webPushNotificationsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<WebPushNotificationDto> UpdateAsync(Guid id, WebPushNotificationUpdateDto input)
        {
            return _webPushNotificationsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _webPushNotificationsAppService.DeleteAsync(id);
        }
    }
}