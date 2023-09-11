import type {
  GetWebPushSubscriptionsInput,
  WebPushSubscriptionCreateDto,
  WebPushSubscriptionDto,
  WebPushSubscriptionExcelDownloadDto,
  WebPushSubscriptionUnsubscribeDto,
  WebPushSubscriptionUpdateDto
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DownloadTokenResultDto } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class WebPushSubscriptionService {
  apiName = 'WebPush';


  create = (input: WebPushSubscriptionCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, WebPushSubscriptionDto>({
      method: 'POST',
      url: '/api/web-push/web-push-subscriptions',
      body: input,
    },
    { apiName: this.apiName,...config });


  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/web-push/web-push-subscriptions/${id}`,
    },
    { apiName: this.apiName,...config });


  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, WebPushSubscriptionDto>({
      method: 'GET',
      url: `/api/web-push/web-push-subscriptions/${id}`,
    },
    { apiName: this.apiName,...config });


  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>({
      method: 'GET',
      url: '/api/web-push/web-push-subscriptions/download-token',
    },
    { apiName: this.apiName,...config });


  getList = (input: GetWebPushSubscriptionsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<WebPushSubscriptionDto>>({
      method: 'GET',
      url: '/api/web-push/web-push-subscriptions',
      params: { filterText: input.filterText, endPoint: input.endPoint, p256dh: input.p256dh, auth: input.auth, userId: input.userId, deviceName: input.deviceName, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });


  getListAsExcelFile = (input: WebPushSubscriptionExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>({
      method: 'GET',
      responseType: 'blob',
      url: '/api/web-push/web-push-subscriptions/as-excel-file',
      params: { downloadToken: input.downloadToken, filterText: input.filterText, name: input.name },
    },
    { apiName: this.apiName,...config });


  update = (id: string, input: WebPushSubscriptionUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, WebPushSubscriptionDto>({
      method: 'PUT',
      url: `/api/web-push/web-push-subscriptions/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  getVapidPublicKey = () =>
    this.restService.request<any, string>({
        method: 'GET',
        responseType: 'text',
        url: '/api/web-push/web-push-subscriptions/vapid-public-key',
      },
      { apiName: this.apiName });

  subscribeByInput = (input: WebPushSubscriptionCreateDto) =>
    this.restService.request<any, WebPushSubscriptionDto>({
        method: 'POST',
        url: '/api/web-push/web-push-subscriptions/subscribe',
        body: input,
      },
      { apiName: this.apiName });

  unsubscribeByUser = (clientId: string) =>
    this.restService.request<any, void>({
        method: 'POST',
        url: '/api/web-push/web-push-subscriptions/unsubscribe-by-user',
        params: { clientId },
      },
      { apiName: this.apiName });

  unsubscribeByWebPushSubscriptionUnsubscribeDto = (webPushSubscriptionUnsubscribeDto: WebPushSubscriptionUnsubscribeDto) =>
    this.restService.request<any, void>({
        method: 'POST',
        url: '/api/web-push/web-push-subscriptions/unsubscribe',
        body: webPushSubscriptionUnsubscribeDto,
      },
      { apiName: this.apiName });
  constructor(private restService: RestService) {}
}
