import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailVerifictionComponent } from './email-verifiction.component';

describe('EmailVerifictionComponent', () => {
  let component: EmailVerifictionComponent;
  let fixture: ComponentFixture<EmailVerifictionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmailVerifictionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmailVerifictionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
