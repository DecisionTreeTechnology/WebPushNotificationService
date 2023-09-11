import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetWebPushSubscriptionsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  endPoint?: string;
  p256dh?: string;
  auth?: string;
  userId?: string;
  deviceName?: string;
}

export interface WebPushSubscriptionCreateDto {
  endPoint: string;
  p256dh: string;
  auth: string;
  userId?: string;
  deviceName?: string;
}

export interface WebPushSubscriptionDto extends FullAuditedEntityDto<string> {
  endPoint: string;
  p256dh: string;
  auth: string;
  userId?: string;
  deviceName?: string;
  concurrencyStamp?: string;
}

export interface WebPushSubscriptionExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
}
export interface WebPushSubscriptionUnsubscribeDto {
  endPoint: string;
  clientId?: string;
}

export interface WebPushSubscriptionUpdateDto {
  endPoint: string;
  p256dh: string;
  auth: string;
  userId?: string;
  deviceName?: string;
  concurrencyStamp?: string;
}
