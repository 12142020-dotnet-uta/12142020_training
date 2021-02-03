import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
//import { RouterTestingModule } from '@angular/router/testing';


describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        AppComponent
      ],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'RpsAngular12142020'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('RpsAngular12142020');
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('.content span').textContent).toContain('RpsAngular12142020 will be starting p3 in 1 week!');
  });

  // add more tests here.
  it('should toggle the login boolean', () => {
    const appComponent = new AppComponent();
    expect(appComponent.login).toBe(false, 'initial value is false');
    appComponent.ToggleLogInComponent();
    expect(appComponent.login).toBe(true, 'the value after calling the function is true');
    appComponent.ToggleLogInComponent();
    expect(appComponent.login).toBe(false, 'the value after calling the function again is true');
  });


});
