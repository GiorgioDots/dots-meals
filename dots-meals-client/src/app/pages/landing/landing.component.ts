import { ClientAuthService } from '@/services/client-auth.service'
import { Component, inject } from '@angular/core'
import { RouterModule } from '@angular/router'

@Component({
  selector: 'app-landing',
  imports: [RouterModule],
  templateUrl: './landing.component.html',
  styleUrl: './landing.component.css',
})
export class LandingComponent {
  readonly clientAuthSvc = inject(ClientAuthService)

  gotoLogin() {
    this.clientAuthSvc.gotoDotsAuthLogin()
  }
}
