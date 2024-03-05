using InternetBank.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBank.Core.Persistence.Contexts.EfCore.Configurations;

public class CurrencyCfg : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(currency => currency.CurrencyAccounts)
            .WithOne(account => account.AccountCurrency)
            .HasForeignKey(currency => currency.CurrencyId);

        builder.HasMany(currency => currency.CurrencyOperations)
            .WithOne(operation => operation.OperationCurrency)
            .HasForeignKey(currency => currency.CurrencyId);
    }
}
