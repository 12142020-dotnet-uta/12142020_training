import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginPlayerComponent } from './login-player.component';

describe('LoginPlayerComponent', () => {
  let component: LoginPlayerComponent;
  let fixture: ComponentFixture<LoginPlayerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({  // in the testbed, you need to import HttpClientModule to silence
      declarations: [LoginPlayerComponent], // an error that pops up saying "NullInjectorError: No provider for HttpClient!"
      imports: [HttpClientModule],          // this is apparently a new bug.
      providers: []
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
