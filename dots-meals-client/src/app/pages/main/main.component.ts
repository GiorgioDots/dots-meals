import { WelcomeComponent } from '@/components/main/welcome/welcome.component'
import { DotsButtonDirective } from '@/directives/dots-button.directive'
import { ClientAuthService } from '@/services/client-auth.service'
import { SessionService } from '@/services/session.service'
import { Component, inject, OnInit, signal } from '@angular/core'
import { Router } from '@angular/router'

@Component({
  selector: 'app-main',
  imports: [DotsButtonDirective, WelcomeComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
})
export class MainComponent implements OnInit {
  readonly clientAuthSvc = inject(ClientAuthService)
  readonly router = inject(Router)
  readonly sessionSvc = inject(SessionService)

  loading = signal(false)
  userFirstAccess = signal(false)

  ngOnInit(): void {
    this.loading.set(true)
    this.sessionSvc.initialize().subscribe({
      next: (res) => {
        this.userFirstAccess.set(!res)
        this.loading.set(false)
      },
      error: () => {
        this.loading.set(false)
      },
    })
  }

  logout() {
    console.log('click')
    this.clientAuthSvc.logout()
    this.sessionSvc.clear()
    this.router.navigate([''])
  }
}
