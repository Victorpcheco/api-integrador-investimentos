using Core.Entities;
using Core.Enums;

namespace Tests.Entities;

public class TransacaoContaTests
{
    private readonly Guid _contaCorrenteIdValido = Guid.NewGuid();

    [Fact]
    public void Criar_DadosValidos_DeveRetornarInstanciaCriadaComValoresCorretos()
    {
        // Arrange
        decimal valor = 150.75m;
        var tipoTransacao = TipoTransacao.Deposito;

        // Act
        var transacao = TransacaoConta.Criar(_contaCorrenteIdValido, valor, tipoTransacao);

        // Assert
        Assert.NotEqual(Guid.Empty, transacao.Id);
        Assert.Equal(_contaCorrenteIdValido, transacao.ContaCorrenteId);
        Assert.Equal(valor, transacao.Valor.Valor);
        Assert.Equal(tipoTransacao, transacao.TipoTransacao);
        Assert.True(transacao.DataOperacao <= DateTime.UtcNow);
        Assert.True(transacao.DataOperacao > DateTime.UtcNow.AddSeconds(-1));
    }

    [Fact]
    public void Criar_ContaCorrenteIdVazio_DeveLancarArgumentException()
    {
        // Arrange
        Guid contaCorrenteIdInvalido = Guid.Empty;
        decimal valor = 100m;
        var tipoTransacao = TipoTransacao.Saque;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => TransacaoConta.Criar(contaCorrenteIdInvalido, valor, tipoTransacao));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Criar_ValorZeroOuNegativo_DevePropagarArgumentExceptionDoValorTransacao(decimal valorInvalido)
    {
        // Arrange
        var tipoTransacao = TipoTransacao.Saque;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => TransacaoConta.Criar(_contaCorrenteIdValido, valorInvalido, tipoTransacao));
    }
}
