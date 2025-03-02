using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dots.Meals.DAL.Enums;

public enum Genders
{
    [Description("Male")]
    MALE = 1,
    [Description("Female")]
    FEMALE = 2
}

public enum ActivityLevels
{
    [Description("Sedentary")]
    SEDENTARY = 1,
    [Description("Lightly Active")]
    LIGHTLY_ACTIVE = 2,
    [Description("Moderately Active")]
    MODERATELY_ACTIVE = 3,
    [Description("Very Active")]
    VERY_ACTIVE = 4,
    [Description("Super Active")]
    SUPER_ACTIVE = 5,
}

public enum DietType
{
    [Description("No dietary restrictions")]
    None = 0,

    [Description("Vegetarian diet (no meat, but may include dairy and eggs)")]
    Vegetarian = 1,

    [Description("Strict plant-based diet (no animal products)")]
    Vegan = 2,

    [Description("Includes fish and seafood but avoids other meats")]
    Pescatarian = 3,

    [Description("Low-carbohydrate diet focusing on fats and proteins")]
    Keto = 4,

    [Description("High-fat, moderate-protein, low-carb diet similar to keto")]
    LowCarb = 5,

    [Description("No animal products except eggs")]
    OvoVegetarian = 6,

    [Description("No animal products except dairy")]
    LactoVegetarian = 7,

    [Description("No animal products except dairy and eggs")]
    LactoOvoVegetarian = 8,

    [Description("Whole, unprocessed foods (often includes raw food)")]
    WholeFoodPlantBased = 9,

    [Description("Avoids gluten-containing grains (wheat, barley, rye)")]
    GlutenFree = 10,

    [Description("Excludes dairy products")]
    DairyFree = 11,

    [Description("Diet following Islamic dietary laws (halal)")]
    Halal = 12,

    [Description("Diet following Jewish dietary laws (kosher)")]
    Kosher = 13
}