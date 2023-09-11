using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents
{
    [RemoteService(Name = "WebPush")]
    [Area("webPush")]
    [ControllerName("WebPushContent")]
    [Route("api/web-push/web-push-contents")]
    public class WebPushContentController : AbpController, IWebPushContentsAppService
    {
        private readonly IWebPushContentsAppService _webPushContentsAppService;

        public WebPushContentController(IWebPushContentsAppService webPushContentsAppService)
        {
            _webPushContentsAppService = webPushContentsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<WebPushContentDto>> GetListAsync(GetWebPushContentsInput input)
        {
            return _webPushContentsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<WebPushContentDto> GetAsync(Guid id)
        {
            return _webPushContentsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<WebPushContentDto> CreateAsync(WebPushContentCreateDto input)
        {
            return _webPushContentsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<WebPushContentDto> UpdateAsync(Guid id, WebPushContentUpdateDto input)
        {
            return _webPushContentsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _webPushContentsAppService.DeleteAsync(id);
        }
    }
}