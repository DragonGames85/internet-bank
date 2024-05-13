using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using monitoring_service.Models.Entity;

namespace monitoring_service
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TracingEntity>? Tracing { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TracingEntity>().HasKey(x => x.Id);
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
