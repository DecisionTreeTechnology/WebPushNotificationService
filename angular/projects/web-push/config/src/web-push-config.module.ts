import { ModuleWithProviders, NgModule } from '@angular/core';
import { WEB_PUSH_ROUTE_PROVIDERS } from './providers/route.provider';
import { WEB_PUSH_SUBSCRIPTIONS_WEB_PUSH_SUBSCRIPTION_ROUTE_PROVIDER } from './providers/web-push-subscription-route.provider';

@NgModule()
export class WebPushConfigModule {
  static forRoot(): ModuleWithProviders<WebPushConfigModule> {
    return {
      ngModule: WebPushConfigModule,
      providers: [
        WEB_PUSH_ROUTE_PROVIDERS,
        WEB_PUSH_SUBSCRIPTIONS_WEB_PUSH_SUBSCRIPTION_ROUTE_PROVIDER,
      ],
    };
  }
}
