using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace AutoJongWebService.Models
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options)
            : base(options)
        {
        }

        public DbSet<CarItem> CarItems { get; set; } = null!;
    }
}