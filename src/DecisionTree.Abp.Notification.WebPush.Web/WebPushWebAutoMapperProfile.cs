using DecisionTree.Abp.Notification.WebPush.Web.Pages.WebPush.WebPushSubscriptions;
using Volo.Abp.AutoMapper;
using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using AutoMapper;

namespace DecisionTree.Abp.Notification.WebPush.Web;

public class WebPushWebAutoMapperProfile : Profile
{
    public WebPushWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<WebPushSubscriptionDto, WebPushSubscriptionUpdateViewModel>();
        CreateMap<WebPushSubscriptionUpdateViewModel, WebPushSubscriptionUpdateDto>();
        CreateMap<WebPushSubscriptionCreateViewModel, WebPushSubscriptionCreateDto>();
    }
}