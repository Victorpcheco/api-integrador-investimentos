namespace Core.ValueObjects;
public record Saldo
{
    public decimal Valor { get; }

    private Saldo(decimal valor) => Valor = valor;

    public static Saldo Criar(decimal valor)
    {
        if (valor < 0)
            throw new ArgumentException("O saldo não pode ser negativo.", nameof(valor));

        return new Saldo(valor);
    }

    public static Saldo Zero() => new(0);
    public Saldo Creditar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor a creditar deve ser maior que zero.", nameof(valor));

        return new Saldo(Valor + valor);
    }
    public Saldo Debitar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor a debitar deve ser maior que zero.", nameof(valor));

        if (valor > Valor)
            throw new InvalidOperationException("Saldo insuficiente para realizar o débito.");

        return new Saldo(Valor - valor);
    }
}
