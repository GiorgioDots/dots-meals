import { DotsAuthApiService } from '@/blocks/api/dots-auth-api.service'
import { JwtAuthService } from '@/blocks/auth/jwt-auth/jwt-auth.service'
import { environment } from '@/envs/environment'
import { AuthTokensService } from '@/services/auth-tokens.service'
import { ClientAuthService } from '@/services/client-auth.service'
import { HttpErrorResponse } from '@angular/common/http'
import { Component, inject } from '@angular/core'
import { Router, RouterOutlet } from '@angular/router'

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  readonly authApi = inject(DotsAuthApiService)
  readonly jwtAuthSvc = inject(JwtAuthService)
  readonly tokensSvc = inject(AuthTokensService)
  readonly clientAuthSvc = inject(ClientAuthService)
  readonly router = inject(Router)

  constructor() {
    this.authApi.init(environment.authUrl)
    this.jwtAuthSvc.initialize({
      jwtTokenGetterFn: () => this.tokensSvc.getToken() ?? '',
      refreshTokenFn: () => this.clientAuthSvc.refreshToken(),
      refreshTokenFailedFn: (err) => {
        if (err instanceof HttpErrorResponse && err.status != 401) {
          return
        }
        // show messages / handle error
        this.clientAuthSvc.logout()
        this.router.navigate([''])
      },
    })
  }
}
