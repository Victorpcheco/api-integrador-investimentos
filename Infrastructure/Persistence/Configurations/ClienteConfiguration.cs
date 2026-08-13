using Core.Entities;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Cpf)
            .IsRequired()
            .HasMaxLength(11)
            .HasConversion(
                cpf => cpf.Numero,
                valor => Cpf.Criar(valor));

        builder.HasIndex(c => c.Cpf)
            .IsUnique();

        builder.Property(c => c.DataCriacao)
            .IsRequired();

        builder.HasOne(c => c.Conta)
            .WithOne(cc => cc.Cliente)
            .HasForeignKey<Conta>(cc => cc.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
