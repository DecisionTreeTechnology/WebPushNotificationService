using System;
using System.Collections.Generic;
using Volo.Abp.MultiTenancy;

namespace DecisionTree.Abp.Notification.WebPush.EventHandlers.Etos;

public class CreateWebPushNotificationEto : IMultiTenant
{
    public CreateWebPushNotificationEto(Guid? tenantId, IList<Guid> userIds, string title, string? body, string? icon, string? image, string? badge, DateTime? timestamp, Dictionary<string, string>? extraData)
    {
        TenantId = tenantId;
        UserIds = userIds;
        Title = title;
        Body = body;
        Icon = icon;
        Image = image;
        Badge = badge;
        Timestamp = timestamp;
        ExtraData = extraData;
    }

    public Guid? TenantId { get; set; }
    
    public IList<Guid> UserIds { get; set; }
    
    public string Title { get; set; }
    
    public string? Body { get; set; }
    
    public string? Icon { get; set; }
    
    public string? Image { get; set; }
    
    public string? Badge { get; set; }
    
    public DateTime? Timestamp { get; set; }
    
    public Dictionary<string, string>? ExtraData { get; set; }
}