import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WebPushComponent } from './components/web-push.component';
import { loadWebPushSubscriptionModuleAsChild } from './web-push-subscriptions/web-push-subscription/web-push-subscription.module';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: WebPushComponent,
  },
  { path: 'web-push-subscriptions', loadChildren: loadWebPushSubscriptionModuleAsChild },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class WebPushRoutingModule {}
