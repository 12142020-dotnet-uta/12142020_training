import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LoginPlayerComponent } from './login-player/login-player.component';
import { PlayerdetailsComponent } from './playerdetails/playerdetails.component';
import { RoutingModule } from './routing.module';
import { PlayerListComponent } from './player-list/player-list.component';
import { EditPlayerComponent } from './edit-player/edit-player.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginPlayerComponent,
    PlayerdetailsComponent,
    PlayerListComponent,
    EditPlayerComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
