import { ButtonComponent } from '@/components/ui/button/button.component'
import { InputComponent } from '@/components/ui/input/input.component'
import { DotsMealsDalEnumsActivityLevels } from '@/main-api/models/dots-meals-dal-enums-activity-levels'
import { DotsMealsDalEnumsDietType } from '@/main-api/models/dots-meals-dal-enums-diet-type'
import { DotsMealsDalEnumsGenders } from '@/main-api/models/dots-meals-dal-enums-genders'
import { ThemeService } from '@/services/theme.service'
import { getEnumOptions } from '@/utils/enum-utils'
import { Component, inject } from '@angular/core'
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
  public gendersOpts = getEnumOptions(DotsMealsDalEnumsGenders)
  public activityLevelsOpts = getEnumOptions(DotsMealsDalEnumsActivityLevels)
  public dietsOpts = getEnumOptions(DotsMealsDalEnumsDietType)

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
    console.log(this.gendersOpts)
    console.log(this.form.value)
    this.form.markAllAsTouched()
    this.form.updateValueAndValidity()
  }
}
