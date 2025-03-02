CREATE TABLE Users (
    Id TEXT PRIMARY KEY,   -- Guid stored as TEXT in SQLite
    Name TEXT NOT NULL CHECK (LENGTH(Name) <= 100), -- Max length constraint
    BirthDate TEXT NOT NULL, -- SQLite doesn't have a DateOnly type, storing as TEXT (ISO 8601 format)
    Weight DECIMAL(10,2) NOT NULL, -- Decimal with precision
    Height DECIMAL(10,2) NOT NULL,  -- Decimal with precision
    Gender INT NOT NULL,
    ActivityLevel INT NULL,
    Allergies TEXT NULL,
    Goal TEXT NULL,
    DietType INT NULL
);

-- Create MealPlan table
CREATE TABLE "MealPlans" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "UserId" TEXT NOT NULL,
    "CreatedAt" INTEGER NOT NULL
);

-- Create MealDay table
CREATE TABLE "MealDays" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "MealPlanId" INTEGER NOT NULL,
    "Day" TEXT NOT NULL,
    FOREIGN KEY ("MealPlanId") REFERENCES "MealPlans" ("Id") ON DELETE CASCADE
);

-- Create Meal table
CREATE TABLE "Meals" (
    "Id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "MealDayId" INTEGER NOT NULL,
    "MealType" TEXT NOT NULL,
    "Food" TEXT NOT NULL,
    FOREIGN KEY ("MealDayId") REFERENCES "MealDays" ("Id") ON DELETE CASCADE
);

-- Create indexes for better query performance
CREATE INDEX "IX_MealDays_MealPlanId" ON "MealDays" ("MealPlanId");
CREATE INDEX "IX_Meals_MealDayId" ON "Meals" ("MealDayId");