/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { MealsGeneratePlanResponse } from '../../models/meals-generate-plan-response';

export interface MealsGeneratePlanEndpoint$Params {
}

export function mealsGeneratePlanEndpoint(http: HttpClient, rootUrl: string, params?: MealsGeneratePlanEndpoint$Params, context?: HttpContext): Observable<StrictHttpResponse<MealsGeneratePlanResponse>> {
  const rb = new RequestBuilder(rootUrl, mealsGeneratePlanEndpoint.PATH, 'post');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'application/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<MealsGeneratePlanResponse>;
    })
  );
}

mealsGeneratePlanEndpoint.PATH = '/meals-features/generate-plan';
