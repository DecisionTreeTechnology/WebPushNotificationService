using System.Threading.Tasks;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

public class WebPushNotificationSendingJob : IAsyncBackgroundJob<WebPushNotificationSendingJobArgs>, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly WebPushNotificationSender _sender;
    private readonly IWebPushContentRepository _contentRepository;
    private readonly IWebPushNotificationRepository _notificationRepository;
    
    public WebPushNotificationSendingJob(
        ICurrentTenant currentTenant,
        WebPushNotificationSender sender,
        IWebPushContentRepository contentRepository,
        IWebPushNotificationRepository notificationRepository)
    {
        _currentTenant = currentTenant;
        _sender = sender;
        _contentRepository = contentRepository;
        _notificationRepository = notificationRepository;
    }

    public async Task ExecuteAsync(WebPushNotificationSendingJobArgs args)
    {
        using var changeTenant = _currentTenant.Change(args.TenantId);
        
        var notification = await _notificationRepository.GetAsync(args.WebPushNotificationId);
        var notificationContent = await _contentRepository.GetAsync(notification.WebPushContentId);

        await _sender.SendNotificationAsync(notification, notificationContent);
    }
}