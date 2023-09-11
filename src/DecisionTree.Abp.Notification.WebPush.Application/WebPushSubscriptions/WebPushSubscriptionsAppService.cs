using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using DecisionTree.Abp.Notification.WebPush.Permissions;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using DecisionTree.Abp.Notification.WebPush.Shared;
using Volo.Abp.EventBus.Distributed;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

[Authorize(WebPushPermissions.WebPushSubscriptions.Default)]
public class WebPushSubscriptionsAppService : ApplicationService, IWebPushSubscriptionsAppService
{
    private readonly IDistributedCache<WebPushSubscriptionExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
    private readonly IWebPushSubscriptionRepository _webPushSubscriptionRepository;
    private readonly WebPushSubscriptionManager _webPushSubscriptionManager;
    private readonly IConfiguration _configuration;
    private readonly IDistributedEventBus _distributedEventBus;

    public WebPushSubscriptionsAppService(IWebPushSubscriptionRepository webPushSubscriptionRepository, WebPushSubscriptionManager webPushSubscriptionManager, IDistributedCache<WebPushSubscriptionExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IConfiguration configuration, IDistributedEventBus distributedEventBus)
    {
        _excelDownloadTokenCache = excelDownloadTokenCache;
        _webPushSubscriptionRepository = webPushSubscriptionRepository;
        _webPushSubscriptionManager = webPushSubscriptionManager;
        _configuration = configuration;
        _distributedEventBus = distributedEventBus;
    }

    public virtual async Task<PagedResultDto<WebPushSubscriptionDto>> GetListAsync(GetWebPushSubscriptionsInput input)
    {
        var totalCount = await _webPushSubscriptionRepository.GetCountAsync(input.FilterText, input.EndPoint, input.P256dh, input.Auth, input.UserId, input.DeviceName);
        var items = await _webPushSubscriptionRepository.GetListAsync(input.FilterText, input.EndPoint, input.P256dh, input.Auth, input.UserId, input.DeviceName, input.Sorting, input.MaxResultCount, input.SkipCount);

        return new PagedResultDto<WebPushSubscriptionDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<WebPushSubscription>, List<WebPushSubscriptionDto>>(items)
        };
    }

    public virtual async Task<WebPushSubscriptionDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<WebPushSubscription, WebPushSubscriptionDto>(await _webPushSubscriptionRepository.GetAsync(id));
    }

    [Authorize(WebPushPermissions.WebPushSubscriptions.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _webPushSubscriptionRepository.DeleteAsync(id);
    }

    [Authorize(WebPushPermissions.WebPushSubscriptions.Create)]
    public virtual async Task<WebPushSubscriptionDto> CreateAsync(WebPushSubscriptionCreateDto input)
    {

        var webPushSubscription = await _webPushSubscriptionManager.CreateAsync(
            input.EndPoint, input.P256dh, input.Auth, input.UserId, input.DeviceName
        );

        return ObjectMapper.Map<WebPushSubscription, WebPushSubscriptionDto>(webPushSubscription);
    }

    [Authorize(WebPushPermissions.WebPushSubscriptions.Edit)]
    public virtual async Task<WebPushSubscriptionDto> UpdateAsync(Guid id, WebPushSubscriptionUpdateDto input)
    {

        var webPushSubscription = await _webPushSubscriptionManager.UpdateAsync(
            id,
            input.EndPoint, input.P256dh, input.Auth, input.UserId, input.DeviceName, input.ConcurrencyStamp
        );

        return ObjectMapper.Map<WebPushSubscription, WebPushSubscriptionDto>(webPushSubscription);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(WebPushSubscriptionExcelDownloadDto input)
    {
        var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await _webPushSubscriptionRepository.GetListAsync(input.FilterText, input.EndPoint, input.P256dh, input.Auth, input.UserId, input.DeviceName);

        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<WebPushSubscription>, List<WebPushSubscriptionExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);

        return new RemoteStreamContent(memoryStream, "WebPushSubscriptions.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");

        await _excelDownloadTokenCache.SetAsync(
            token,
            new WebPushSubscriptionExcelDownloadTokenCacheItem { Token = token },
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
            });

        return new DownloadTokenResultDto
        {
            Token = token
        };
    }
    
    #region User Methods
    [AllowAnonymous]
    public virtual async Task<WebPushSubscriptionDto> Subscribe(WebPushSubscriptionCreateDto input)
    {
        var authorized =
            await AuthorizationService.IsGrantedAsync(WebPushPermissions.WebPushSubscriptions.Subscribe);
        if (!authorized)
        {
            throw new AbpAuthorizationException(
                "You are not authorized to subscribe to web push notifications.");
        }
        
        WebPushSubscription webPushSubscription = await _webPushSubscriptionManager.CreateOrUpdateAsync(null, 
            input.EndPoint, input.P256dh, input.Auth, input.UserId, input.DeviceName
        );
        
        return ObjectMapper.Map<WebPushSubscription, WebPushSubscriptionDto>(webPushSubscription);
    }

    [AllowAnonymous]
    public virtual async Task Unsubscribe(WebPushSubscriptionUnsubscribeDto input)
    {
        var targetSubscription = await _webPushSubscriptionManager.GetByEndpointAsync(input.EndPoint);
            
        if (targetSubscription == null)
        {
            return;
        }

        if (input.UserId != targetSubscription.UserId || targetSubscription.UserId != CurrentUser.Id)
        {
            return;
        }
            
        await _webPushSubscriptionManager.DeleteByEndpointAsync(input.EndPoint, true);
    }
    
    [AllowAnonymous]
    public virtual async Task UnsubscribeByUserAsync(Guid clientId)
    {
        var targetSubscriptions = await _webPushSubscriptionManager.GetByUserIdAsync(clientId);
            
        if (targetSubscriptions.Count < 0)
        {
            return;
        }

        foreach (var targetSubscription in targetSubscriptions)
        {
            if (clientId == targetSubscription.UserId || targetSubscription.UserId == CurrentUser.Id)
            {
                await _webPushSubscriptionManager.DeleteByUserAsync(targetSubscription.UserId, true);
            }
        }
    }
        
    /// <summary>
    /// Get the VAPID public key.
    /// </summary>
    /// <returns>VAPID public key or undefined.</returns>
    [AllowAnonymous]
    public Task<string> GetVapidPublicKeyAsync()
    {
        var vapidPublicKey = _configuration.GetSection("WebPush:VapidKeys")["PublicKey"];
        return Task.FromResult(vapidPublicKey ?? "undefined");
    }
    #endregion
}