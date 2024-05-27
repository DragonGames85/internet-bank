using InternetBank.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBank.Auth.Persistence.Contexts.EfCore.Configurations;

public class DeviceCfg : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(config => config.User)
            .WithMany(user => user.Devices);
    }
}
