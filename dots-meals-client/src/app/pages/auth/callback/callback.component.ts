import { environment } from '@/envs/environment'
import { ClientAuthService } from '@/services/client-auth.service'
import { HttpClient, HttpErrorResponse } from '@angular/common/http'
import { Component, inject, OnInit } from '@angular/core'
import { ActivatedRoute, Router } from '@angular/router'

@Component({
  selector: 'app-callback',
  imports: [],
  templateUrl: './callback.component.html',
  styleUrl: './callback.component.css',
})
export class CallbackComponent implements OnInit {
  private readonly route = inject(ActivatedRoute)
  private readonly router = inject(Router)
  private clientAuthSvc = inject(ClientAuthService)
  errorMessage?: string

  ngOnInit(): void {
    this.clientAuthSvc.handleAuthCallback(this.route.snapshot.queryParamMap).subscribe({
      next: () => {
        this.router.navigate(['app'])
      },
      error: (e) => {
        if (typeof e == 'string') {
          this.errorMessage = e
          return
        }
        this.errorMessage = 'Something went wrong'
      },
    })
  }
}
