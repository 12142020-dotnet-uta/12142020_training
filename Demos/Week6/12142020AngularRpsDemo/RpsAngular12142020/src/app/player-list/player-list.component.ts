import { RpsApiService } from './../rps-api.service';
import { PlayerViewModel } from './../player-view-model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent implements OnInit {
  playerList: PlayerViewModel[];
  selectedPlayer: PlayerViewModel = new PlayerViewModel();
  playerToEdit: PlayerViewModel = new PlayerViewModel();
  constructor(private rpsService: RpsApiService) { }

  ngOnInit(): void {
    this.rpsService.PlayerList().subscribe(x => this.playerList = x);
  }

  PlayerDetails(guid: string): void {
    this.rpsService.PlayerDetails(guid).subscribe(x => this.selectedPlayer = x);

  }

  PlayerEdit(playerId: string): void {
    this.rpsService.PlayerDetails(playerId).subscribe(x => this.playerToEdit = x);

  }

}
