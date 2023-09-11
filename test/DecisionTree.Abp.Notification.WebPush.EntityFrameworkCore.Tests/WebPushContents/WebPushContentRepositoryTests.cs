using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using DecisionTree.Abp.Notification.WebPush.EntityFrameworkCore;
using Xunit;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents
{
    public class WebPushContentRepositoryTests : WebPushEntityFrameworkCoreTestBase
    {
        private readonly IWebPushContentRepository _webPushContentRepository;

        public WebPushContentRepositoryTests()
        {
            _webPushContentRepository = GetRequiredService<IWebPushContentRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _webPushContentRepository.GetListAsync(
                    title: "028952a5b9994af284da380b897c2a8c4677a55494974272",
                    message: "81e82f8932b74de0972ad62135d18bdb2e7946255e2a4d47b6c56ad2cb12daa26551f2cacd044"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _webPushContentRepository.GetCountAsync(
                    title: "339c5e01dc5d4943891c62c147fcbfd45b403c7d74e34164b952e394cb2a617b9",
                    message: "dc9b1d62a4f14a46a0cd"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}