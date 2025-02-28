using Dots.Meals.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace User.RetrieveLoggedUser;

internal sealed class Endpoint(DotsMealsDbContext dc) : EndpointWithoutRequest<Response?>
{
    private readonly DotsMealsDbContext dc = dc;

    public override void Configure()
    {
        Post("/user-features/retrieve-logged-user");
    }

    public override async Task HandleAsync(CancellationToken c)
    {
        var userId = User.Claims.GetUserId();
        var user = await dc.Users.FirstOrDefaultAsync(x => x.Id == new Guid(userId), cancellationToken: c);
        if (user == null)
        {
            await SendAsync(null, cancellation: c);
            return;
        }
        await SendAsync(new Response(user), cancellation: c);
    }
}