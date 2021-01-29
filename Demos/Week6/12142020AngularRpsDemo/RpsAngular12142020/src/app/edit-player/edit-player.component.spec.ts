import { PlayerViewModel } from './../player-view-model';
import { RpsApiService } from './../rps-api.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { EditPlayerComponent } from './edit-player.component';
// import { NO_ERRORS_SCHEMA } from '@angular/core';
// import { MockBuilder, MockInstance, MockProvider } from 'ng-mocks';
import { of } from 'rxjs';


describe('Testing EditPlayerComponent', () => {// things declared in the describe() are valid for all it() methods
  let component: EditPlayerComponent;
  let fixture: ComponentFixture<EditPlayerComponent>;
  let mockEditPlayer;
  let player: PlayerViewModel = {
    playerId: null, fname: 'Moore', lname: 'Moore', numWins: 1,
    numLosses: 3, iformFile: null, jpgStringImage: null,
  };

  beforeEach(async () => {
    const MockRpsApiService = jasmine.createSpyObj('RpsApiService', ['EditPlayer']);// createa a spy(mock) of the service and say which method will be mocked.
    mockEditPlayer = MockRpsApiService.EditPlayer.and.returnValue(of(player));// create a spy of the method and tell it what to return

    await TestBed.configureTestingModule({
      declarations: [EditPlayerComponent],//provide all the involved Components, and their involved components here. YOu can avoid having to do this by including the "NO_ERRORS_SCHEMA" seen below
      imports: [HttpClientModule],// import HttpClientModule to quite the false flag error
      providers: [{ provide: RpsApiService, useValue: MockRpsApiService }],// list the services you will be mocking. Because RpsApiService itself has a dependency, provide the mock in the providers array.
    })
      .compileComponents();
    fixture = TestBed.createComponent(EditPlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('verifies that EmitEvent() emits the event with the correct value', () => {
    spyOn(component.editedPlayer, 'emit');
    // component.editedPlayer;
    component.EmitEvent(true);
    expect(component.editedPlayer.emit).toHaveBeenCalledWith(true);
  });

  // the below method may have a race condition because OnSubmit() calls a method that returns Observable. So it may fail. the fake async version is below
  it('verifies that OnSubmit() calles the EmitEvent method', () => {
    component.selectedPlayer.fname = 'Roy';// set these values to make sure they afre set to null by the method
    component.selectedPlayer.lname = 'Rogers';
    expect(component.selectedPlayer.fname).toBe('Roy');// verify that the values are as expected.
    expect(component.selectedPlayer.lname).toBe('Rogers');

    let SpyEmitEvent = spyOn(component, 'EmitEvent');// OnSubmit() is suppose to cal this method. Just verifying that.
    component.OnSubmit();
    expect(mockEditPlayer.calls.any()).toBe(true, 'EditPlayer called');// this works!
    expect(SpyEmitEvent.calls.any()).toBe(true, 'EmitEvent() was called');
    expect(component.selectedPlayer.fname).toBe(null);
    expect(component.selectedPlayer.lname).toBe(null);
  });

  // link: https://angular.io/guide/testing-components-scenarios#async-test-with-fakeasync
  // // it('calls the EmitEvent method', fakeAsync( () => {
  // //   let SpyEmitEvent = spyOn(component, 'EmitEvent');
  // //   component.OnSubmit();
  // //   // fixture.detectChanges();
  // //   // tick();
  // //   // fixture.detectChanges();

  // //   expect(mockEditPlayer.calls.any()).toBe(true, 'EditPlayer called');// this works!
  // //   expect(SpyEmitEvent.calls.any()).toBe(true);

  // //   // the below is just us testing the async-ness, but turns out you have to usefakeAsync to use of()
  // //   let x = 0;
  // //   of(1).subscribe(() => { x = 1; });
  // //   x = 2;
  // //   tick(); // (or maybe settimeout or something)// comment this out to run the test without the fakeAsync
  // //   console.log(`The return of the test of the "of()" was => ${x}`);
  // // } ));

});//end of describe
