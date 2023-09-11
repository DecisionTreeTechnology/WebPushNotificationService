import { ABP, downloadBlob, ListService, PagedResultDto, TrackByService } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { DateAdapter } from '@abp/ng.theme.shared/extensions';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { filter, finalize, switchMap, tap } from 'rxjs/operators';
import type {
  GetWebPushSubscriptionsInput, WebPushSubscriptionCreateDto,
  WebPushSubscriptionDto
} from '../../../proxy/web-push-subscriptions/models';
import { WebPushSubscriptionService } from '../../../proxy/web-push-subscriptions/web-push-subscription.service';
import { SwPush } from '@angular/service-worker';
@Component({
  selector: 'lib-web-push-subscription',
  changeDetection: ChangeDetectionStrategy.Default,
  providers: [ListService, { provide: NgbDateAdapter, useClass: DateAdapter }],
  templateUrl: './web-push-subscription.component.html',
  styles: [],
})
export class WebPushSubscriptionComponent implements OnInit {
  data: PagedResultDto<WebPushSubscriptionDto> = {
    items: [],
    totalCount: 0,
  };

  filters = {} as GetWebPushSubscriptionsInput;

  form: FormGroup;

  isFiltersHidden = true;

  isModalBusy = false;

  isModalOpen = false;

  isExportToExcelBusy = false;

  selected?: WebPushSubscriptionDto;

  deviceName: string;

  constructor(
    public readonly list: ListService,
    public readonly track: TrackByService,
    public readonly service: WebPushSubscriptionService,
    private confirmation: ConfirmationService,
    private fb: FormBuilder,
    private swPush: SwPush,
    private toast: ToasterService
  ) {}

  ngOnInit() {
    const getData = (query: ABP.PageQueryParams) =>
      this.service.getList({
        ...query,
        ...this.filters,
        filterText: query.filter,
      });

    const setData = (list: PagedResultDto<WebPushSubscriptionDto>) => (this.data = list);

    this.list.hookToQuery(getData).subscribe(setData);
    this.getDeviceName();
  }

  clearFilters() {
    this.filters = {} as GetWebPushSubscriptionsInput;
  }

  buildForm() {
    const { endPoint, p256dh, auth, userId, deviceName } = this.selected || {};

    this.form = this.fb.group({
      endPoint: [endPoint ?? null, [Validators.required]],
      p256dh: [p256dh ?? null, [Validators.required]],
      auth: [auth ?? null, [Validators.required]],
      userId: [userId ?? null, []],
      deviceName: [deviceName ?? null, []],
    });
  }

  hideForm() {
    this.isModalOpen = false;
    this.form.reset();
  }

  showForm() {
    this.buildForm();
    this.isModalOpen = true;
  }

  submitForm() {
    if (this.form.invalid) return;

    const request = this.selected
      ? this.service.update(this.selected.id, {
          ...this.form.value,
          concurrencyStamp: this.selected.concurrencyStamp,
        })
      : this.service.create(this.form.value);

    this.isModalBusy = true;

    request
      .pipe(
        finalize(() => (this.isModalBusy = false)),
        tap(() => this.hideForm())
      )
      .subscribe(this.list.get);
  }

  create() {
    this.selected = undefined;
    this.showForm();
  }

  update(record: WebPushSubscriptionDto) {
    this.selected = record;
    this.showForm();
  }

  delete(record: WebPushSubscriptionDto) {
    this.confirmation
      .warn('WebPush::DeleteConfirmationMessage', 'WebPush::AreYouSure', {
        messageLocalizationParams: [],
      })
      .pipe(
        filter(status => status === Confirmation.Status.confirm),
        switchMap(() => this.service.delete(record.id))
      )
      .subscribe(this.list.get);
  }

  exportToExcel() {
    this.isExportToExcelBusy = true;
    this.service
      .getDownloadToken()
      .pipe(
        switchMap(({ token }) =>
          this.service.getListAsExcelFile({ downloadToken: token, filterText: this.list.filter })
        ),
        finalize(() => (this.isExportToExcelBusy = false))
      )
      .subscribe(result => {
        downloadBlob(result, 'WebPushSubscription.xlsx');
      });
  }

  subscribeToWebPush(){
    this.service.getVapidPublicKey().subscribe(result => {
      if (result) {
        this.subscribeToNotification(result);
      }
    });
  }

  subscribeToNotification(vapidPublicKey: string) {
    this.swPush
      .requestSubscription({
        serverPublicKey: vapidPublicKey,
      })
      .then(sub => {
        const endpoint = sub.endpoint;
        const p256dh = this.arrayBufferToBase64(sub.getKey('p256dh'));
        const auth = this.arrayBufferToBase64(sub.getKey('auth'));
        const webPushSubscriptionCreateDto = {} as WebPushSubscriptionCreateDto;
        webPushSubscriptionCreateDto.endPoint = endpoint;
        webPushSubscriptionCreateDto.p256dh = p256dh;
        webPushSubscriptionCreateDto.auth = auth;
        webPushSubscriptionCreateDto.deviceName = this.deviceName;
        this.service.subscribeByInput(webPushSubscriptionCreateDto).subscribe(result => {
          if (result) {
            this.toast.success('Web push subscription is activated', 'Success');
          }
        });
      })
      .catch(() => this.toast.error('Could not subscribe to notifications', 'Error'));
  }

  private arrayBufferToBase64(buffer) {
    let binary = '';
    const bytes = new Uint8Array(buffer);
    const len = bytes.byteLength;
    for (let i = 0; i < len; i++) {
      binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
  }

  private getDeviceName() {
    if (navigator.userAgent.match(/Android/i)) {
      this.deviceName = 'Android Device';
    } else if (navigator.userAgent.match(/iPhone|iPad|iPod/i)) {
      this.deviceName = 'iOS Device';
    } else if (navigator.userAgent.match(/Windows/i)) {
      this.deviceName = 'Windows Device';
    } else if (navigator.userAgent.match(/Mac/i)) {
      this.deviceName = 'Mac Device';
    } else {
      this.deviceName = 'Unknown Device';
    }
  }
}
