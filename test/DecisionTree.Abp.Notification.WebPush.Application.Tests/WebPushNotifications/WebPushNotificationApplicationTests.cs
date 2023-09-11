using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications
{
    public class WebPushNotificationsAppServiceTests : WebPushApplicationTestBase
    {
        private readonly IWebPushNotificationsAppService _webPushNotificationsAppService;
        private readonly IRepository<WebPushNotification, Guid> _webPushNotificationRepository;

        public WebPushNotificationsAppServiceTests()
        {
            _webPushNotificationsAppService = GetRequiredService<IWebPushNotificationsAppService>();
            _webPushNotificationRepository = GetRequiredService<IRepository<WebPushNotification, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _webPushNotificationsAppService.GetListAsync(new GetWebPushNotificationsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.WebPushNotification.Id == Guid.Parse("d4183cea-cb86-49b6-b637-dd2418a418ce")).ShouldBe(true);
            result.Items.Any(x => x.WebPushNotification.Id == Guid.Parse("8114813b-c7ea-45f2-bb92-a5e291a919a3")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _webPushNotificationsAppService.GetAsync(Guid.Parse("d4183cea-cb86-49b6-b637-dd2418a418ce"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("d4183cea-cb86-49b6-b637-dd2418a418ce"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WebPushNotificationCreateDto
            {
                UserId = Guid.Parse("0181805d-d8d1-421a-be6d-8e8a85741f67"),
                Sent = true,
                SentTime = new DateTime(2001, 5, 19),
                FailureReason = "1bbc10b7842247d7b5f117ad9158f9b1e86f9bf2084143",
                RetryCount = 1411051048,
                WebPushContentId = Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c")
            };

            // Act
            var serviceResult = await _webPushNotificationsAppService.CreateAsync(input);

            // Assert
            var result = await _webPushNotificationRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe(Guid.Parse("0181805d-d8d1-421a-be6d-8e8a85741f67"));
            result.Sent.ShouldBe(true);
            result.SentTime.ShouldBe(new DateTime(2001, 5, 19));
            result.FailureReason.ShouldBe("1bbc10b7842247d7b5f117ad9158f9b1e86f9bf2084143");
            result.RetryCount.ShouldBe(1411051048);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WebPushNotificationUpdateDto()
            {
                UserId = Guid.Parse("8ba52532-c1ff-4446-8dd1-04ee26559eba"),
                Sent = true,
                SentTime = new DateTime(2005, 3, 8),
                FailureReason = "1448346be21d4",
                RetryCount = 1186161144,
                WebPushContentId = Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c")
            };

            // Act
            var serviceResult = await _webPushNotificationsAppService.UpdateAsync(Guid.Parse("d4183cea-cb86-49b6-b637-dd2418a418ce"), input);

            // Assert
            var result = await _webPushNotificationRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe(Guid.Parse("8ba52532-c1ff-4446-8dd1-04ee26559eba"));
            result.Sent.ShouldBe(true);
            result.SentTime.ShouldBe(new DateTime(2005, 3, 8));
            result.FailureReason.ShouldBe("1448346be21d4");
            result.RetryCount.ShouldBe(1186161144);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _webPushNotificationsAppService.DeleteAsync(Guid.Parse("d4183cea-cb86-49b6-b637-dd2418a418ce"));

            // Assert
            var result = await _webPushNotificationRepository.FindAsync(c => c.Id == Guid.Parse("d4183cea-cb86-49b6-b637-dd2418a418ce"));

            result.ShouldBeNull();
        }
    }
}