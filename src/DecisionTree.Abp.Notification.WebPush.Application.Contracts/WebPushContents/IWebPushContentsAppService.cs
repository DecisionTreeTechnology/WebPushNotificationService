using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents;

public interface IWebPushContentsAppService : IApplicationService
{
    Task<PagedResultDto<WebPushContentDto>> GetListAsync(GetWebPushContentsInput input);

    Task<WebPushContentDto> GetAsync(Guid id);

    Task DeleteAsync(Guid id);

    Task<WebPushContentDto> CreateAsync(WebPushContentCreateDto input);

    Task<WebPushContentDto> UpdateAsync(Guid id, WebPushContentUpdateDto input);
}