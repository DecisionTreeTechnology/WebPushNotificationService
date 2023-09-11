using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

public interface IWebPushNotificationRepository : IRepository<WebPushNotification, Guid>
{
    Task<WebPushNotificationWithNavigationProperties> GetWithNavigationPropertiesAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    Task<List<WebPushNotificationWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
        string filterText = null,
        Guid? userId = null,
        bool? sent = null,
        DateTime? sentTimeMin = null,
        DateTime? sentTimeMax = null,
        string failureReason = null,
        int? retryCountMin = null,
        int? retryCountMax = null,
        Guid? webPushContentId = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<List<WebPushNotification>> GetListAsync(
        string filterText = null,
        Guid? userId = null,
        bool? sent = null,
        DateTime? sentTimeMin = null,
        DateTime? sentTimeMax = null,
        string failureReason = null,
        int? retryCountMin = null,
        int? retryCountMax = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountAsync(
        string filterText = null,
        Guid? userId = null,
        bool? sent = null,
        DateTime? sentTimeMin = null,
        DateTime? sentTimeMax = null,
        string failureReason = null,
        int? retryCountMin = null,
        int? retryCountMax = null,
        Guid? webPushContentId = null,
        CancellationToken cancellationToken = default);
}