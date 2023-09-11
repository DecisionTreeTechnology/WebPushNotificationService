using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents;

public class WebPushContentUpdateDto : IHasConcurrencyStamp
{
    [Required]
    public string Title { get; set; }
    public string? Message { get; set; }

    public string ConcurrencyStamp { get; set; }
}