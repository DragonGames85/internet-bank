using InternetBank.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBank.Auth.Persistence.Contexts.EfCore.Configurations;

public class HideAccountCfg : IEntityTypeConfiguration<HideAccount>
{
    public void Configure(EntityTypeBuilder<HideAccount> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(hideAccount => hideAccount.HideAccountUser)
            .WithMany(user => user.UserHideAccounts);
    }
}
