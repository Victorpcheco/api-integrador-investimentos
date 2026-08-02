using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<ContaCorrente> ContasCorrentes => Set<ContaCorrente>();
    public DbSet<TransacaoConta> TransacoesConta => Set<TransacaoConta>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
