using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

public class WebPushSubscriptionUpdateDto : IHasConcurrencyStamp
{
    [Required]
    public string EndPoint { get; set; }
    [Required]
    public string P256dh { get; set; }
    [Required]
    public string Auth { get; set; }
    public Guid UserId { get; set; }
    public string? DeviceName { get; set; }

    public string ConcurrencyStamp { get; set; }
}