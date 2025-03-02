using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dots.Meals.DAL.Converters;
public class DateOnlyConverter : ValueConverter<DateOnly, string>
{
    public DateOnlyConverter() : base(
        d => d.ToString("yyyy-MM-dd"),
        s => DateOnly.Parse(s))
    { }
}

public class DateOnlyComparer : ValueComparer<DateOnly>
{
    public DateOnlyComparer() : base(
        (d1, d2) => d1 == d2,
        d => d.GetHashCode())
    { }
}