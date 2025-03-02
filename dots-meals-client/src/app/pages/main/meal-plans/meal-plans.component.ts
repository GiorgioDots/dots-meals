import { MealsRetrievePlansResponse } from '@/main-api/models'
import { MealsFeaturesService } from '@/main-api/services'
import { DatePipe } from '@angular/common'
import { Component, inject, OnInit, signal } from '@angular/core'
import { RouterModule } from '@angular/router'

@Component({
  selector: 'app-meal-plans',
  imports: [RouterModule],
  templateUrl: './meal-plans.component.html',
  styleUrl: './meal-plans.component.css',
})
export class MealPlansComponent {}
