import { environment } from '@/envs/environment';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-callback',
  imports: [],
  templateUrl: './callback.component.html',
  styleUrl: './callback.component.scss',
})
export class CallbackComponent implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly http = inject(HttpClient);
  errorMessage?: string;

  ngOnInit(): void {
    const cbState = this.route.snapshot.queryParamMap.get('state');
    const cbCode = this.route.snapshot.queryParamMap.get('code');
    const cbStatus = this.route.snapshot.queryParamMap.get('status');

    const state = sessionStorage.getItem('auth:state');
    const verifier = sessionStorage.getItem('auth:code_verifier');

    this.cleanSessionStorage();

    if (state != cbState) {
      this.errorMessage = 'Request forged';
      return;
    }

    if (cbStatus != undefined) {
      switch (cbStatus) {
        case 'cancelled':
        default:
          this.errorMessage = 'Authorization was not accepted';
          return;
      }
    }

    this.http
      .post<JwtAuthResponseDTO>(`${environment.authUrl}/oauth/token`, {
        code: cbCode,
        code_verifier: verifier,
        grant_type: 'token',
      })
      .subscribe({
        next: (res: JwtAuthResponseDTO) => {
          localStorage.setItem('auth:rfrsh', res.refresh_token);
          sessionStorage.setItem('auth:tkn', res.access_token);
          this.router.navigate(['']);
        },
        error: (e) => {
          if (e instanceof HttpErrorResponse) {
            this.errorMessage = e.error.error;
          }
        },
      });
  }

  cleanSessionStorage() {
    sessionStorage.removeItem('auth:state');
    sessionStorage.removeItem('auth:code_verifier');
  }
}

export interface JwtAuthResponseDTO {
  access_token: string;
  refresh_token: string;
  expires_at: number;
}
