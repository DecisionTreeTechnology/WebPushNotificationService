using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using Volo.Abp.Uow;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

public class WebPushNotificationManager : DomainService
{
    private readonly IWebPushNotificationRepository _webPushNotificationRepository;

    public WebPushNotificationManager(IWebPushNotificationRepository webPushNotificationRepository)
    {
        _webPushNotificationRepository = webPushNotificationRepository;
    }

    [UnitOfWork(true)]
    public async Task<WebPushNotification> CreateAsync(Guid userId, Guid webPushContentId)
    {
        var webPushNotification = new WebPushNotification(
            GuidGenerator.Create(),
            webPushContentId, userId,  false, string.Empty, 0
        );

        return await _webPushNotificationRepository.InsertAsync(webPushNotification);
    }

    public async Task<WebPushNotification> UpdateAsync(
        Guid id,
        Guid webPushContentId, Guid userId, bool sent, string? failureReason, int retryCount, DateTime? sentTime = null, [CanBeNull] string concurrencyStamp = null
    )
    {
        Check.NotNull(webPushContentId, nameof(webPushContentId));

        var webPushNotification = await _webPushNotificationRepository.GetAsync(id);

        webPushNotification.WebPushContentId = webPushContentId;
        webPushNotification.UserId = userId;
        webPushNotification.Sent = sent;
        webPushNotification.FailureReason = failureReason;
        webPushNotification.RetryCount = retryCount;
        webPushNotification.SentTime = sentTime;

        webPushNotification.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _webPushNotificationRepository.UpdateAsync(webPushNotification);
    }
        
    public async Task SetNotificationResultAsync(WebPushNotification notification, bool success, 
        string? failureReason = null)
    {
        notification.SetResult(Clock, success, failureReason);
        await _webPushNotificationRepository.UpdateAsync(notification, true);
    }
}