// import { PlayerViewModel } from './../player-view-model';
import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
// import { Mock } from 'protractor/built/driverProviders';
import { of } from 'rxjs';

import { PlayerListComponent } from './player-list.component';
import { RpsApiService } from '../rps-api.service';

describe('PlayerListComponent', () => {
  let component: PlayerListComponent;
  let fixture: ComponentFixture<PlayerListComponent>;
  let mockEditPlayerList;// must be declared here to still be in scope inthe it() tests.
  let mockSelectedPlayer;
  let MockRpsApiService;
  let playerList = [{
    playerId: 'testing', fname: 'Moore', lname: 'Moore', numWins: 1,
    numLosses: 3, iformFile: null, jpgStringImage: null,
  },
  {
    playerId: 'stayin alive', fname: 'Mark', lname: 'Mark', numWins: 82,
    numLosses: 27, iformFile: null, jpgStringImage: null,
  }];

  beforeEach(async () => {
    MockRpsApiService = jasmine.createSpyObj('RpsApiService', ['PlayerList', 'PlayerDetails']);//name the class you will be mocking and give an array of the methods inthat service
    mockEditPlayerList = MockRpsApiService.PlayerList.and.returnValue(of(playerList));// this need to send an array of players back
    mockSelectedPlayer = MockRpsApiService.PlayerDetails.and.returnValue(of(playerList[1]));//this needs just one player
    await TestBed.configureTestingModule({
      declarations: [PlayerListComponent],
      imports: [HttpClientModule],
      providers: [{ provide: RpsApiService, useValue: MockRpsApiService }]
    })
      .compileComponents();
    fixture = TestBed.createComponent(PlayerListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('ngOninit() instantiates the player list on creation', () => {
    component.ngOnInit();// call the method to populate with the mocked values

    // assert the second numWins is 41 or 82.
    expect(component.playerList[1].numWins).toEqual(82);
    expect(mockEditPlayerList.calls.any()).toBe(true);// also verify that the mock was indeed called.
  });

  it('verifies that PlayerDetails() gets a player by playerId', () => {
    component.playerToEdit.fname = 'ZoomIsCool';
    expect(component.playerToEdit.fname).toBe('ZoomIsCool');
    component.PlayerDetails('stayin alive');// this value gets sent to the mocked service and the predetermined player is sent back.

    // then we check the selected player to verify that the correct player was sent back.
    expect(component.selectedPlayer.playerId).toBe('stayin alive');
    expect(component.playerToEdit.fname).toBe(null);
    expect(mockSelectedPlayer.calls.any()).toBe(true);// also verify that the mock was indeed called.
  });

  // it('it gets a player object to edit', () => {
  //   component.PlayerEdit('stayin alive');

  //
  //   expect(component.playerToEdit.playerId).toBe('stayin alive');
  //   expect(mockSelectedPlayer.calls.any()).toBe(true);
  // });

  // it('it changes the list to the a different list.', () => {

  //   expect(component.playerList).toBeNull;
  //   component.ReloadList();

  //   //this time check the losses
  //   expect(component.playerList[1].numLosses).toEqual(27);
  // })






});// end of describe
