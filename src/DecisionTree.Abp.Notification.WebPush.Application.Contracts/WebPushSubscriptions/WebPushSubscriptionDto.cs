using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

public class WebPushSubscriptionDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string EndPoint { get; set; }
    public string P256dh { get; set; }
    public string Auth { get; set; }
    public Guid UserId { get; set; }
    public string? DeviceName { get; set; }

    public string ConcurrencyStamp { get; set; }
}