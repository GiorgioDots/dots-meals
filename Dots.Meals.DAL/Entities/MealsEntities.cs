﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dots.Meals.DAL.Entities;
public class MealPlan
{
    [Key]
    public int Id { get; set; }

    public Guid UserId { get; set; } // Store which user the meal plan belongs to

    public string Name { get; set; }

    public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    [JsonIgnore]
    public virtual List<MealDay> MealDays { get; set; } = new();
}

public class MealDay
{
    [Key]
    public int Id { get; set; }

    public int MealPlanId { get; set; } // Foreign key

    [ForeignKey("MealPlanId")]
    [JsonIgnore]
    public MealPlan MealPlan { get; set; }

    public string Day { get; set; }
    public virtual List<Meal> Meals { get; set; } = new();
}

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

