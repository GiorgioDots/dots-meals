using Dots.Meals.DAL.Converters;
using Dots.Meals.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dots.Meals.DAL;
public class DotsMealsDbContext : DbContext
{
    public DbSet<Users> Users { get; set; }
    public DbSet<MealPlan> MealPlans { get; set; }
    public DbSet<MealDay> MealDays { get; set; }
    public DbSet<Meal> Meals { get; set; }

    public DotsMealsDbContext(DbContextOptions<DotsMealsDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Users table
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(u => u.BirthDate)
                .IsRequired()
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            entity.Property(u => u.Weight)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            entity.Property(u => u.Height)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            entity.Property(u => u.Gender)
                .IsRequired()
                .HasConversion<int>(); // Store as an integer

            entity.Property(u => u.ActivityLevel)
                .HasConversion<int?>(); // Nullable enum stored as an integer

            entity.Property(u => u.DietType)
                .HasConversion<int?>(); // Nullable enum stored as an integer

            entity.Property(u => u.Allergies)
                .HasMaxLength(255); // Optional field with max length

            entity.Property(u => u.Goal)
                .HasMaxLength(255); // Optional field with max length

            modelBuilder.Entity<MealPlan>().HasMany(m => m.Days).WithOne(d => d.MealPlan).HasForeignKey(d => d.MealPlanId);
            modelBuilder.Entity<MealDay>().HasMany(d => d.Meals).WithOne(m => m.MealDay).HasForeignKey(m => m.MealDayId);
        });
    }

}
