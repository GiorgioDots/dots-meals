using Dots.Meals.Api.Extensions;
using Dots.Meals.Api.Services.OpenAI;
using Dots.Meals.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meals.GeneratePlan;

internal sealed class Endpoint : EndpointWithoutRequest<Response>
{
    public DotsMealsDbContext dc { get; set; }
    public OpenAiService openAI { get; set; }

    public override void Configure()
    {
        Post("meals-features/generate-plan");
    }

    public override async Task HandleAsync(CancellationToken c)
    {
        var user = await dc.Users.FirstOrDefaultAsync(k => k.Id == new Guid(User.Claims.GetUserId()), cancellationToken: c);
        if (user == null)
        {
            ThrowError("User information are not completed, insert your information before requesting a new plan");
        }

        var res = await openAI.GenerateMealPlanAsync(user, c);

        if (res == null)
        {
            ThrowError("Something went wrong when generating the plan");
        }
        var mealPlan = new MealPlan
        {
            UserId = user.Id,
            Name = "Welcome plan",
            CreatedAt = DateTimeOffset.Now.ToUnixTimeMilliseconds()
        };
        dc.MealPlans.Add(mealPlan);
        foreach (var day in res.days)
        {
            var mealDay = new MealDay
            {
                Day = day.day,
                MealPlan = mealPlan
            };
            dc.MealDays.Add(mealDay);
            foreach (var meal in day.meals)
            {
                var mealn = new Meal
                {
                    MealType = meal.meal,
                    Food = meal.food,
                    MealDay = mealDay
                };
                dc.Meals.Add(mealn);
            }
        }

        await dc.SaveChangesAsync(c);

        await SendAsync(new Response(), cancellation: c);
    }
}