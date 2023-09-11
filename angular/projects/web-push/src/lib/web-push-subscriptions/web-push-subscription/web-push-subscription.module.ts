import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgModule } from '@angular/core';
import {
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';
import { PageModule } from '@abp/ng.components/page';
import { WebPushSubscriptionComponent } from './components/web-push-subscription.component';
import { WebPushSubscriptionRoutingModule } from './web-push-subscription-routing.module';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../../../../../dev-app/src/environments/environment';

@NgModule({
  declarations: [WebPushSubscriptionComponent],
  imports: [
    WebPushSubscriptionRoutingModule,
    CoreModule,
    ThemeSharedModule,
    CommercialUiModule,
    NgxValidateCoreModule,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,
    PageModule,
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production,
      // Register the ServiceWorker as soon as the app is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: 'registerWhenStable:30000',
    }),
  ],
})
export class WebPushSubscriptionModule {}

export function loadWebPushSubscriptionModuleAsChild() {
  return Promise.resolve(WebPushSubscriptionModule);
}
