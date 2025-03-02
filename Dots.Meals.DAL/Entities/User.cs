using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dots.Meals.DAL.Enums;

namespace Dots.Meals.DAL.Entities;
public class User
{
    [Key] 
    public Guid Id { get; set; }

    [Required] 
    [MaxLength(100)] 
    public required string Name { get; set; }

    [Required] 
    public DateOnly BirthDate { get; set; }

    [Required] 
    public decimal Weight { get; set; }

    [Required]
    public decimal Height { get; set; }

    [Required]
    public Genders Gender { get; set; }

    public ActivityLevels? ActivityLevel { get; set; }

    public string? Allergies { get; set; }

    public string? Goal { get; set; }

    public DietType? DietType { get; set; }
}
