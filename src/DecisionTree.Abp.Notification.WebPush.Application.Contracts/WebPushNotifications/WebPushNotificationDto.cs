using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

public class WebPushNotificationDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid UserId { get; set; }
    public bool Sent { get; set; }
    public DateTime? SentTime { get; set; }
    public string? FailureReason { get; set; }
    public int RetryCount { get; set; }
    public Guid WebPushContentId { get; set; }

    public string ConcurrencyStamp { get; set; }
}