using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions
{
    public class EfCoreWebPushSubscriptionRepository : EfCoreRepository<WebPushDbContext, WebPushSubscription, Guid>, IWebPushSubscriptionRepository
    {
        public EfCoreWebPushSubscriptionRepository(IDbContextProvider<WebPushDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<WebPushSubscription>> GetListAsync(
            string filterText = null,
            string endPoint = null,
            string p256dh = null,
            string auth = null,
            Guid? userId = null,
            string deviceName = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, endPoint, p256dh, auth, userId, deviceName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WebPushSubscriptionConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string endPoint = null,
            string p256dh = null,
            string auth = null,
            Guid? userId = null,
            string deviceName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, endPoint, p256dh, auth, userId, deviceName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<WebPushSubscription> ApplyFilter(
            IQueryable<WebPushSubscription> query,
            string filterText,
            string endPoint = null,
            string p256dh = null,
            string auth = null,
            Guid? userId = null,
            string deviceName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EndPoint.Contains(filterText) || e.P256dh.Contains(filterText) || e.Auth.Contains(filterText) || e.DeviceName.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(endPoint), e => e.EndPoint.Contains(endPoint))
                    .WhereIf(!string.IsNullOrWhiteSpace(p256dh), e => e.P256dh.Contains(p256dh))
                    .WhereIf(!string.IsNullOrWhiteSpace(auth), e => e.Auth.Contains(auth))
                    .WhereIf(userId.HasValue, e => e.UserId == userId)
                    .WhereIf(!string.IsNullOrWhiteSpace(deviceName), e => e.DeviceName.Contains(deviceName));
        }
    }
}