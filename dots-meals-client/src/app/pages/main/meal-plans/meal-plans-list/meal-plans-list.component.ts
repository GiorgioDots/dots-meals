import { MealsRetrievePlansResponse } from '@/main-api/models';
import { MealsFeaturesService } from '@/main-api/services';
import { DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';

@Component({
  selector: 'app-meal-plans-list',
  imports: [DatePipe],
  templateUrl: './meal-plans-list.component.html',
  styleUrl: './meal-plans-list.component.css'
})
export class MealPlansListComponent {
  readonly mealsApi = inject(MealsFeaturesService)

  mealPlans = signal<MealsRetrievePlansResponse[]>([]);

  ngOnInit(): void {
    this.mealsApi.mealsRetrievePlansEndpoint().subscribe({
      next: (res) => {
        const sorted = res.sort((a,b) => a.CreatedAt! - b.CreatedAt!)
        this.mealPlans.set(sorted);
      }
    })
  }
}
