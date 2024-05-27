using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace AutoJongWebService.Models
{
    public class RequestContext : DbContext
    {
        public RequestContext(DbContextOptions<RequestContext> options)
            : base(options)
        {
        }

        public DbSet<RequestItem> RequestItems { get; set; } = null!;
    }
}