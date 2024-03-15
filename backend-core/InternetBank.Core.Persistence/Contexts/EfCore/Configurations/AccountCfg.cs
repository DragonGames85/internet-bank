using InternetBank.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBank.Core.Persistence.Contexts.EfCore.Configurations;

public class AccountCfg : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(account => account.ReceivedOperations)
            .WithOne(operation => operation.ReceiveAccount)
            .HasForeignKey(account => account.ReceiveAccountId);

        builder.HasMany(account => account.SentOperations)
            .WithOne(operation => operation.SendAccount)
            .HasForeignKey(account => account.SendAccountId);

        builder.HasOne(account => account.AccountCurrency)
            .WithMany(currency => currency.CurrencyAccounts)
            .HasForeignKey(account => account.CurrencyId);
    }
}
