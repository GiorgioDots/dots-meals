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

    public DotsMealsDbContext(DbContextOptions<DotsMealsDbContext> options) : base(options) { }
}
