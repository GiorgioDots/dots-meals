/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { EnumsRetrieveTranslationsEnumOptions } from '../../models/enums-retrieve-translations-enum-options';

export interface EnumsRetrieveTranslationsEndpoint$Params {
}

export function enumsRetrieveTranslationsEndpoint(http: HttpClient, rootUrl: string, params?: EnumsRetrieveTranslationsEndpoint$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<EnumsRetrieveTranslationsEnumOptions>>> {
  const rb = new RequestBuilder(rootUrl, enumsRetrieveTranslationsEndpoint.PATH, 'post');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'application/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Array<EnumsRetrieveTranslationsEnumOptions>>;
    })
  );
}

enumsRetrieveTranslationsEndpoint.PATH = '/enums-features/retrieve-translations';
