using Core.Entities;

namespace Tests.Entities;

public class ContaTests
{
    private readonly Guid _clienteId = Guid.NewGuid();
    private const string NumeroContaValido = "12345-6";

    [Fact]
    public void Criar_DadosValidos_DeveRetornarContaComSaldoZerado()
    {
        // Act
        var conta = Conta.Criar(NumeroContaValido, _clienteId);

        // Assert
        Assert.NotEqual(Guid.Empty, conta.Id);
        Assert.Equal(NumeroContaValido, conta.NumeroConta);
        Assert.Equal(0m, conta.Saldo.Valor);
        Assert.Equal(_clienteId, conta.ClienteId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Criar_NumeroContaVazioOuNulo_DeveLancarArgumentException(string? numeroConta)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Conta.Criar(numeroConta!, _clienteId));
    }

    [Fact]
    public void Creditar_ValorPositivo_DeveAumentarSaldo()
    {
        // Arrange
        var conta = Conta.Criar(NumeroContaValido, _clienteId);

        // Act
        conta.Creditar(200m);

        // Assert
        Assert.Equal(200m, conta.Saldo.Valor);
    }

    [Fact]
    public void Creditar_MultiplosCreditos_DeveAcumularSaldo()
    {
        // Arrange
        var conta = Conta.Criar(NumeroContaValido, _clienteId);

        // Act
        conta.Creditar(100m);
        conta.Creditar(50m);

        // Assert
        Assert.Equal(150m, conta.Saldo.Valor);
    }

    [Fact]
    public void Creditar_ValorZero_DeveLancarArgumentException()
    {
        // Arrange
        var conta = Conta.Criar(NumeroContaValido, _clienteId);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => conta.Creditar(0m));
    }

    [Fact]
    public void Debitar_ValorValido_DeveDiminuirSaldo()
    {
        // Arrange
        var conta = Conta.Criar(NumeroContaValido, _clienteId);
        conta.Creditar(200m);

        // Act
        conta.Debitar(80m);

        // Assert
        Assert.Equal(120m, conta.Saldo.Valor);
    }

    [Fact]
    public void Debitar_SaldoInsuficiente_DeveLancarInvalidOperationException()
    {
        // Arrange
        var conta = Conta.Criar(NumeroContaValido, _clienteId);
        conta.Creditar(50m);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => conta.Debitar(100m));
    }

    [Fact]
    public void Debitar_ValorIgualAoSaldo_DeveZerarSaldo()
    {
        // Arrange
        var conta = Conta.Criar(NumeroContaValido, _clienteId);
        conta.Creditar(100m);

        // Act
        conta.Debitar(100m);

        // Assert
        Assert.Equal(0m, conta.Saldo.Valor);
    }

    [Fact]
    public void CreditarEDebitar_FluxoCompleto_DeveCalcularSaldoCorretamente()
    {
        // Arrange
        var conta = Conta.Criar(NumeroContaValido, _clienteId);

        // Act
        conta.Creditar(500m);
        conta.Debitar(200m);
        conta.Creditar(100m);
        conta.Debitar(50m);

        // Assert
        Assert.Equal(350m, conta.Saldo.Valor);
    }
}
