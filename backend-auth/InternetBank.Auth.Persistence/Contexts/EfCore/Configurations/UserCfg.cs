using InternetBank.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBank.Auth.Persistence.Contexts.EfCore.Configurations;

public class UserCfg : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(account => account.UserRoles)
            .WithMany(role => role.RoleUsers);
    }
}
