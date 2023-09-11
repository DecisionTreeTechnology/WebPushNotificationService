using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents;

public class WebPushContentManager : DomainService
{
    private readonly IWebPushContentRepository _webPushContentRepository;

    public WebPushContentManager(IWebPushContentRepository webPushContentRepository)
    {
        _webPushContentRepository = webPushContentRepository;
    }

    public async Task<WebPushContent> CreateAsync(
        string title, string? message, Dictionary<string, string>? extraData)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));

        var webPushContent = new WebPushContent(
            GuidGenerator.Create(),
            title, message, extraData
        );

        return await _webPushContentRepository.InsertAsync(webPushContent);
    }

    public async Task<WebPushContent> UpdateAsync(
        Guid id,
        string title, string? message, string? concurrencyStamp = null
    )
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));

        var webPushContent = await _webPushContentRepository.GetAsync(id);

        webPushContent.Title = title;
        webPushContent.Message = message;

        webPushContent.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _webPushContentRepository.UpdateAsync(webPushContent);
    }

}