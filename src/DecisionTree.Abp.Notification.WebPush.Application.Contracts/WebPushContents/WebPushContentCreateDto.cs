using System.ComponentModel.DataAnnotations;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents;

public class WebPushContentCreateDto
{
    [Required]
    public string Title { get; set; }
    public string? Message { get; set; }
}