using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

public class WebPushSubscriptionCreateDto
{
    [Required]
    public string EndPoint { get; set; }
    [Required]
    public string P256dh { get; set; }
    [Required]
    public string Auth { get; set; }
    public Guid UserId { get; set; }
    public string? DeviceName { get; set; }
}