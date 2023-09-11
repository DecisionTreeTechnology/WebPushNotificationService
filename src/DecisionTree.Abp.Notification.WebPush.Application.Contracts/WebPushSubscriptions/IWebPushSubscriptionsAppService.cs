using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DecisionTree.Abp.Notification.WebPush.Shared;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

public interface IWebPushSubscriptionsAppService : IApplicationService
{
    Task<PagedResultDto<WebPushSubscriptionDto>> GetListAsync(GetWebPushSubscriptionsInput input);

    Task<WebPushSubscriptionDto> GetAsync(Guid id);

    Task DeleteAsync(Guid id);

    Task<WebPushSubscriptionDto> CreateAsync(WebPushSubscriptionCreateDto input);

    Task<WebPushSubscriptionDto> UpdateAsync(Guid id, WebPushSubscriptionUpdateDto input);

    Task<IRemoteStreamContent> GetListAsExcelFileAsync(WebPushSubscriptionExcelDownloadDto input);

    Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    
    Task<WebPushSubscriptionDto> Subscribe(WebPushSubscriptionCreateDto input);
        
    Task Unsubscribe(WebPushSubscriptionUnsubscribeDto input);

    Task UnsubscribeByUserAsync(Guid clientId);

    Task<string> GetVapidPublicKeyAsync();
}