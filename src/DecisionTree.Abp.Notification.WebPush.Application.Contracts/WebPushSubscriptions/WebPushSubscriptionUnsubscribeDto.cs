using System;
using System.ComponentModel.DataAnnotations;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

public class WebPushSubscriptionUnsubscribeDto
{
    [Required]
    public string EndPoint { get; set; }

    public Guid? UserId { get; set; }
}