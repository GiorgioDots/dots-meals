/*
	jsrepo 1.41.2
	Installed from github/GiorgioDots/dots-ng-lib
	2-28-2025
*/

import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { JwtAuthRequestDTO } from "./jwt-auth-request-dto";
import { JwtAuthResponseDTO } from "./jwt-auth-response-dto";

@Injectable({
  providedIn: "root",
})
export class DotsAuthApiService {
  private authUrl?: string;
  private readonly http = inject(HttpClient);

  init(authUrl: string) {
    this.authUrl = authUrl;
  }

  getToken(data: JwtAuthRequestDTO) {
    if (!this.authUrl)
      throw new Error(
        "No authUrl setted, maybe you forgot to initialize the service",
      );
    return this.http.post<JwtAuthResponseDTO>(
      `${this.authUrl}/oauth/token`,
      data,
    );
  }
}
