using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications
{
    public class WebPushNotificationsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IWebPushNotificationRepository _webPushNotificationRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly WebPushContentsDataSeedContributor _webPushContentsDataSeedContributor;

        public WebPushNotificationsDataSeedContributor(IWebPushNotificationRepository webPushNotificationRepository, IUnitOfWorkManager unitOfWorkManager, WebPushContentsDataSeedContributor webPushContentsDataSeedContributor)
        {
            _webPushNotificationRepository = webPushNotificationRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _webPushContentsDataSeedContributor = webPushContentsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _webPushContentsDataSeedContributor.SeedAsync(context);

            await _webPushNotificationRepository.InsertAsync(new WebPushNotification
            (
                id: Guid.Parse("d4183cea-cb86-49b6-b637-dd2418a418ce"),
                userId: Guid.Parse("c69ce058-e8f8-4e31-9041-b148e7998bdb"),
                sent: true,
                sentTime: new DateTime(2016, 6, 5),
                failureReason: "f4dcb4402be84ba88612b7f75bcf01f609730808b976440ca206abcd8baebfc2e21",
                retryCount: 1548683346,
                webPushContentId: Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c")
            ));

            await _webPushNotificationRepository.InsertAsync(new WebPushNotification
            (
                id: Guid.Parse("8114813b-c7ea-45f2-bb92-a5e291a919a3"),
                userId: Guid.Parse("96aea76f-c366-47b9-b1ea-7e6d6db3b0a3"),
                sent: true,
                sentTime: new DateTime(2014, 7, 5),
                failureReason: "273861f3a2e9409d8",
                retryCount: 983876280,
                webPushContentId: Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c")
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}