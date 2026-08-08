using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class TransacaoContaRepository(
    AppDbContext context
) : ITransacaoContaRepository
{
    public async Task AdicionarAsync(TransacaoConta transacao, CancellationToken cancellationToken = default)
    {
        await context.TransacoesConta.AddAsync(transacao, cancellationToken);
    }
}
