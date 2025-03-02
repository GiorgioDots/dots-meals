using Dots.Meals.DAL.Entities;

namespace Meals.RetrievePlans;

internal sealed class Mapper : ResponseMapper<List<Response>, List<MealPlan>>
{
    public override List<Response> FromEntity(List<MealPlan> e)
    {
        return e.Select(k => new Response()
        {
            Id = k.Id,
            CreatedAt = k.CreatedAt,
            Name = k.Name,
        }).ToList();
    }
}