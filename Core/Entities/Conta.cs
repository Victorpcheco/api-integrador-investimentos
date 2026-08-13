using Core.ValueObjects;

namespace Core.Entities;
public class Conta
{
    public Guid Id { get; private init; }
    public string NumeroConta { get; private set; } = string.Empty;
    public Saldo Saldo { get; private set; } = null!;

    public Guid ClienteId { get; private set; }
    public Cliente Cliente { get; private set; } = null!;
    private Conta() { }

    private Conta(string numeroConta, Guid clienteId)
    {
        Id = Guid.NewGuid();
        NumeroConta = numeroConta;
        Saldo = Saldo.Zero();
        ClienteId = clienteId;
    }

    public static Conta Criar(string numeroConta, Guid clienteId)
    {
        if (string.IsNullOrWhiteSpace(numeroConta))
            throw new ArgumentException("O número da conta não pode ser vazio.", nameof(numeroConta));

        return new Conta(numeroConta, clienteId);
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
