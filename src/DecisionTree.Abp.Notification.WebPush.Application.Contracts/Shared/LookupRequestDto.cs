using Volo.Abp.Application.Dtos;

namespace DecisionTree.Abp.Notification.WebPush.Shared;

public class LookupRequestDto : PagedResultRequestDto
{
    public string? Filter { get; set; }

    public LookupRequestDto()
    {
        MaxResultCount = MaxMaxResultCount;
    }
}