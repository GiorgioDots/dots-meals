using Dots.Meals.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace User.RetrieveLoggedUser;

internal sealed class Response
{
    public Response(Users user)
    {
        Name = user.Name;
        BirthDate = user.BirthDate;
        Weight = user.Weight;
        Height = user.Height;
    }

    public string Name { get; set; }

    public DateOnly BirthDate { get; set; }

    public decimal Weight { get; set; }

    public decimal Height { get; set; }
}
