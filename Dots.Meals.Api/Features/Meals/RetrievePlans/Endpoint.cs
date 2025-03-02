using Dots.Meals.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Meals.RetrievePlans;

internal sealed class Endpoint : EndpointWithoutRequest<List<Response>, Mapper>
{
    public DotsMealsDbContext dc { get; set; }

    public override void Configure()
    {
        Post("meals-features/retrieve-plans");
    }

    public override async Task HandleAsync(CancellationToken c)
    {
        var plans = await dc.MealPlans
            .ToListAsync();

        var res = Map.FromEntity(plans);

        await SendAsync(res, cancellation: c);
    }
}