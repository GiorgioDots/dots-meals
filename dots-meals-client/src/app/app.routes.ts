import { Routes } from '@angular/router'
import { notAuthGuard } from './core/guards/not-auth.guard'
import { isAuthGuard } from './core/guards/is-auth.guard'

export const routes: Routes = [
  {
    path: 'auth/callback',
    loadComponent: () =>
      import('@/pages/auth/callback/callback.component').then((k) => k.CallbackComponent),
    canActivate: [notAuthGuard],
  },
  {
    path: 'app',
    loadComponent: () => import('@/pages/main/main.component').then((k) => k.MainComponent),
    canActivate: [isAuthGuard],
    children: [
      {
        path: '',
        pathMatch: 'full',
        loadComponent: () =>
          import('@/pages/main/meal-plans/meal-plans.component').then((k) => k.MealPlansComponent),
      },
    ],
  },
  {
    path: '',
    pathMatch: 'full',
    loadComponent: () =>
      import('@/pages/landing/landing.component').then((k) => k.LandingComponent),
  },
]
