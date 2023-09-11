using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents;

public interface IWebPushContentRepository : IRepository<WebPushContent, Guid>
{
    Task<List<WebPushContent>> GetListAsync(
        string filterText = null,
        string title = null,
        string? message = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default
    );

    Task<long> GetCountAsync(
        string filterText = null,
        string title = null,
        string? message = null,
        CancellationToken cancellationToken = default);
}