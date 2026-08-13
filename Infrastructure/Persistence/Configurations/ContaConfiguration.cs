using Core.Entities;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ContaConfiguration : IEntityTypeConfiguration<Conta>
{
    public void Configure(EntityTypeBuilder<Conta> builder)
    {
        builder.ToTable("Contas");

        builder.HasKey(cc => cc.Id);

        builder.Property(cc => cc.Id)
            .ValueGeneratedNever();

        builder.Property(cc => cc.NumeroConta)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(cc => cc.NumeroConta)
            .IsUnique();

        builder.Property(cc => cc.Saldo)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasConversion(
                saldo => saldo.Valor,
                valor => Saldo.Criar(valor));

        builder.Property(cc => cc.ClienteId)
            .IsRequired();
    }
}
