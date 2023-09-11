import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { WebPushComponent } from './components/web-push.component';
import { WebPushRoutingModule } from './web-push-routing.module';

@NgModule({
  declarations: [WebPushComponent],
  imports: [CoreModule, ThemeSharedModule, WebPushRoutingModule],
  exports: [WebPushComponent],
})
export class WebPushModule {
  static forChild(): ModuleWithProviders<WebPushModule> {
    return {
      ngModule: WebPushModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<WebPushModule> {
    return new LazyModuleFactory(WebPushModule.forChild());
  }
}
