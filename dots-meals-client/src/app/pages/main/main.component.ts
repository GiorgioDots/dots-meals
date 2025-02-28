import { ClientAuthService } from '@/services/client-auth.service'
import { Component, inject } from '@angular/core'
import { Router } from '@angular/router'

@Component({
  selector: 'app-main',
  imports: [],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss',
})
export class MainComponent {
  readonly clientAuthSvc = inject(ClientAuthService)
  readonly router = inject(Router)

  logout() {
    this.clientAuthSvc.logout()
    this.router.navigate([''])
  }
}
