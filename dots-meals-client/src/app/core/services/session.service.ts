import { UserRetrieveLoggedUserResponse } from '@/main-api/models'
import { inject, Injectable, signal } from '@angular/core'
import { SessionUser } from '../models/session-user'
import { UserFeaturesService } from '@/main-api/services'
import { tap } from 'rxjs'

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  readonly userApi = inject(UserFeaturesService)

  currentUser = signal<SessionUser | null>(null)

  initialize() {
    return this.userApi.userRetrieveLoggedUserEndpoint().pipe(
      tap((res) => {
        if (res) {
          this.currentUser.set(new SessionUser(res))
        } else {
          this.currentUser.set(null)
        }
      }),
    )
  }

  clear() {
    this.currentUser.set(null)
  }
}
