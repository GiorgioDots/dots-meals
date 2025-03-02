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
    public DbSet<User> Users { get; set; }
    public DbSet<MealPlan> MealPlans { get; set; }
    public DbSet<MealDay> MealDays { get; set; }
    public DbSet<Meal> Meals { get; set; }

    public DotsMealsDbContext(DbContextOptions<DotsMealsDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Users table
        modelBuilder.Entity<User>(entity =>
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
        });

        // MealPlan Configuration
        //modelBuilder.Entity<MealPlan>().ToTable("MealPlans");
        modelBuilder.Entity<MealPlan>()
            .HasKey(mp => mp.Id);

        modelBuilder.Entity<MealPlan>()
            .Property(mp => mp.UserId)
            .IsRequired()
            .HasConversion<Guid>(); // Ensure UserId is stored as a GUID

        modelBuilder.Entity<MealPlan>()
            .Property(mp => mp.CreatedAt);

        modelBuilder.Entity<MealPlan>()
            .HasMany(mp => mp.MealDays)
            .WithOne(md => md.MealPlan)
            .HasForeignKey(md => md.MealPlanId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete MealDays when MealPlan is deleted

        // MealDay Configuration
        //modelBuilder.Entity<MealDay>().ToTable("MealDays");
        modelBuilder.Entity<MealDay>()
            .HasKey(md => md.Id);

        modelBuilder.Entity<MealDay>()
            .Property(md => md.Day)
            .IsRequired();

        modelBuilder.Entity<MealDay>()
            .HasMany(md => md.Meals)
            .WithOne(m => m.MealDay)
            .HasForeignKey(m => m.MealDayId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete Meals when MealDay is deleted

        // Meal Configuration
        //modelBuilder.Entity<Meal>().ToTable("Meal");
        modelBuilder.Entity<Meal>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Meal>()
            .Property(m => m.MealType)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Meal>()
            .Property(m => m.Food)
            .IsRequired()
            .HasMaxLength(255);
    }

}
