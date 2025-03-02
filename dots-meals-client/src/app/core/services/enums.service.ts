import {
  EnumsRetrieveTranslationsEnumOption,
  EnumsRetrieveTranslationsEnumOptions,
} from '@/main-api/models'
import { EnumsFeaturesService } from '@/main-api/services'
import { inject, Injectable } from '@angular/core'
import { tap } from 'rxjs'

@Injectable({
  providedIn: 'root',
})
export class EnumsService {
  readonly enumsApi = inject(EnumsFeaturesService)
  private enums: EnumsRetrieveTranslationsEnumOptions[] = []

  constructor() {}

  initialize() {
    return this.enumsApi.enumsRetrieveTranslationsEndpoint().pipe(
      tap((res) => {
        this.enums = res
      }),
    )
  }

  getEnumOptions(enumName: EnumsTypes): EnumsRetrieveTranslationsEnumOption[] {
    const opts = this.enums.find((k) => k.EnumName == enumName)
    return opts?.Options ?? []
  }
}

export enum EnumsTypes {
  Genders = 'Genders',
  ActivityLevels = 'ActivityLevels',
  DietType = 'DietType',
}
