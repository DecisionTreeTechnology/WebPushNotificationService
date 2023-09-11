using DecisionTree.Abp.Notification.WebPush.Shared;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using DecisionTree.Abp.Notification.WebPush.Permissions;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

[Authorize(WebPushPermissions.WebPushNotifications.Default)]
public class WebPushNotificationsAppService : ApplicationService, IWebPushNotificationsAppService
{

    private readonly IWebPushNotificationRepository _webPushNotificationRepository;
    private readonly WebPushNotificationManager _webPushNotificationManager;
    private readonly IRepository<WebPushContent, Guid> _webPushContentRepository;

    public WebPushNotificationsAppService(IWebPushNotificationRepository webPushNotificationRepository, WebPushNotificationManager webPushNotificationManager, IRepository<WebPushContent, Guid> webPushContentRepository)
    {

        _webPushNotificationRepository = webPushNotificationRepository;
        _webPushNotificationManager = webPushNotificationManager; _webPushContentRepository = webPushContentRepository;
    }

    public virtual async Task<PagedResultDto<WebPushNotificationWithNavigationPropertiesDto>> GetListAsync(GetWebPushNotificationsInput input)
    {
        var totalCount = await _webPushNotificationRepository.GetCountAsync(input.FilterText, input.UserId, input.Sent, input.SentTimeMin, input.SentTimeMax, input.FailureReason, input.RetryCountMin, input.RetryCountMax, input.WebPushContentId);
        var items = await _webPushNotificationRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.Sent, input.SentTimeMin, input.SentTimeMax, input.FailureReason, input.RetryCountMin, input.RetryCountMax, input.WebPushContentId, input.Sorting, input.MaxResultCount, input.SkipCount);

        return new PagedResultDto<WebPushNotificationWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<WebPushNotificationWithNavigationProperties>, List<WebPushNotificationWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<WebPushNotificationWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<WebPushNotificationWithNavigationProperties, WebPushNotificationWithNavigationPropertiesDto>
            (await _webPushNotificationRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<WebPushNotificationDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<WebPushNotification, WebPushNotificationDto>(await _webPushNotificationRepository.GetAsync(id));
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetWebPushContentLookupAsync(LookupRequestDto input)
    {
        var query = (await _webPushContentRepository.GetQueryableAsync())
            .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                x => x.Title != null &&
                     x.Title.Contains(input.Filter));

        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<WebPushContent>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<WebPushContent>, List<LookupDto<Guid>>>(lookupData)
        };
    }

    [Authorize(WebPushPermissions.WebPushNotifications.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _webPushNotificationRepository.DeleteAsync(id);
    }

    [Authorize(WebPushPermissions.WebPushNotifications.Create)]
    public virtual async Task<WebPushNotificationDto> CreateAsync(WebPushNotificationCreateDto input)
    {
        if (input.WebPushContentId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["WebPushContent"]]);
        }

        var webPushNotification = await _webPushNotificationManager.CreateAsync(
            input.UserId,input.WebPushContentId
        );

        return ObjectMapper.Map<WebPushNotification, WebPushNotificationDto>(webPushNotification);
    }

    [Authorize(WebPushPermissions.WebPushNotifications.Edit)]
    public virtual async Task<WebPushNotificationDto> UpdateAsync(Guid id, WebPushNotificationUpdateDto input)
    {
        if (input.WebPushContentId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["WebPushContent"]]);
        }

        var webPushNotification = await _webPushNotificationManager.UpdateAsync(
            id,
            input.WebPushContentId, input.UserId, input.Sent, input.FailureReason, input.RetryCount, input.SentTime, input.ConcurrencyStamp
        );

        return ObjectMapper.Map<WebPushNotification, WebPushNotificationDto>(webPushNotification);
    }
}