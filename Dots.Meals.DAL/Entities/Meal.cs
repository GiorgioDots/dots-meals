using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dots.Meals.DAL.Entities;
public class Meal
{
    [Key]
    public int Id { get; set; }

    public int MealDayId { get; set; } // Foreign key
    [ForeignKey("MealDayId")]
    [JsonIgnore]
    public MealDay MealDay { get; set; }

    public string MealType { get; set; }
    public string Food { get; set; }
}

