using InternetBank.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InternetBank.Auth.Persistence.Contexts.EfCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<HideAccount> HideAccounts => Set<HideAccount>();
    public DbSet<Config> Configs => Set<Config>();
    public DbSet<Device> Devices => Set<Device>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
