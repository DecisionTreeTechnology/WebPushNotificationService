using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;
using DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;
using Xunit;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications
{
    public class WebPushNotificationRepositoryTests : WebPushEntityFrameworkCoreTestBase
    {
        private readonly IWebPushNotificationRepository _webPushNotificationRepository;

        public WebPushNotificationRepositoryTests()
        {
            _webPushNotificationRepository = GetRequiredService<IWebPushNotificationRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _webPushNotificationRepository.GetListAsync(
                    userId: Guid.Parse("c69ce058-e8f8-4e31-9041-b148e7998bdb"),
                    sent: true,
                    failureReason: "f4dcb4402be84ba88612b7f75bcf01f609730808b976440ca206abcd8baebfc2e21"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("d4183cea-cb86-49b6-b637-dd2418a418ce"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _webPushNotificationRepository.GetCountAsync(
                    userId: Guid.Parse("96aea76f-c366-47b9-b1ea-7e6d6db3b0a3"),
                    sent: true,
                    failureReason: "273861f3a2e9409d8"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}