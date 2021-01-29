//import { MockRpsApiService } from './edit-player.component.spec';
import { PlayerViewModel } from './../player-view-model';
import { RpsApiService } from './../rps-api.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { EditPlayerComponent } from './edit-player.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { MockBuilder, MockInstance, MockProvider } from 'ng-mocks';
import { EMPTY, Observable, of, Subject } from 'rxjs';


describe('EditPlayerComponent', () => {// things declared in the describe() are valid for all it() methods
  let component: EditPlayerComponent;
  let fixture: ComponentFixture<EditPlayerComponent>;
  let mockEditPlayer;
  let player: PlayerViewModel = {
    playerId: null, fname: 'Moore', lname: 'Moore', numWins: 1,
    numLosses: 3, iformFile: null, jpgStringImage: null,
  };

  beforeEach(async () => {
    const MockRpsApiService = jasmine.createSpyObj('RpsApiService', ['EditPlayer']);
    mockEditPlayer = MockRpsApiService.EditPlayer.and.returnValue(of(player));
    await TestBed.configureTestingModule({
      declarations: [EditPlayerComponent],//provide all the involved Components, and their involved components here. YOu can avoid having to do this by including the "NO_ERRORS_SCHEMA" seen below
      imports: [HttpClientModule],// import HttpClientModule to quite the false flag error
      providers: [{ provide: RpsApiService, useValue: MockRpsApiService }, HttpClient],// list the services you will be mocking. Because RpsApiService itself has a dependency, provide the mock in the providers array.
    })
      .compileComponents();
    fixture = TestBed.createComponent(EditPlayerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('emits the event correctly', () => {
    spyOn(component.editedPlayer, 'emit');
    component.editedPlayer;
    component.EmitEvent(true);
    expect(component.editedPlayer.emit).toHaveBeenCalledWith(true);
  });


  // the below method may have a race condition because OnSubmit() calls a method that returns Observable. So it may fail. the fake async version is below
  it('calls the EmitEvent method', () => {
    let SpyEmitEvent = spyOn(component, 'EmitEvent');
    component.OnSubmit();
    // fixture.detectChanges();
    // tick();
    // fixture.detectChanges();

    expect(mockEditPlayer.calls.any()).toBe(true, 'EditPlayer called');// this works!
    expect(SpyEmitEvent.calls.any()).toBe(true);
    expect(component.selectedPlayer.fname).toBe(null);
    expect(component.selectedPlayer.lname).toBe(null);
  });

  // it('calls the EmitEvent method', () => {
  //   let SpyEmitEvent = spyOn(component, 'EmitEvent');
  //   component.OnSubmit();
  //   // fixture.detectChanges();
  //   // tick();
  //   // fixture.detectChanges();

  //   expect(mockEditPlayer.calls.any()).toBe(true, 'EditPlayer called');// this works!
  //   expect(SpyEmitEvent.calls.any()).toBe(true);

  //   // the below is just us testing the async-ness, but turns out you have to usefakeAsync to use of()
  //   let x = 0;
  //   of(1).subscribe(() => { x = 1; });
  //   x = 2;
  //   tick(); // (or maybe settimeout or something)// comment this out to run the test without the fakeAsync
  //   console.log(`THe return of the test of the "of()" was => ${x}`);
  // });

});//end of describe
