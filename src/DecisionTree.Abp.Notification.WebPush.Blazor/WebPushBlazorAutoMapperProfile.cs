using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using AutoMapper;

namespace DecisionTree.Abp.Notification.WebPush.Blazor;

public class WebPushBlazorAutoMapperProfile : Profile
{
    public WebPushBlazorAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<WebPushSubscriptionDto, WebPushSubscriptionUpdateDto>();
    }
}