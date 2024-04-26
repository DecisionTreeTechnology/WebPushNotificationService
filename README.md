# WebPushNotificationService ABP Framework Module
# Overview
This module integrates web push notification services into applications using the [ABP framework](https://abp.io/). It facilitates real-time communication by enabling the sending and receiving of notifications directly through web browsers.

# Features
- Notification Management: Create, send, and manage web push notifications.
- Subscription Handling: Manage user subscriptions for receiving notifications.
- Real-Time Updates: Deliver notifications in real-time to engage users effectively.
# Getting Started
To implement this module in your ABP framework application, @<Lexuan Zhou> TBD... Ensure to configure the necessary dependencies and integrate the service with your application's frontend and backend.

# Configuration
Step 1:

1. Install the [WebPushNotificationService ABP Framework Module](https://github.com/DecisionTreeTechnology/WebPushNotificationService) from Nuget into your project

2. Add DependsOn(typeof(WebPushxxxModule)) attribute to configure the module dependencies.

3. Add ```builder.ConfigureWebPush();``` to the OnModelCreating() method in ```MyProjectMigrationsDbContext.cs```.

4. Add EF Core migrations and update your database.  The service will rely on three table to retrive and push notifications: ```WebPushContents```, ```WebPushSubscriptions``` and ```WebPushNotifications```


Step 2:

A pair of VAPID key is needed for the system (what is [VAPID Key](https://stackoverflow.com/questions/40392257/what-is-vapid-and-why-is-it-useful) 🤔). There are multiple ways to generate them. 

For Angular:

- Install the [web-push](https://www.npmjs.com/package/web-push) package from NPM by running ```npm install web-push -g```
- Generate the vapid keys by running ```web-push generate-vapid-keys [--json]```. An example of a pair of VAPID Keys would be 

```
Public Key:
BFwNomlUWWD2-wDR1cLhPQOxFskYDuUmseG_Pvi2DWI8eMMQuQufq27cQWa_tpO9sGK-AxRrKLdM2YLtdHTW1Dc

Private Key:
Ycpv7MDLJpB9512dZoQ6m2puX6tlnhMoKVtydY0Lz94
```


Step 3:

Set up the configuration by putting the pair of VAPID Keys into your application appsettings.json file. The structure of the key value pair would be as follow:

```
"WebPush": {
    "VapidKeys": {
      "PublicKey": "YOUR-PUBLIC-KEY",
      "PrivateKey": "YOUR-PUBLIC-KEY"
    },
    "Email": "YOUR-EMAIL"
  }
```

# Usuage
Create a ETO (event trasfer object) and push it to the event bus and it will handle the notification queue under the hood
```
var pushNotificationEto = 
        new CreateWebPushNotificationEto(CurrentTenant.Id, new List<Guid>{createMessageInput.ToUserId}, createMessageInput.Subject, 
            null, null, null, null, new DateTime?(), null);
await _distributedEventBus.PublishAsync(pushNotificationEto);
```




Contributions are welcome! If you have suggestions or improvements, please fork the repository and submit a pull request.

# License
This project is licensed under the MIT License - see the LICENSE file for details.