using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications
{
    public class WebPushNotification : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual bool Sent { get; set; }

        public virtual DateTime? SentTime { get; set; }
        
        public virtual string? FailureReason { get; set; }

        public virtual int RetryCount { get; set; }
        public Guid WebPushContentId { get; set; }

        public WebPushNotification()
        {

        }

        public WebPushNotification(Guid id, Guid webPushContentId, Guid userId, bool sent, string? failureReason, int retryCount, DateTime? sentTime = null)
        {

            Id = id;
            UserId = userId;
            Sent = sent;
            FailureReason = failureReason;
            RetryCount = retryCount;
            SentTime = sentTime;
            WebPushContentId = webPushContentId;
        }
        public void SetResult(IClock clock, bool success, string? failureReason = null)
        {
            if (SentTime.HasValue)
            {
                throw new Exception("Notification already sent.");
            }
            
            SentTime = clock.Now;
            Sent = success;
            FailureReason = failureReason;
        }
    }
}