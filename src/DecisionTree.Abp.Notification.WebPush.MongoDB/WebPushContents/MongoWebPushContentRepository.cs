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

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents
{
    public class MongoWebPushContentRepository : MongoDbRepository<WebPushMongoDbContext, WebPushContent, Guid>, IWebPushContentRepository
    {
        public MongoWebPushContentRepository(IMongoDbContextProvider<WebPushMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<WebPushContent>> GetListAsync(
            string filterText = null,
            string title = null,
            string? message = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, title, message);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WebPushContentConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<WebPushContent>>()
                .PageBy<WebPushContent, IMongoQueryable<WebPushContent>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
           string filterText = null,
           string title = null,
           string? message = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, title, message);
            return await query.As<IMongoQueryable<WebPushContent>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<WebPushContent> ApplyFilter(
            IQueryable<WebPushContent> query,
            string filterText,
            string title = null,
            string? message = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Title.Contains(filterText) || e.Message.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Title.Contains(title))
                    .WhereIf(!string.IsNullOrWhiteSpace(message), e => e.Message.Contains(message));
        }
    }
}