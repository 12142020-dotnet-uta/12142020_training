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
  @Output() editedPlayer = new EventEmitter<boolean>();

  constructor(private rpsService: RpsApiService) { }
  ngOnInit(): void { }

  OnSubmit(): void {
    this.rpsService.EditPlayer(this.selectedPlayer)
      .subscribe(x => {
        this.EmitEvent(true);
      });
    this.selectedPlayer.fname = null;
    this.selectedPlayer.lname = null;
  }

  EmitEvent(tru: boolean): void {
    this.editedPlayer.emit(tru);
  }
}
