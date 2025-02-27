namespace Dots.Meals.Api.Features.Test;

internal sealed class Endpoint : Endpoint<Request, Response, Mapper>
{
    public override void Configure()
    {
        Post("route");
        //AllowAnonymous();
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        await SendAsync(new Response { Message = "Hello" });
    }
}