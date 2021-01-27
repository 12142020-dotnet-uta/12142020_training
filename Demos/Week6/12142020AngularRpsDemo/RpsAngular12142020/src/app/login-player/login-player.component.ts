import { RpsApiService } from './../rps-api.service';
import { Component, OnInit, Input } from '@angular/core';
import { LoginPlayerViewModel } from '../login-player-view-model';
import { NgModule } from '@angular/core';
import { PlayerViewModel } from '../player-view-model';

@Component({
  selector: 'app-login-player',
  templateUrl: './login-player.component.html',
  styleUrls: ['./login-player.component.css']
})
export class LoginPlayerComponent implements OnInit {
  loginPlayerViewModel: LoginPlayerViewModel = new LoginPlayerViewModel();// = new LoginPlayerViewModel();
  playerViewModel: PlayerViewModel = new PlayerViewModel();
  @Input() login1: boolean = false;

  //used mainly for DI
  constructor(private rpsApiService: RpsApiService) { }

  // Used to initiate the component and it's variables, lists, observables, etc.
  // Best Practice is to do this here..NOT in the constructor
  ngOnInit(): void {
  }

  OnSubmit(): void {
    this.rpsApiService.LoginPlayer(this.loginPlayerViewModel).subscribe(x => this.playerViewModel = x);
    console.log("Here after the call to the service.");

  }


}
