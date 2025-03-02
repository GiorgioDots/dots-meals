using Dots.Meals.DAL.Entities;
using Dots.Meals.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace User.RetrieveLoggedUser;

internal sealed class Response
{
    public Response(Dots.Meals.DAL.Entities.User user)
    {
        Name = user.Name;
        BirthDate = user.BirthDate;
        Weight = user.Weight;
        Height = user.Height;
        Gender = user.Gender;
        ActivityLevel = user.ActivityLevel;
        Allergies = user.Allergies;
        Goal = user.Goal;
        DietType = user.DietType;
    }

    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public Genders Gender { get; set; }
    public ActivityLevels? ActivityLevel { get; set; }
    public string? Allergies { get; set; }
    public string? Goal { get; set; }
    public DietType? DietType { get; set; }
}
