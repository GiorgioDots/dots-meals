import { UserRetrieveLoggedUserResponse } from '@/main-api/models'

export class SessionUser implements UserRetrieveLoggedUserResponse {
  Name?: string | undefined
  BirthDate?: string | undefined
  Height?: number | undefined
  Weight?: number | undefined

  constructor(initialVal?: UserRetrieveLoggedUserResponse) {
    this.Name = initialVal?.Name
    this.BirthDate = initialVal?.BirthDate
    this.Height = initialVal?.Height
    this.Weight = initialVal?.Weight
  }
}
