using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ContaRepository(
    AppDbContext context
) : IContaRepository
{
    public async Task<Conta?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Contas
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
