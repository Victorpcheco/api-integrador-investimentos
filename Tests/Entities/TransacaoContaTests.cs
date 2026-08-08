using Core.Entities;
using Core.Enums;
using System;

namespace Tests.Entities;

public class TransacaoContaTests
{
    private readonly Guid _contaIdValido = Guid.NewGuid();

    [Fact]
    public void Criar_DadosValidos_DeveRetornarInstanciaCriadaComValoresCorretos()
    {
        // Arrange
        decimal valor = 150.75m;
        var tipoTransacao = TipoTransacao.Deposito;

        // Act
        var transacao = TransacaoConta.Criar(_contaIdValido, valor, tipoTransacao);

        // Assert
        Assert.NotEqual(Guid.Empty, transacao.Id);
        Assert.Equal(_contaIdValido, transacao.ContaId);
        Assert.Equal(valor, transacao.Valor.Valor);
        Assert.Equal(tipoTransacao, transacao.TipoTransacao);
        Assert.True(transacao.DataOperacao <= DateTime.UtcNow);
        Assert.True(transacao.DataOperacao > DateTime.UtcNow.AddSeconds(-1));
    }

    [Fact]
    public void Criar_ContaIdVazio_DeveLancarArgumentException()
    {
        // Arrange
        Guid contaIdInvalido = Guid.Empty;
        decimal valor = 100m;
        var tipoTransacao = TipoTransacao.Saque;

        // Act
        Action act = () => TransacaoConta.Criar(contaIdInvalido, valor, tipoTransacao);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Criar_ValorZeroOuNegativo_DevePropagarArgumentExceptionDoValorTransacao(decimal valorInvalido)
    {
        // Arrange
        var tipoTransacao = TipoTransacao.Saque;

        // Act
        Action act = () => TransacaoConta.Criar(_contaIdValido, valorInvalido, tipoTransacao);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }
}
