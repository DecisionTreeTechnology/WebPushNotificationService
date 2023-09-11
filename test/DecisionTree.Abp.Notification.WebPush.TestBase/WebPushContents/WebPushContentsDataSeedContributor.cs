using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents
{
    public class WebPushContentsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IWebPushContentRepository _webPushContentRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public WebPushContentsDataSeedContributor(IWebPushContentRepository webPushContentRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _webPushContentRepository = webPushContentRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _webPushContentRepository.InsertAsync(new WebPushContent
            (
                id: Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c"),
                title: "028952a5b9994af284da380b897c2a8c4677a55494974272",
                message: "81e82f8932b74de0972ad62135d18bdb2e7946255e2a4d47b6c56ad2cb12daa26551f2cacd044",
                null
                
            ));

            await _webPushContentRepository.InsertAsync(new WebPushContent
            (
                id: Guid.Parse("b40e0a75-ce60-4210-895b-33d4f97f5a39"),
                title: "339c5e01dc5d4943891c62c147fcbfd45b403c7d74e34164b952e394cb2a617b9",
                message: "dc9b1d62a4f14a46a0cd",
                null
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}