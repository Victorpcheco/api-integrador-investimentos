using Core.Entities;

namespace Core.Interfaces;

public interface ITransacaoContaRepository
{
    Task AdicionarAsync(TransacaoConta transacao, CancellationToken cancellationToken = default);
}
