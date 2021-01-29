import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title: string = 'RpsAngular12142020';
  login: boolean = false;

  ToggleLogInComponent(): void {
    this.login = !this.login;
    console.log('ToggleLogInComponent() Works!');
  }
}
