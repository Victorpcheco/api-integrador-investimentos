using Core.Entities;

namespace Core.Interfaces;

public interface IContaRepository
{
    Task<Conta?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
}
