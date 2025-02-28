using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dots.Meals.DAL.Entities;
public class Users
{
    [Key] 
    public Guid Id { get; set; }

    [Required] 
    [MaxLength(100)] 
    public required string Name { get; set; }

    [Required] 
    public DateOnly BirthDate { get; set; }

    [Required] 
    public decimal Weight { get; set; }

    [Required]
    public decimal Height { get; set; }
}
