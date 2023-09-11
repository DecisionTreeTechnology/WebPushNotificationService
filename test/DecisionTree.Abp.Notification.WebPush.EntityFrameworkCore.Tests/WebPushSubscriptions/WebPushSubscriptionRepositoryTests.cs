using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;
using Xunit;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions
{
    public class WebPushSubscriptionRepositoryTests : WebPushEntityFrameworkCoreTestBase
    {
        private readonly IWebPushSubscriptionRepository _webPushSubscriptionRepository;

        public WebPushSubscriptionRepositoryTests()
        {
            _webPushSubscriptionRepository = GetRequiredService<IWebPushSubscriptionRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _webPushSubscriptionRepository.GetListAsync(
                    endPoint: "fdea09a244df427a8fb642bc873f7f374297ee8b422040a4820456883cee3e5c503c234014504572bfc1b9fd30",
                    p256dh: "0f88bbc61e804eab8f6b0aef9",
                    auth: "9f617fbdb09142389628152d6e6db488acff4711e19842df9874f70a723415cbf68b",
                    userId: Guid.Parse("70bf4715-d745-47d2-9bb4-4757ca128fd3"),
                    deviceName: "9b0c830883af4267a0d85e52f25f9d7a7f15"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("91f2d542-9d2d-49b7-98b9-d924df4c0fa2"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _webPushSubscriptionRepository.GetCountAsync(
                    endPoint: "89a3a20dbfb641e4ae3d523c1551c4b0510fb4db47c54339bb0fd507dd8e896053a731e76b4047ab9a91adce",
                    p256dh: "d58d533389e440119b7a4bc47188294bb57f128060554e59bc33e3bb6f",
                    auth: "58ee88a659cf41c2ba11d5903f8429b879060341e4e749fcadc",
                    userId: Guid.Parse("010a646f-82e7-42fb-8cde-30525f8eb448"),
                    deviceName: "98f3634505c24652a9cdf82264ffd8f50"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}