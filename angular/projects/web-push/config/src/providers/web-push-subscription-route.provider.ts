import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';
import { eWebPushRouteNames } from '../enums/route-names';

export const WEB_PUSH_SUBSCRIPTIONS_WEB_PUSH_SUBSCRIPTION_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/web-push/web-push-subscriptions',
        parentName: eWebPushRouteNames.WebPush,
        name: 'WebPush::Menu:WebPushSubscriptions',
        layout: eLayoutType.application,
        requiredPolicy: 'WebPush.WebPushSubscriptions',
      },
    ]);
  };
}
