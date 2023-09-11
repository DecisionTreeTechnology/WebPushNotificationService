using DecisionTree.Abp.Notification.WebPush.WebPushContents;
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

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications
{
    public class EfCoreWebPushNotificationRepository : EfCoreRepository<WebPushDbContext, WebPushNotification, Guid>, IWebPushNotificationRepository
    {
        public EfCoreWebPushNotificationRepository(IDbContextProvider<WebPushDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<WebPushNotificationWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(webPushNotification => new WebPushNotificationWithNavigationProperties
                {
                    WebPushNotification = webPushNotification,
                    WebPushContent = dbContext.Set<WebPushContent>().FirstOrDefault(c => c.Id == webPushNotification.WebPushContentId)
                }).FirstOrDefault();
        }

        public async Task<List<WebPushNotificationWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, userId, sent, sentTimeMin, sentTimeMax, failureReason, retryCountMin, retryCountMax, webPushContentId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WebPushNotificationConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<WebPushNotificationWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from webPushNotification in (await GetDbSetAsync())
                   join webPushContent in (await GetDbContextAsync()).Set<WebPushContent>() on webPushNotification.WebPushContentId equals webPushContent.Id into webPushContents
                   from webPushContent in webPushContents.DefaultIfEmpty()
                   select new WebPushNotificationWithNavigationProperties
                   {
                       WebPushNotification = webPushNotification,
                       WebPushContent = webPushContent
                   };
        }

        protected virtual IQueryable<WebPushNotificationWithNavigationProperties> ApplyFilter(
            IQueryable<WebPushNotificationWithNavigationProperties> query,
            string filterText,
            Guid? userId = null,
            bool? sent = null,
            DateTime? sentTimeMin = null,
            DateTime? sentTimeMax = null,
            string failureReason = null,
            int? retryCountMin = null,
            int? retryCountMax = null,
            Guid? webPushContentId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.WebPushNotification.FailureReason.Contains(filterText))
                    .WhereIf(userId.HasValue, e => e.WebPushNotification.UserId == userId)
                    .WhereIf(sent.HasValue, e => e.WebPushNotification.Sent == sent)
                    .WhereIf(sentTimeMin.HasValue, e => e.WebPushNotification.SentTime >= sentTimeMin.Value)
                    .WhereIf(sentTimeMax.HasValue, e => e.WebPushNotification.SentTime <= sentTimeMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(failureReason), e => e.WebPushNotification.FailureReason.Contains(failureReason))
                    .WhereIf(retryCountMin.HasValue, e => e.WebPushNotification.RetryCount >= retryCountMin.Value)
                    .WhereIf(retryCountMax.HasValue, e => e.WebPushNotification.RetryCount <= retryCountMax.Value)
                    .WhereIf(webPushContentId != null && webPushContentId != Guid.Empty, e => e.WebPushContent != null && e.WebPushContent.Id == webPushContentId);
        }

        public async Task<List<WebPushNotification>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, userId, sent, sentTimeMin, sentTimeMax, failureReason, retryCountMin, retryCountMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WebPushNotificationConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            Guid? userId = null,
            bool? sent = null,
            DateTime? sentTimeMin = null,
            DateTime? sentTimeMax = null,
            string failureReason = null,
            int? retryCountMin = null,
            int? retryCountMax = null,
            Guid? webPushContentId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, userId, sent, sentTimeMin, sentTimeMax, failureReason, retryCountMin, retryCountMax, webPushContentId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<WebPushNotification> ApplyFilter(
            IQueryable<WebPushNotification> query,
            string filterText,
            Guid? userId = null,
            bool? sent = null,
            DateTime? sentTimeMin = null,
            DateTime? sentTimeMax = null,
            string failureReason = null,
            int? retryCountMin = null,
            int? retryCountMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.FailureReason.Contains(filterText))
                    .WhereIf(userId.HasValue, e => e.UserId == userId)
                    .WhereIf(sent.HasValue, e => e.Sent == sent)
                    .WhereIf(sentTimeMin.HasValue, e => e.SentTime >= sentTimeMin.Value)
                    .WhereIf(sentTimeMax.HasValue, e => e.SentTime <= sentTimeMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(failureReason), e => e.FailureReason.Contains(failureReason))
                    .WhereIf(retryCountMin.HasValue, e => e.RetryCount >= retryCountMin.Value)
                    .WhereIf(retryCountMax.HasValue, e => e.RetryCount <= retryCountMax.Value);
        }
    }
}