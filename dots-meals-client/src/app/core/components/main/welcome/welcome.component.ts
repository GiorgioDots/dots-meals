import { ButtonComponent } from '@/components/ui/button/button.component'
import { InputComponent } from '@/components/ui/input/input.component'
import { UserUpdateUserDataRequest } from '@/main-api/models'
import { DotsMealsDalEnumsActivityLevels } from '@/main-api/models/dots-meals-dal-enums-activity-levels'
import { DotsMealsDalEnumsDietType } from '@/main-api/models/dots-meals-dal-enums-diet-type'
import { DotsMealsDalEnumsGenders } from '@/main-api/models/dots-meals-dal-enums-genders'
import { UserFeaturesService } from '@/main-api/services'
import { EnumsService, EnumsTypes } from '@/services/enums.service'
import { ThemeService } from '@/services/theme.service'
import { Component, inject, signal } from '@angular/core'
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms'
import { NgIcon, provideIcons } from '@ng-icons/core'
import { heroChevronDown, heroMoon, heroSun } from '@ng-icons/heroicons/outline'
export enum Tes {
  MALE = 1,
  FEMALE = 2,
}

@Component({
  selector: 'app-welcome',
  imports: [InputComponent, ButtonComponent, NgIcon, ReactiveFormsModule],
  templateUrl: './welcome.component.html',
  styleUrl: './welcome.component.css',
  providers: [provideIcons({ heroSun, heroMoon, heroChevronDown })],
})
export class WelcomeComponent {
  readonly themeSvc = inject(ThemeService)
  readonly enumsSvc = inject(EnumsService)
  readonly userApi = inject(UserFeaturesService)

  public gendersOpts = this.enumsSvc.getEnumOptions(EnumsTypes.Genders)
  public activityLevelsOpts = this.enumsSvc.getEnumOptions(EnumsTypes.ActivityLevels)
  public dietsOpts = this.enumsSvc.getEnumOptions(EnumsTypes.DietType)

  readonly defaultLoadingMsg = 'Generate My Meal Plan! 🎉'

  loading = signal(false)
  loadingMsg = signal(this.defaultLoadingMsg)

  readonly form = new FormGroup({
    name: new FormControl('', [Validators.required]),
    birthDate: new FormControl('', Validators.required),
    weight: new FormControl('', [Validators.required, Validators.min(1), Validators.max(500)]),
    height: new FormControl('', [Validators.required, Validators.min(30), Validators.max(250)]),
    gender: new FormControl('', Validators.required),
    activityLevel: new FormControl(''), // Nullabe
    allergies: new FormControl(''),
    goal: new FormControl(''),
    dietType: new FormControl(''), // Nullable enum
  })

  onSubmit() {
    this.form.markAllAsTouched()
    this.form.updateValueAndValidity()
    if (!this.form.valid) return
    const formValue = this.form.value
    const data: UserUpdateUserDataRequest = {
      ActivityLevel: this.getNumberOrUndefined(formValue.activityLevel),
      Allergies: formValue.allergies,
      BirthDate: formValue.birthDate!,
      DietType: this.getNumberOrUndefined(formValue.dietType),
      Gender: this.getNumberOrUndefined(formValue.gender),
      Goal: formValue.goal,
      Height: this.getNumberOrUndefined(formValue.height),
      Name: formValue.name!,
      Weight: this.getNumberOrUndefined(formValue.weight),
    }
    this.loading.set(true)
    this.loadingMsg.set('Saving data')
    this.userApi
      .userUpdateUserDataEndpoint({
        body: data,
      })
      .subscribe({
        next: () => {
          this.loading.set(false);
        },
        error: () => {
          this.loading.set(false)
          this.loadingMsg.set(this.defaultLoadingMsg)
        },
      })
  }

  getNumberOrUndefined(str: string | null | undefined) {
    if (!str) {
      return undefined
    }
    const num = +str
    if (isNaN(num)) {
      return undefined
    }
    return num
  }
}
