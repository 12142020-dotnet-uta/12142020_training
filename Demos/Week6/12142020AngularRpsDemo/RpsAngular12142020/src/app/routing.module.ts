import { LoginPlayerComponent } from './login-player/login-player.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { PlayerListComponent } from './player-list/player-list.component';
// import { CommonModule } from '@angular/common';

const routes: Routes = [
  { path: 'login', component: LoginPlayerComponent },
  { path: 'playerlist', component: PlayerListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

// @NgModule({
//   declarations: [],
//   imports: [
//     CommonModule
//   ]
// })
export class RoutingModule { }
