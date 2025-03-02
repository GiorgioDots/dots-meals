using Dots.Meals.Api.Features.Test;
using Dots.Meals.DAL.Entities;
using Dots.Meals.DAL.Enums;
using System.Xml.Linq;

namespace User.UpdateUserData;

internal sealed class Mapper : Mapper<Request, Response, Users>
{
    public override Users ToEntity(Request request)
    {
        return new Users
        {
            Name = request.Name,
            BirthDate = request.BirthDate,
            Weight = request.Weight,
            Height = request.Height,
            Gender = request.Gender,
            ActivityLevel = request.ActivityLevel,
            Allergies = request.Allergies,
            Goal = request.Goal,
            DietType = request.DietType
        };
    }

    public override Users UpdateEntity(Request request, Users e)
    {
        e.Name = request.Name;
        e.BirthDate = request.BirthDate;
        e.Weight = request.Weight;
        e.Height = request.Height;
        e.Gender = request.Gender;
        e.ActivityLevel = request.ActivityLevel;
        e.Allergies = request.Allergies;
        e.Goal = request.Goal;
        e.DietType = request.DietType;
        return e;
    }
}