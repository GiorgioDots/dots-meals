using Dots.Meals.Api.Extensions;
using Dots.Meals.Api.Services.OpenAI;
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

        await SendAsync(new Response(), cancellation: c);
    }
}