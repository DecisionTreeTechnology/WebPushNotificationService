using Volo.Abp.Application.Dtos;
using System;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

public class GetWebPushSubscriptionsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public string? EndPoint { get; set; }
    public string? P256dh { get; set; }
    public string? Auth { get; set; }
    public Guid? UserId { get; set; }
    public string? DeviceName { get; set; }

    public GetWebPushSubscriptionsInput()
    {

    }
}