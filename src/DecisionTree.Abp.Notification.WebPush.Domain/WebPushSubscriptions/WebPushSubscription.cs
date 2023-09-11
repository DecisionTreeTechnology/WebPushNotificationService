using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

using Volo.Abp;

namespace DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;

public class WebPushSubscription : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; set; }
        
    public virtual string EndPoint { get; set; }
        
    public virtual string P256dh { get; set; }
        
    public virtual string Auth { get; set; }

    public virtual Guid UserId { get; set; }
        
    public virtual string? DeviceName { get; set; }

    public WebPushSubscription()
    {

    }

    public WebPushSubscription(Guid id, string endPoint, string p256dh, string auth, Guid userId, string? deviceName)
    {

        Id = id;
        Check.NotNull(endPoint, nameof(endPoint));
        Check.NotNull(p256dh, nameof(p256dh));
        Check.NotNull(auth, nameof(auth));
        EndPoint = endPoint;
        P256dh = p256dh;
        Auth = auth;
        UserId = userId;
        DeviceName = deviceName;
    }

}