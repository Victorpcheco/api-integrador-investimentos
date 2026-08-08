using Core.Enums;
using Core.ValueObjects;

namespace Core.Entities;

public class TransacaoConta
{
    public Guid Id { get; private init; }
    public Guid ContaId { get; private set; }
    public Conta Conta { get; private set; } = null!;
    
    public ValorTransacao Valor { get; private set; } = null!;
    public TipoTransacao TipoTransacao { get; private set; }
    public DateTime DataOperacao { get; private init; }

    private TransacaoConta() { }

    private TransacaoConta(Guid contaId, ValorTransacao valor, TipoTransacao tipoTransacao)
    {
        Id = Guid.NewGuid();
        ContaId = contaId;
        Valor = valor;
        TipoTransacao = tipoTransacao;
        DataOperacao = DateTime.UtcNow;
    }

    public static TransacaoConta Criar(Guid contaId, decimal valor, TipoTransacao tipoTransacao)
    {
        if (contaId == Guid.Empty)
            throw new ArgumentException("Id da conta corrente inválido.", nameof(contaId));

        var valorValido = ValorTransacao.Criar(valor);

        return new TransacaoConta(contaId, valorValido, tipoTransacao);
    }
}

