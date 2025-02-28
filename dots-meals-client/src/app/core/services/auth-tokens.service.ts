import { TokensHandler } from '@/blocks/auth/jwt-auth/tokens-handler';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthTokensService extends TokensHandler<any> {
  constructor() {
    super('auth:tkn', 'auth:rfrsh');
  }
  
  protected override extractTokenData(token: string) {
    return {};
  }
}
