using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using DecisionTree.Abp.Notification.WebPush.WebPushNotifications;
using System;
using DecisionTree.Abp.Notification.WebPush.Shared;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using AutoMapper;

namespace DecisionTree.Abp.Notification.WebPush;

public class WebPushApplicationAutoMapperProfile : Profile
{
    public WebPushApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<WebPushContent, WebPushContentDto>();

        CreateMap<WebPushNotification, WebPushNotificationDto>();
        CreateMap<WebPushNotificationWithNavigationProperties, WebPushNotificationWithNavigationPropertiesDto>();
        CreateMap<WebPushContent, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Title));

        CreateMap<WebPushSubscription, WebPushSubscriptionDto>();
        CreateMap<WebPushSubscription, WebPushSubscriptionExcelDto>();
    }
}