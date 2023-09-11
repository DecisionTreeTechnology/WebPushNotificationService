using DecisionTree.Abp.Notification.WebPush.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

public interface IWebPushNotificationsAppService : IApplicationService
{
    Task<PagedResultDto<WebPushNotificationWithNavigationPropertiesDto>> GetListAsync(GetWebPushNotificationsInput input);

    Task<WebPushNotificationWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

    Task<WebPushNotificationDto> GetAsync(Guid id);

    Task<PagedResultDto<LookupDto<Guid>>> GetWebPushContentLookupAsync(LookupRequestDto input);

    Task DeleteAsync(Guid id);

    Task<WebPushNotificationDto> CreateAsync(WebPushNotificationCreateDto input);

    Task<WebPushNotificationDto> UpdateAsync(Guid id, WebPushNotificationUpdateDto input);
}