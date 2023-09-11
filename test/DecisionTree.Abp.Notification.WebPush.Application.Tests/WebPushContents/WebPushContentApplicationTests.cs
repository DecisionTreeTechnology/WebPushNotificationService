using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents
{
    public class WebPushContentsAppServiceTests : WebPushApplicationTestBase
    {
        private readonly IWebPushContentsAppService _webPushContentsAppService;
        private readonly IRepository<WebPushContent, Guid> _webPushContentRepository;

        public WebPushContentsAppServiceTests()
        {
            _webPushContentsAppService = GetRequiredService<IWebPushContentsAppService>();
            _webPushContentRepository = GetRequiredService<IRepository<WebPushContent, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _webPushContentsAppService.GetListAsync(new GetWebPushContentsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("b40e0a75-ce60-4210-895b-33d4f97f5a39")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _webPushContentsAppService.GetAsync(Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new WebPushContentCreateDto
            {
                Title = "4cc23de037664ce19340b4d8a85c",
                Message = "567fdefc"
            };

            // Act
            var serviceResult = await _webPushContentsAppService.CreateAsync(input);

            // Assert
            var result = await _webPushContentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Title.ShouldBe("4cc23de037664ce19340b4d8a85c");
            result.Message.ShouldBe("567fdefc");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new WebPushContentUpdateDto()
            {
                Title = "0ca9489fa63e4e5c851e4b9692e6bf8445eba9aa59984e59964de",
                Message = "5f01423e111a487a8c341b1e2da985a9f56b1999c630467ca33452ee2482c4cd0ace2fb3f151"
            };

            // Act
            var serviceResult = await _webPushContentsAppService.UpdateAsync(Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c"), input);

            // Assert
            var result = await _webPushContentRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Title.ShouldBe("0ca9489fa63e4e5c851e4b9692e6bf8445eba9aa59984e59964de");
            result.Message.ShouldBe("5f01423e111a487a8c341b1e2da985a9f56b1999c630467ca33452ee2482c4cd0ace2fb3f151");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _webPushContentsAppService.DeleteAsync(Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c"));

            // Assert
            var result = await _webPushContentRepository.FindAsync(c => c.Id == Guid.Parse("0dd54c23-42f4-49f7-aa6e-992b1f84b45c"));

            result.ShouldBeNull();
        }
    }
}