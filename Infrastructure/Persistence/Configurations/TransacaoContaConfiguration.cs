using Core.Entities;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TransacaoContaConfiguration : IEntityTypeConfiguration<TransacaoConta>
{
    public void Configure(EntityTypeBuilder<TransacaoConta> builder)
    {
        builder.ToTable("TransacoesConta");

        builder.HasKey(tc => tc.Id);

        builder.Property(tc => tc.Id)
            .ValueGeneratedNever();

        builder.Property(tc => tc.Valor)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasConversion(
                valor => valor.Valor,
                valor => ValorTransacao.Criar(valor));

        builder.Property(tc => tc.TipoTransacao)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(tc => tc.DataOperacao)
            .IsRequired();

        builder.Property(tc => tc.ContaCorrenteId)
            .IsRequired();
            
        builder.HasOne(tc => tc.ContaCorrente)
            .WithMany()
            .HasForeignKey(tc => tc.ContaCorrenteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
