using InternetBank.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBank.Auth.Persistence.Contexts.EfCore.Configurations;

public class RoleCfg : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(role => role.RoleUsers)
            .WithMany(account => account.UserRoles);
    }
}
