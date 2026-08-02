namespace Core.Interfaces;

/// <summary>
/// Abstração para gerenciamento de transações e persistência no banco de dados.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
