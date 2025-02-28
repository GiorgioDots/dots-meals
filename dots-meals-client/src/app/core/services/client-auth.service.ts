import { inject, Injectable } from '@angular/core'
import { AuthTokensService } from './auth-tokens.service'
import {
  generateCodeChallenge,
  generateCodeVerifier,
  generateState,
  getDotsAuthLoginUrl,
} from '@/blocks/utils/auth-utils'
import { environment } from '@/envs/environment'
import { catchError, map, tap, throwError } from 'rxjs'
import { DotsAuthApiService } from '@/blocks/api/dots-auth-api.service'
import { GrantTypes } from '@/blocks/api/jwt-auth-request-dto'
import { ParamMap } from '@angular/router'
import { HttpErrorResponse } from '@angular/common/http'

@Injectable({
  providedIn: 'root',
})
export class ClientAuthService {
  private tokensHandler = inject(AuthTokensService)
  private dotsAuthApi = inject(DotsAuthApiService)

  public get loggedIn() {
    return this.tokensHandler.getToken() != null
  }

  public async gotoDotsAuthLogin() {
    const codeVerifier = generateCodeVerifier()
    const code_challenge = await generateCodeChallenge(codeVerifier)
    const state = generateState()
    sessionStorage.setItem('auth:code_verifier', codeVerifier)
    sessionStorage.setItem('auth:state', state)
    location.href = getDotsAuthLoginUrl(
      environment.authUrl,
      environment.clientId,
      `${location.origin}/auth/callback`,
      code_challenge,
      state,
    )
  }

  public handleAuthCallback(params: ParamMap) {
    const cbState = params.get('state')
    const cbCode = params.get('code')
    const cbStatus = params.get('status')

    const state = sessionStorage.getItem('auth:state')
    const verifier = sessionStorage.getItem('auth:code_verifier')
    sessionStorage.removeItem('auth:state')
    sessionStorage.removeItem('auth:code_verifier')

    if (!state || !cbState || state != cbState) {
      return throwError(() => 'Request forged')
    }

    if (cbStatus) {
      switch (cbStatus) {
        case 'cancelled':
        default:
          throwError(() => 'Authorization was not accepted')
      }
    }

    if (!cbCode || !verifier) {
      return throwError(() => 'Request invalid')
    }

    return this.login(cbCode, verifier).pipe(
      catchError((e) => {
        if (e instanceof HttpErrorResponse) {
          return throwError(() => e.error.error)
        }
        return throwError(() => e)
      }),
    )
  }

  public login(callBackCode: string, verifier: string) {
    return this.dotsAuthApi
      .getToken({
        code: callBackCode,
        code_verifier: verifier,
        grant_type: 'token',
      })
      .pipe(tap((res) => this.tokensHandler.setTokens(res.access_token, res.refresh_token)))
  }

  public refreshToken() {
    const refreshToken = this.tokensHandler.getRefreshToken()
    if (!refreshToken) {
      return throwError(() => 'No token or refresh token found')
    }
    return this.dotsAuthApi
      .getToken({
        grant_type: GrantTypes.refresh_token,
        refresh_token: refreshToken,
      })
      .pipe(
        tap((res) => {
          this.tokensHandler.setTokens(res.access_token, res.refresh_token)
        }),
        map((res) => {
          return res.access_token
        }),
      )
  }

  public logout() {
    this.tokensHandler.clearTokens()
  }
}
