using Core.Entities;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ContaCorrenteConfiguration : IEntityTypeConfiguration<ContaCorrente>
{
    public void Configure(EntityTypeBuilder<ContaCorrente> builder)
    {
        builder.ToTable("ContasCorrentes");

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
