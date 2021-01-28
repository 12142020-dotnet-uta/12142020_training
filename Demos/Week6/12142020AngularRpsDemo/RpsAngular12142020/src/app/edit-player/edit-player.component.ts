import { RpsApiService } from './../rps-api.service';
import { PlayerViewModel } from './../player-view-model';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-edit-player',
  templateUrl: './edit-player.component.html',
  styleUrls: ['./edit-player.component.css']
})
export class EditPlayerComponent implements OnInit {
  @Input() selectedPlayer: PlayerViewModel = new PlayerViewModel();
  @Output() editedPlayer = new EventEmitter<void>();


  constructor(private rpsService: RpsApiService) { }

  ngOnInit(): void {
  }

  OnSubmit(): void {

    // TODO complete an event emmitter to the parent component (playerlist)
    // that will update the player nd update the list.
    this.rpsService.EditPlayer(this.selectedPlayer)
      .subscribe(x => {
        this.editedPlayer.emit();
      });
    this.selectedPlayer.fname = null;
    this.selectedPlayer.lname = null;
    // emit the event

  }
}
