using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DecisionTree.Abp.Notification.WebPush.NotificationPayloads;
using DecisionTree.Abp.Notification.WebPush.WebPushContents;
using DecisionTree.Abp.Notification.WebPush.WebPushSubscriptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;
using WebPush;

namespace DecisionTree.Abp.Notification.WebPush.WebPushNotifications;

public class WebPushNotificationSender : ITransientDependency
{
    private readonly WebPushSubscriptionManager _subscriptionManager;
    private readonly WebPushNotificationManager _webPushNotificationManager;
    private readonly IConfiguration _configuration;

    public WebPushNotificationSender(
        WebPushSubscriptionManager subscriptionManager, 
        WebPushNotificationManager webPushNotificationManager,
        IConfiguration configuration)
    {
        _subscriptionManager = subscriptionManager;
        _webPushNotificationManager = webPushNotificationManager;
        _configuration = configuration;
    }
    
    public async Task SendNotificationAsync(WebPushNotification notification, WebPushContent webPushContent)
    {
        var subscriptions = await _subscriptionManager.GetByUserIdAsync(notification.UserId);
        var payload = JsonConvert.SerializeObject(new NotificationPayload(webPushContent));
        var exceptions = new List<string>();
        foreach (var subscription in subscriptions)
        {
            try
            {
                await SendAsync(subscription, payload);
            }
            catch (Exception e)
            {
                exceptions.Add(e.Message);
            }
        }

        if (exceptions.Count == 0)
        {
            await _webPushNotificationManager.SetNotificationResultAsync(notification, true);
        }
        else
        {
            await _webPushNotificationManager.SetNotificationResultAsync(notification, false,
                string.Join("; ", exceptions.ToArray()));
        }
    }
    
    private async Task SendAsync(WebPushSubscription subscription, string payload)
    {
        var vapidKeys = _configuration.GetSection("WebPush:VapidKeys");
        var vapidPublicKey = vapidKeys["PublicKey"];
        var vapidPrivateKey = vapidKeys["PrivateKey"];
        var vapidEmail = _configuration.GetSection("WebPush")["Email"];

        if (string.IsNullOrEmpty(vapidPublicKey) || string.IsNullOrEmpty(vapidPrivateKey) ||
            string.IsNullOrEmpty(vapidEmail))
        {
            throw new Exception("Web Push Vapid Keys or Email must be set.");
        }
        
        var webPushClient = new WebPushClient();
        webPushClient.SetVapidDetails(new VapidDetails(vapidEmail, vapidPublicKey, vapidPrivateKey));
        
        var pushSubscription = new PushSubscription(subscription.EndPoint, subscription.P256dh, subscription.Auth);
        try
        {
            await webPushClient.SendNotificationAsync(pushSubscription, payload);
        }
        catch (WebPushException exception)
        {
            if (exception.StatusCode is HttpStatusCode.Gone or HttpStatusCode.NotFound)
            {
                // Subscription invalided, remove it.
                await _subscriptionManager.DeleteByEndpointAsync(subscription.EndPoint, true);
            }
            else
            {
                throw;
            }
        }
    }
}