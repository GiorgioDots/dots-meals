/*
	jsrepo 1.41.2
	Installed from github/GiorgioDots/dots-ng-lib
	2-28-2025
*/

export interface JwtAuthRequestDTO {
  code?: string;
  code_verifier?: string;
  grant_type: string;
  refresh_token?: string;
}

export enum GrantTypes {
  token = "token",
  refresh_token = "refresh_token",
}
