CREATE TABLE Users (
    Id TEXT PRIMARY KEY,   -- Guid stored as TEXT in SQLite
    Name TEXT NOT NULL CHECK (LENGTH(Name) <= 100), -- Max length constraint
    BirthDate TEXT NOT NULL, -- SQLite doesn't have a DateOnly type, storing as TEXT (ISO 8601 format)
    Weight DECIMAL(10,2) NOT NULL, -- Decimal with precision
    Height DECIMAL(10,2) NOT NULL  -- Decimal with precision
);
