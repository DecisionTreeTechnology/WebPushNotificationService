using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

public interface IWebPushSubscriptionRepository : IRepository<WebPushSubscription, Guid>
{
    Task<List<WebPushSubscription>> GetListAsync(
        string filterText = null,
        string endPoint = null,
        string p256dh = null,
        string auth = null,
        Guid? userId = null,
        string deviceName = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountAsync(
        string filterText = null,
        string endPoint = null,
        string p256dh = null,
        string auth = null,
        Guid? userId = null,
        string deviceName = null,
        CancellationToken cancellationToken = default);
}