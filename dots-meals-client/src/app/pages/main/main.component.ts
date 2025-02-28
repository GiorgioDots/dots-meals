import { DotsButtonDirective } from '@/directives/dots-button.directive'
import { ClientAuthService } from '@/services/client-auth.service'
import { Component, inject } from '@angular/core'
import { Router } from '@angular/router'

@Component({
  selector: 'app-main',
  imports: [DotsButtonDirective],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
})
export class MainComponent {
  readonly clientAuthSvc = inject(ClientAuthService)
  readonly router = inject(Router)

  logout() {
    console.log("click")
    this.clientAuthSvc.logout()
    this.router.navigate([''])
  }
}
