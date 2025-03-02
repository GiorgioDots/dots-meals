using Dots.Meals.Api.Extensions;
using Dots.Meals.DAL.Enums;

namespace Enums.RetrieveTranslations;

internal sealed class Endpoint : EndpointWithoutRequest<List<EnumOptions>>
{
    private List<Type> _enums = [
        typeof(Genders), 
        typeof(ActivityLevels), 
        typeof(DietType)
    ];

    public override void Configure()
    {
        Post("enums-features/retrieve-translations");
    }

    public override async Task HandleAsync(CancellationToken c)
    {
        var res = new List<EnumOptions>();

        foreach (var enumType in _enums)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException($"Type {enumType.Name} is not an enum.");

            var options = Enum.GetValues(enumType)
                .Cast<Enum>()
                .Select(e => new EnumOption
                {
                    Key = e.ToString(),
                    Description = e.GetDescription(), // You can modify this to use descriptions if needed
                    Value = Convert.ToInt32(e)
                })
                .ToList();

            res.Add(new EnumOptions
            {
                EnumName = enumType.Name,
                Options = options 
            });
        }

        await SendAsync(res, cancellation: c);
    }
}