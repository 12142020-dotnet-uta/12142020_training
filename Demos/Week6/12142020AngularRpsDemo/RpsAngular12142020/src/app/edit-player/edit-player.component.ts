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

  // take the edited submitted player, send it to back-end
  // to enter the changes into the Db.Then call the Emit
  // event method to emit the event and pass the event to
  // the playerList copmponent to refetch the playersList and display the changes.
  OnSubmit(): void {                                // 1. call OnSubmit() 1.5. set then verify that the selectedPlayer has expected data
    this.rpsService.EditPlayer(this.selectedPlayer)// 2. mock the service and return a mocked player
      .subscribe(x => {                           //3. verifyu that EmitEvent() was called
        this.EmitEvent(true);
      });
    this.selectedPlayer.fname = null;
    this.selectedPlayer.lname = null;
  }

  EmitEvent(tru: boolean): void {
    this.editedPlayer.emit(tru);
  }
}
