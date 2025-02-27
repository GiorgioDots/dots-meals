import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'auth/callback',
    loadComponent: () =>
      import('@/pages/auth/callback/callback.component').then(
        (k) => k.CallbackComponent
      ),
  },
];
