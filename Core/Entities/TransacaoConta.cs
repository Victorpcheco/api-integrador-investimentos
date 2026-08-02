using Core.Enums;
using Core.ValueObjects;

namespace Core.Entities;

public class TransacaoConta
{
    public Guid Id { get; private init; }
    public Guid ContaCorrenteId { get; private set; }
    public ContaCorrente ContaCorrente { get; private set; } = null!;
    
    public ValorTransacao Valor { get; private set; } = null!;
    public TipoTransacao TipoTransacao { get; private set; }
    public DateTime DataOperacao { get; private init; }

    private TransacaoConta() { }

    private TransacaoConta(Guid contaCorrenteId, ValorTransacao valor, TipoTransacao tipoTransacao)
    {
        Id = Guid.NewGuid();
        ContaCorrenteId = contaCorrenteId;
        Valor = valor;
        TipoTransacao = tipoTransacao;
        DataOperacao = DateTime.UtcNow;
    }

    public static TransacaoConta Criar(Guid contaCorrenteId, decimal valor, TipoTransacao tipoTransacao)
    {
        if (contaCorrenteId == Guid.Empty)
            throw new ArgumentException("Id da conta corrente inválido.", nameof(contaCorrenteId));

        var valorValido = ValorTransacao.Criar(valor);

        return new TransacaoConta(contaCorrenteId, valorValido, tipoTransacao);
    }
}

