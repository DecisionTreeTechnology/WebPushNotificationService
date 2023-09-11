using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

public class WebPushSubscriptionManager : DomainService
{
    private readonly IWebPushSubscriptionRepository _webPushSubscriptionRepository;

    public WebPushSubscriptionManager(IWebPushSubscriptionRepository webPushSubscriptionRepository)
    {
        _webPushSubscriptionRepository = webPushSubscriptionRepository;
    }
    
    public async Task<WebPushSubscription> CreateOrUpdateAsync(
        Guid? id, string endPoint, string p256dh, string auth, Guid userId,
        string? deviceName = null, string? concurrencyStamp = null)
    {
        var target = await _webPushSubscriptionRepository.FindAsync(x => x.EndPoint == endPoint);
        if (target == null)
        {
            return await CreateAsync(endPoint, p256dh, auth, userId, deviceName);
        }

        if (id is not null && id != target.Id)
        {
            throw new AbpException("Subscription Id is not match with endpoint.");
        }
        return await UpdateAsync(target.Id, endPoint, p256dh, auth, userId, deviceName, concurrencyStamp);
    }

    public async Task<WebPushSubscription> CreateAsync(
        string endPoint, string p256dh, string auth, Guid userId, string? deviceName)
    {
        Check.NotNullOrWhiteSpace(endPoint, nameof(endPoint));
        Check.NotNullOrWhiteSpace(p256dh, nameof(p256dh));
        Check.NotNullOrWhiteSpace(auth, nameof(auth));

        var webPushSubscription = new WebPushSubscription(
            GuidGenerator.Create(),
            endPoint, p256dh, auth, userId, deviceName
        );

        return await _webPushSubscriptionRepository.InsertAsync(webPushSubscription);
    }

    public async Task<WebPushSubscription> UpdateAsync(
        Guid id,
        string endPoint, string p256dh, string auth, Guid userId, string? deviceName, string? concurrencyStamp = null
    )
    {
        Check.NotNullOrWhiteSpace(endPoint, nameof(endPoint));
        Check.NotNullOrWhiteSpace(p256dh, nameof(p256dh));
        Check.NotNullOrWhiteSpace(auth, nameof(auth));

        var webPushSubscription = await _webPushSubscriptionRepository.GetAsync(id);

        webPushSubscription.EndPoint = endPoint;
        webPushSubscription.P256dh = p256dh;
        webPushSubscription.Auth = auth;
        webPushSubscription.UserId = userId;
        webPushSubscription.DeviceName = deviceName;

        webPushSubscription.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _webPushSubscriptionRepository.UpdateAsync(webPushSubscription);
    }
        
    public async Task<List<WebPushSubscription>> GetByUserIdAsync(Guid userId)
    {
        return await _webPushSubscriptionRepository.GetListAsync(x => x.UserId == userId);
    }
        
    public async Task<WebPushSubscription?> GetByEndpointAsync(string endpoint)
    {
        return await _webPushSubscriptionRepository.FindAsync(x => x.EndPoint == endpoint);
    }

    public async Task DeleteByEndpointAsync(string endpoint, bool immediately = false)
    {
        var subscriptions = await _webPushSubscriptionRepository
            .GetListAsync(x => x.EndPoint == endpoint);

        await _webPushSubscriptionRepository.DeleteManyAsync(subscriptions, autoSave:immediately);
    }
        
    public async Task DeleteByUserAsync(Guid clientId, bool immediately = false)
    {
        var subscriptions = await _webPushSubscriptionRepository
            .GetListAsync(x => x.UserId == clientId);

        await _webPushSubscriptionRepository.DeleteManyAsync(subscriptions, autoSave:immediately);
    }
}