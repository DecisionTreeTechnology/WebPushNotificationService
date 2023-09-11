using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Newtonsoft.Json;
using Volo.Abp;
using Volo.Abp.Data;

namespace DecisionTree.Abp.Notification.WebPush.WebPushContents;

public class WebPushContent : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; set; }

    [JsonProperty("title")]
    public virtual string Title { get; set; }

    [JsonProperty("message")]
    public virtual string? Message { get; set; }

    public WebPushContent()
    {

    }
        
    public WebPushContent(Guid id, Guid? tenantId, string title) : base(id)
    {
        TenantId = tenantId;
        Title = title;
    }
        
    public WebPushContent(Guid id, Guid? tenantId, string title, string? message) : base(id)
    {
        TenantId = tenantId;
        Title = title;
        Message = message;
    }

    public WebPushContent(Guid id, string title, string? message, Dictionary<string, string>? extraData)
    {

        Id = id;
        Check.NotNull(title, nameof(title));
        Title = title;
        Message = message;
            
        foreach (var pair in extraData ?? new Dictionary<string, string>())
        {
            SetDataValue(pair.Key, pair.Value);
        }
    }
        
    public object GetDataValue(string name)
    {
        return this.GetProperty(name);
    }

    public void SetDataValue(string name, object? value)
    {
        this.SetProperty(name, value);
    }
}