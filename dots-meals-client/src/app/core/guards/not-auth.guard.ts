import { ClientAuthService } from '@/services/client-auth.service'
import { inject } from '@angular/core'
import { CanActivateFn, Router } from '@angular/router'

export const notAuthGuard: CanActivateFn = (route, state) => {
  const clientAuth = inject(ClientAuthService)
  if (clientAuth.loggedIn) {
    const router = inject(Router)
    router.navigate([''])
  }
  return !clientAuth.loggedIn
}
