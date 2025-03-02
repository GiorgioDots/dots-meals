using Dots.Meals.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace User.UpdateUserData;

internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
{
    public DotsMealsDbContext dc { get; set; }

    public override void Configure()
    {
        Post("user-features/update-user-data");
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        var userId = User.Claims.GetUserId();
        var user = await dc.Users.FirstOrDefaultAsync(x => x.Id == new Guid(userId), cancellationToken: c);
        if (user == null)
        {
            user = Map.ToEntity(r);
            user.Id = new Guid(userId);
            dc.Users.Add(user);
        }
        else
        {
            Map.UpdateEntity(r, user);
        }

        await dc.SaveChangesAsync(c);

        await SendAsync(new Response(), cancellation: c);
    }
}