namespace Dots.Meals.Api.Features.Test;

public sealed class Request
{
    public required string Test { get; set; }

    internal sealed class Validator : Validator<Request>
    {
        public Validator()
        {

        }
    }
}

public sealed class Response
{
    public required string Message { get; set; }
}
