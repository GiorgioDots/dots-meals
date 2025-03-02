namespace Meals.RetrievePlans;

internal sealed class Request
{


    internal sealed class Validator : Validator<Request>
    {
        public Validator()
        {

        }
    }
}

internal sealed class Response
{
    public int Id { get; set; }
    public string Name { get; set; }
    public long CreatedAt { get; set; }
}
