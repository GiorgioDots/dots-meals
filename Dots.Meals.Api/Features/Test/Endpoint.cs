using Dots.Meals.DAL;
using Microsoft.EntityFrameworkCore;

namespace Dots.Meals.Api.Features.Test;

internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
{
    private readonly DotsMealsDbContext dc;

    public Endpoint(DotsMealsDbContext dc)
    {
        this.dc = dc;
    }

    public override void Configure()
    {
        Post("route");
        //AllowAnonymous();
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        var data = await dc.Users.Where(k => k.Weight > 50).ToListAsync(cancellationToken: c);

        await SendAsync(new Response { Message = "Hello" });
    }
}