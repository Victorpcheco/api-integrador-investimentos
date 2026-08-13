namespace Core.ValueObjects;

public record ValorTransacao
{
    public decimal Valor { get; }

    private ValorTransacao(decimal valor) => Valor = valor;

    public static ValorTransacao Criar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor da transação deve ser estritamente maior que zero.", nameof(valor));

        return new ValorTransacao(valor);
    }
}
