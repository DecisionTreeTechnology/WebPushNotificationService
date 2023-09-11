import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { WebPushComponent } from './web-push.component';

describe('WebPushComponent', () => {
  let component: WebPushComponent;
  let fixture: ComponentFixture<WebPushComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ WebPushComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WebPushComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
