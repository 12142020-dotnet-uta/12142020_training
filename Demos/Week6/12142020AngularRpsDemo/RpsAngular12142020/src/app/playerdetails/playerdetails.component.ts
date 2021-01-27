import { Component, OnInit, Input } from '@angular/core';
import { PlayerViewModel } from '../player-view-model';

@Component({
  selector: 'app-playerdetails',
  templateUrl: './playerdetails.component.html',
  styleUrls: ['./playerdetails.component.css']
})
export class PlayerdetailsComponent implements OnInit {
  @Input() playerViewModel: PlayerViewModel = null;
  constructor() { }

  ngOnInit(): void {
  }

}
