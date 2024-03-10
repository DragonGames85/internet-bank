using InternetBank.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBank.Core.Persistence.Contexts.EfCore.Configurations;

public class OperationCfg : IEntityTypeConfiguration<Operation>
{
    public void Configure(EntityTypeBuilder<Operation> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(operation => operation.ReceiveAccount)
            .WithMany(account => account.ReceivedOperations)
            .HasForeignKey(operation => operation.ReceiveAccountId);

        builder.HasOne(operation => operation.SendAccount)
            .WithMany(account => account.SentOperations)
            .HasForeignKey(operation => operation.SendAccountId);

        builder.HasOne(operation => operation.OperationCurrency)
            .WithMany(currency => currency.CurrencyOperations)
            .HasForeignKey(operation => operation.CurrencyId);
    }
}
