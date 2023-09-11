import { Component, OnInit } from '@angular/core';
import { WebPushService } from '../services/web-push.service';

@Component({
  selector: 'lib-web-push',
  template: ` <p>web-push works!</p> `,
  styles: [],
})
export class WebPushComponent implements OnInit {
  constructor(private service: WebPushService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
