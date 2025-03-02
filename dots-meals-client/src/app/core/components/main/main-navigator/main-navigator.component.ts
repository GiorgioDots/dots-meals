import { Component, EventEmitter, Output } from '@angular/core';
import { ButtonComponent } from "../../ui/button/button.component";
import { LogoComponent } from "../../ui/logo/logo.component";

@Component({
  selector: 'app-main-navigator',
  imports: [ButtonComponent, LogoComponent],
  templateUrl: './main-navigator.component.html',
  styleUrl: './main-navigator.component.css'
})
export class MainNavigatorComponent {
  @Output() logout = new EventEmitter();

  onLogout() {
    this.logout.emit();
  }
}
