using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dots.Meals.DAL.Entities;
public class MealPlan
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid UserId { get; set; } // Store which user the meal plan belongs to
    [ForeignKey("UserId")]
    [JsonIgnore]
    public Users User { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public virtual List<MealDay> Days { get; set; }
}