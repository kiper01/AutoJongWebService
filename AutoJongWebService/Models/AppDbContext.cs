using Microsoft.EntityFrameworkCore;

namespace AutoJongWebService.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CarItem> CarItems { get; set; }
        public DbSet<RequestItem> RequestItems { get; set; }
        public DbSet<ReviewItem> ReviewItems { get; set; }
        public DbSet<AdminItem> AdminItems { get; set; }
    }
}
