import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WebPushSubscriptionComponent } from './components/web-push-subscription.component';

const routes: Routes = [
  {
    path: '',
    component: WebPushSubscriptionComponent,
    canActivate: [AuthGuard, PermissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class WebPushSubscriptionRoutingModule {}
