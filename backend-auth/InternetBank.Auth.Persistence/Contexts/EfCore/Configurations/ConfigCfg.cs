using InternetBank.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBank.Auth.Persistence.Contexts.EfCore.Configurations;

public class ConfigCfg : IEntityTypeConfiguration<Config>
{
    public void Configure(EntityTypeBuilder<Config> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(config => config.User)
            .WithOne(user => user.UserConfig)
            .HasForeignKey<Config>(hideAccount => hideAccount.UserId);
    }
}
