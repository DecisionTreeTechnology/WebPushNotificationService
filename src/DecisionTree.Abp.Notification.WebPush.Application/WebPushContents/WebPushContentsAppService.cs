using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using DecisionTree.Abp.Notification.WebPush.Permissions;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents;

[Authorize(WebPushPermissions.WebPushContents.Default)]
public class WebPushContentsAppService : ApplicationService, IWebPushContentsAppService
{

    private readonly IWebPushContentRepository _webPushContentRepository;
    private readonly WebPushContentManager _webPushContentManager;

    public WebPushContentsAppService(IWebPushContentRepository webPushContentRepository, WebPushContentManager webPushContentManager)
    {

        _webPushContentRepository = webPushContentRepository;
        _webPushContentManager = webPushContentManager;
    }

    public virtual async Task<PagedResultDto<WebPushContentDto>> GetListAsync(GetWebPushContentsInput input)
    {
        var totalCount = await _webPushContentRepository.GetCountAsync(input.FilterText, input.Title, input.Message);
        var items = await _webPushContentRepository.GetListAsync(input.FilterText, input.Title, input.Message, input.Sorting, input.MaxResultCount, input.SkipCount);

        return new PagedResultDto<WebPushContentDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<WebPushContent>, List<WebPushContentDto>>(items)
        };
    }

    public virtual async Task<WebPushContentDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<WebPushContent, WebPushContentDto>(await _webPushContentRepository.GetAsync(id));
    }

    [Authorize(WebPushPermissions.WebPushContents.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _webPushContentRepository.DeleteAsync(id);
    }

    [Authorize(WebPushPermissions.WebPushContents.Create)]
    public virtual async Task<WebPushContentDto> CreateAsync(WebPushContentCreateDto input)
    {

        var webPushContent = await _webPushContentManager.CreateAsync(
            input.Title, input.Message, null
        );

        return ObjectMapper.Map<WebPushContent, WebPushContentDto>(webPushContent);
    }

    [Authorize(WebPushPermissions.WebPushContents.Edit)]
    public virtual async Task<WebPushContentDto> UpdateAsync(Guid id, WebPushContentUpdateDto input)
    {

        var webPushContent = await _webPushContentManager.UpdateAsync(
            id,
            input.Title, input.Message, input.ConcurrencyStamp
        );

        return ObjectMapper.Map<WebPushContent, WebPushContentDto>(webPushContent);
    }
}