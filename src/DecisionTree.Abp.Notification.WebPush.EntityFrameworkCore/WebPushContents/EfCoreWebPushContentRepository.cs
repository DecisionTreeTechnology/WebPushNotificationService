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

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents
{
    public class EfCoreWebPushContentRepository : EfCoreRepository<WebPushDbContext, WebPushContent, Guid>, IWebPushContentRepository
    {
        public EfCoreWebPushContentRepository(IDbContextProvider<WebPushDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<WebPushContent>> GetListAsync(
            string filterText = null,
            string title = null,
            string message = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, title, message);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WebPushContentConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string title = null,
            string message = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, title, message);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<WebPushContent> ApplyFilter(
            IQueryable<WebPushContent> query,
            string filterText,
            string title = null,
            string message = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Title.Contains(filterText) || e.Message.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Title.Contains(title))
                    .WhereIf(!string.IsNullOrWhiteSpace(message), e => e.Message.Contains(message));
        }
    }
}