using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dots.Meals.DAL.Entities;
public class MealDay
{
    [Key]
    public int Id { get; set; }

    public int MealPlanId { get; set; } // Foreign key
    [ForeignKey("MealPlanId")]
    [JsonIgnore]
    public MealPlan MealPlan { get; set; }

    public string Day { get; set; }
    public virtual List<Meal> Meals { get; set; }
}