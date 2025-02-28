import { DotsButtonDirective } from '@/directives/dots-button.directive'
import { Component } from '@angular/core'

@Component({
  selector: 'app-welcome',
  imports: [DotsButtonDirective],
  templateUrl: './welcome.component.html',
  styleUrl: './welcome.component.css',
})
export class WelcomeComponent {}
