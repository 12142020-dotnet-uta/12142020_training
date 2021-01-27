import { LoginPlayerViewModel } from './login-player-view-model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PlayerViewModel } from './player-view-model';
import { Observable, of } from 'rxjs';
//import { Console } from 'console';

@Injectable({
  providedIn: 'root'
})
export class RpsApiService {


  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      //'Access-Control-Allow-Headers': '*'
    })
  };

  constructor(private http: HttpClient) { }

  LoginPlayer(loginPlayerViewModel: LoginPlayerViewModel): Observable<PlayerViewModel> {
    //use http to post the player and get back the playerviewmodel
    return this.http.post<PlayerViewModel>('https://localhost:44345/login/loginplayer', loginPlayerViewModel, this.httpOptions);
  }

  PlayerList(): Observable<PlayerViewModel[]> {
    return this.http.get<PlayerViewModel[]>('https://localhost:44345/Player/PlayersList');
  }



  PlayerDetails(playerId: string): Observable<PlayerViewModel> {
    return this.http.get<PlayerViewModel>('https://localhost:44345/Player/PlayerDetails/' + playerId);
  }

  //called by EditPlayer() in EditPlayer component.
  // needs to return a player object to be added to the list OD redirect to the playerlist route to redisplay the list page.
  // OnSubmit of the edited player. the list needs to be updated.
  EditPlayer(player: PlayerViewModel): void {
    //create sometihng ot send back
    this.http.post<PlayerViewModel>('https://localhost:44345/Player/EditedPlayer/', player, this.httpOptions);

  }


}//end of class

