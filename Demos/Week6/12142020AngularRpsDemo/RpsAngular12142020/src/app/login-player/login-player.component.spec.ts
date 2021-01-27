import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginPlayerComponent } from './login-player.component';

describe('LoginPlayerComponent', () => {
  let component: LoginPlayerComponent;
  let fixture: ComponentFixture<LoginPlayerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginPlayerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginPlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
