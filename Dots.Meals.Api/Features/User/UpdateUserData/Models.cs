using Dots.Meals.DAL.Entities;
using Dots.Meals.DAL.Enums;
using FluentValidation;

namespace User.UpdateUserData;

internal sealed class Request
{
    public required string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public Genders Gender { get; set; }
    public ActivityLevels? ActivityLevel { get; set; }
    public string? Allergies { get; set; }
    public string? Goal { get; set; }
    public DietType? DietType { get; set; }

    internal sealed class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Weight must be greater than 0.")
                .LessThanOrEqualTo(500).WithMessage("Weight cannot exceed 500kg.");

            RuleFor(x => x.Height)
                .GreaterThan(30).WithMessage("Height must be greater than 30cm.")
                .LessThanOrEqualTo(250).WithMessage("Height cannot exceed 250cm.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender selection.");

            RuleFor(x => x.ActivityLevel)
                .IsInEnum().WithMessage("Invalid activity level selection.")
                .When(x => x.ActivityLevel.HasValue);

            RuleFor(x => x.DietType)
                .IsInEnum().WithMessage("Invalid diet type selection.")
                .When(x => x.DietType.HasValue);

            RuleFor(x => x.Allergies)
                .MaximumLength(255).WithMessage("Allergies description cannot exceed 255 characters.");

            RuleFor(x => x.Goal)
                .MaximumLength(255).WithMessage("Goal description cannot exceed 255 characters.");
        }
    }
}

internal sealed class Response
{
    public string Message = "Information saved";
}
