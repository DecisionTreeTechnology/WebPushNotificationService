using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents;

public class WebPushContentDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Title { get; set; }
    public string? Message { get; set; }

    public string ConcurrencyStamp { get; set; }
}