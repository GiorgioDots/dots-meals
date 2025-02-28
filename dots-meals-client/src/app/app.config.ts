import {
  ApplicationConfig,
  importProvidersFrom,
  provideAppInitializer,
  provideZoneChangeDetection,
} from '@angular/core'
import { provideRouter } from '@angular/router'

import { routes } from './app.routes'
import { provideHttpClient, withInterceptors } from '@angular/common/http'
import { jwtAuthInterceptor } from './core/blocks/auth/jwt-auth/jwt-auth.interceptor'
import { ApiModule } from '@/main-api/api.module'
import { environment } from '@/envs/environment'

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideAppInitializer(() => {
      initApp()
    }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([jwtAuthInterceptor])),
    importProvidersFrom(ApiModule.forRoot({ rootUrl: environment.apiUrl })),
  ],
}

const initApp = () => {
  window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (event) => {
    const theme = event.matches ? 'dark' : 'light'
    document.documentElement.setAttribute('data-theme', theme)
  })
  let theme = localStorage.getItem('app_theme')
  if (theme == null) {
    theme =
      window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches
        ? 'dark'
        : 'light'
  }
  document.documentElement.setAttribute('data-theme', theme)
}
