using Core.ValueObjects;

namespace Core.Entities;
public class ContaCorrente
{
    public Guid Id { get; private set; }
    public string NumeroConta { get; private set; } = string.Empty;
    public Saldo Saldo { get; private set; } = null!;

    public Guid ClienteId { get; private set; }
    public Cliente Cliente { get; private set; } = null!;
    private ContaCorrente() { }

    private ContaCorrente(string numeroConta, Guid clienteId)
    {
        Id = Guid.NewGuid();
        NumeroConta = numeroConta;
        Saldo = Saldo.Zero();
        ClienteId = clienteId;
    }

    public static ContaCorrente Criar(string numeroConta, Guid clienteId)
    {
        if (string.IsNullOrWhiteSpace(numeroConta))
            throw new ArgumentException("O número da conta não pode ser vazio.", nameof(numeroConta));

        return new ContaCorrente(numeroConta, clienteId);
    }

    public void Creditar(decimal valor)
    {
        Saldo = Saldo.Creditar(valor);
    }

    public void Debitar(decimal valor)
    {
        Saldo = Saldo.Debitar(valor);
    }
}
