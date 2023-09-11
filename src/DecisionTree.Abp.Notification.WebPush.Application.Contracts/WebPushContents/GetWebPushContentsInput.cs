using Volo.Abp.Application.Dtos;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents;

public class GetWebPushContentsInput : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public string? Title { get; set; }
    public string? Message { get; set; }

    public GetWebPushContentsInput()
    {

    }
}