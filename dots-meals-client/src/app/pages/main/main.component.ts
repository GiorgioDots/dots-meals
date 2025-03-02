import { WelcomeComponent } from '@/components/main/welcome/welcome.component'
import { EnumsFeaturesService } from '@/main-api/services'
import { ClientAuthService } from '@/services/client-auth.service'
import { EnumsService } from '@/services/enums.service'
import { SessionService } from '@/services/session.service'
import { Component, inject, OnInit, signal } from '@angular/core'
import { Router } from '@angular/router'
import { switchMap } from 'rxjs'

@Component({
  selector: 'app-main',
  imports: [WelcomeComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css',
})
export class MainComponent implements OnInit {
  readonly clientAuthSvc = inject(ClientAuthService)
  readonly router = inject(Router)
  readonly sessionSvc = inject(SessionService)
  readonly enumsSvc = inject(EnumsService)

  loading = signal(false)
  userFirstAccess = signal(false)

  ngOnInit(): void {
    this.loading.set(true)
    this.enumsSvc
      .initialize()
      .pipe(
        switchMap(() => {
          return this.sessionSvc.initialize()
        }),
      )
      .subscribe({
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
