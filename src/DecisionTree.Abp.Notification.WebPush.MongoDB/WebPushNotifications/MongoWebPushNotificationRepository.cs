using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using DecisionTree.Abp.Notification.WebPush.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications
{
    public class MongoWebPushNotificationRepository : MongoDbRepository<WebPushMongoDbContext, WebPushNotification, Guid>, IWebPushNotificationRepository
    {
        public MongoWebPushNotificationRepository(IMongoDbContextProvider<WebPushMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<WebPushNotificationWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var webPushNotification = await (await GetMongoQueryableAsync(cancellationToken))
                .FirstOrDefaultAsync(e => e.Id == id, GetCancellationToken(cancellationToken));

            var webPushContent = await (await GetDbContextAsync(cancellationToken)).Collection<WebPushContent>().AsQueryable().FirstOrDefaultAsync(e => e.Id == webPushNotification.WebPushContentId, cancellationToken: cancellationToken);

            return new WebPushNotificationWithNavigationProperties
            {
                WebPushNotification = webPushNotification,
                WebPushContent = webPushContent,

            };
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, userId, sent, sentTimeMin, sentTimeMax, failureReason, retryCountMin, retryCountMax, webPushContentId);
            var webPushNotifications = await query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WebPushNotificationConsts.GetDefaultSorting(false) : sorting.Split('.').Last())
                .As<IMongoQueryable<WebPushNotification>>()
                .PageBy<WebPushNotification, IMongoQueryable<WebPushNotification>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var dbContext = await GetDbContextAsync(cancellationToken);
            return webPushNotifications.Select(s => new WebPushNotificationWithNavigationProperties
            {
                WebPushNotification = s,
                WebPushContent = dbContext.Collection<WebPushContent>().AsQueryable().FirstOrDefault(e => e.Id == s.WebPushContentId),

            }).ToList();
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, userId, sent, sentTimeMin, sentTimeMax, failureReason, retryCountMin, retryCountMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WebPushNotificationConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<WebPushNotification>>()
                .PageBy<WebPushNotification, IMongoQueryable<WebPushNotification>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, userId, sent, sentTimeMin, sentTimeMax, failureReason, retryCountMin, retryCountMax, webPushContentId);
            return await query.As<IMongoQueryable<WebPushNotification>>().LongCountAsync(GetCancellationToken(cancellationToken));
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
            int? retryCountMax = null,
            Guid? webPushContentId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.FailureReason.Contains(filterText))
                    .WhereIf(userId.HasValue, e => e.UserId == userId)
                    .WhereIf(sent.HasValue, e => e.Sent == sent)
                    .WhereIf(sentTimeMin.HasValue, e => e.SentTime >= sentTimeMin.Value)
                    .WhereIf(sentTimeMax.HasValue, e => e.SentTime <= sentTimeMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(failureReason), e => e.FailureReason.Contains(failureReason))
                    .WhereIf(retryCountMin.HasValue, e => e.RetryCount >= retryCountMin.Value)
                    .WhereIf(retryCountMax.HasValue, e => e.RetryCount <= retryCountMax.Value)
                    .WhereIf(webPushContentId != null && webPushContentId != Guid.Empty, e => e.WebPushContentId == webPushContentId);
        }
    }
}