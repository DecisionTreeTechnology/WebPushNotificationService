using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions
{
    public class WebPushSubscriptionsAppServiceTests : WebPushApplicationTestBase
    {
        private readonly IWebPushSubscriptionsAppService _webPushSubscriptionsAppService;
        private readonly IRepository<WebPushSubscription, Guid> _webPushSubscriptionRepository;

        public WebPushSubscriptionsAppServiceTests()
        {
            _webPushSubscriptionsAppService = GetRequiredService<IWebPushSubscriptionsAppService>();
            _webPushSubscriptionRepository = GetRequiredService<IRepository<WebPushSubscription, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _webPushSubscriptionsAppService.GetListAsync(new GetWebPushSubscriptionsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("91f2d542-9d2d-49b7-98b9-d924df4c0fa2")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("01391a73-0053-4a2c-9951-85e31cfc9fda")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _webPushSubscriptionsAppService.GetAsync(Guid.Parse("91f2d542-9d2d-49b7-98b9-d924df4c0fa2"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("91f2d542-9d2d-49b7-98b9-d924df4c0fa2"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WebPushSubscriptionCreateDto
            {
                EndPoint = "6343db57766a48dcb2e8b2642cfbd306cc951b99",
                P256dh = "8147ffaf74b1469aa0179356754144b535e70ddd1fae40",
                Auth = "75a5246f81d74856a647532b72ae2c4e687e384761f844629d2f24e9f511bee914645190107040d8ad52b57d7df0da4e1",
                UserId = Guid.Parse("f7057103-02e8-4736-ae70-fd66c77ee0ec"),
                DeviceName = "255b51b157064fb0b783791f78d35d55ba199887d58e48b2b6a953c55b"
            };

            // Act
            var serviceResult = await _webPushSubscriptionsAppService.CreateAsync(input);

            // Assert
            var result = await _webPushSubscriptionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EndPoint.ShouldBe("6343db57766a48dcb2e8b2642cfbd306cc951b99");
            result.P256dh.ShouldBe("8147ffaf74b1469aa0179356754144b535e70ddd1fae40");
            result.Auth.ShouldBe("75a5246f81d74856a647532b72ae2c4e687e384761f844629d2f24e9f511bee914645190107040d8ad52b57d7df0da4e1");
            result.UserId.ShouldBe(Guid.Parse("f7057103-02e8-4736-ae70-fd66c77ee0ec"));
            result.DeviceName.ShouldBe("255b51b157064fb0b783791f78d35d55ba199887d58e48b2b6a953c55b");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WebPushSubscriptionUpdateDto()
            {
                EndPoint = "3d55c778a2e94eba8255731e4d7fad8427db1d42e5694aef870bc59",
                P256dh = "002be22ab0614faf9c8800c058",
                Auth = "eea8f52351cf487fada56838cf04b7fc9564132d7c9348af9b452403b3410adc466be30013fd432f9d00233",
                UserId = Guid.Parse("48f1fe49-adb6-4aee-9a87-ce88bd64fa87"),
                DeviceName = "ab21954c155d4"
            };

            // Act
            var serviceResult = await _webPushSubscriptionsAppService.UpdateAsync(Guid.Parse("91f2d542-9d2d-49b7-98b9-d924df4c0fa2"), input);

            // Assert
            var result = await _webPushSubscriptionRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.EndPoint.ShouldBe("3d55c778a2e94eba8255731e4d7fad8427db1d42e5694aef870bc59");
            result.P256dh.ShouldBe("002be22ab0614faf9c8800c058");
            result.Auth.ShouldBe("eea8f52351cf487fada56838cf04b7fc9564132d7c9348af9b452403b3410adc466be30013fd432f9d00233");
            result.UserId.ShouldBe(Guid.Parse("48f1fe49-adb6-4aee-9a87-ce88bd64fa87"));
            result.DeviceName.ShouldBe("ab21954c155d4");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _webPushSubscriptionsAppService.DeleteAsync(Guid.Parse("91f2d542-9d2d-49b7-98b9-d924df4c0fa2"));

            // Assert
            var result = await _webPushSubscriptionRepository.FindAsync(c => c.Id == Guid.Parse("91f2d542-9d2d-49b7-98b9-d924df4c0fa2"));

            result.ShouldBeNull();
        }
    }
}