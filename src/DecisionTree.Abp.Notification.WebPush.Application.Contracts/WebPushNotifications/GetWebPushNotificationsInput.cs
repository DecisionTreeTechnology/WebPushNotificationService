using Volo.Abp.Application.Dtos;
using System;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

public class GetWebPushNotificationsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public Guid? UserId { get; set; }
    public bool? Sent { get; set; }
    public DateTime? SentTimeMin { get; set; }
    public DateTime? SentTimeMax { get; set; }
    public string? FailureReason { get; set; }
    public int? RetryCountMin { get; set; }
    public int? RetryCountMax { get; set; }
    public Guid? WebPushContentId { get; set; }

    public GetWebPushNotificationsInput()
    {

    }
}