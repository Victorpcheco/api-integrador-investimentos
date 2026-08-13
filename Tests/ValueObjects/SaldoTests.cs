using Core.ValueObjects;

namespace Tests.ValueObjects;

public class SaldoTests
{
    [Fact]
    public void Criar_ValorPositivo_DeveRetornarInstancia()
    {
        // Act
        var saldo = Saldo.Criar(100m);

        // Assert
        Assert.Equal(100m, saldo.Valor);
    }

    [Fact]
    public void Criar_ValorZero_DeveRetornarInstancia()
    {
        // Act
        var saldo = Saldo.Criar(0m);

        // Assert
        Assert.Equal(0m, saldo.Valor);
    }

    [Fact]
    public void Criar_ValorNegativo_DeveLancarArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => Saldo.Criar(-1m));
    }

    [Fact]
    public void Zero_DeveCriarSaldoComValorZero()
    {
        // Act
        var saldo = Saldo.Zero();

        // Assert
        Assert.Equal(0m, saldo.Valor);
    }

    [Fact]
    public void Creditar_ValorPositivo_DeveRetornarNovoSaldoAcrescido()
    {
        // Arrange
        var saldo = Saldo.Criar(100m);

        // Act
        var novoSaldo = saldo.Creditar(50m);

        // Assert
        Assert.Equal(150m, novoSaldo.Valor);
    }

    [Fact]
    public void Creditar_NaoDeveAlterarSaldoOriginal()
    {
        // Arrange
        var saldo = Saldo.Criar(100m);

        // Act
        saldo.Creditar(50m);

        // Assert
        Assert.Equal(100m, saldo.Valor);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Creditar_ValorZeroOuNegativo_DeveLancarArgumentException(decimal valor)
    {
        // Arrange
        var saldo = Saldo.Criar(100m);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => saldo.Creditar(valor));
    }

    [Fact]
    public void Debitar_ValorValido_DeveRetornarNovoSaldoSubtraido()
    {
        // Arrange
        var saldo = Saldo.Criar(100m);

        // Act
        var novoSaldo = saldo.Debitar(30m);

        // Assert
        Assert.Equal(70m, novoSaldo.Valor);
    }

    [Fact]
    public void Debitar_ValorIgualAoSaldo_DeveRetornarSaldoZerado()
    {
        // Arrange
        var saldo = Saldo.Criar(100m);

        // Act
        var novoSaldo = saldo.Debitar(100m);

        // Assert
        Assert.Equal(0m, novoSaldo.Valor);
    }

    [Fact]
    public void Debitar_NaoDeveAlterarSaldoOriginal()
    {
        // Arrange
        var saldo = Saldo.Criar(100m);

        // Act
        saldo.Debitar(30m);

        // Assert
        Assert.Equal(100m, saldo.Valor);
    }

    [Fact]
    public void Debitar_SaldoInsuficiente_DeveLancarInvalidOperationException()
    {
        // Arrange
        var saldo = Saldo.Criar(50m);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => saldo.Debitar(100m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Debitar_ValorZeroOuNegativo_DeveLancarArgumentException(decimal valor)
    {
        // Arrange
        var saldo = Saldo.Criar(100m);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => saldo.Debitar(valor));
    }

    [Fact]
    public void Igualdade_SaldosComMesmoValor_DevemSerIguais()
    {
        // Arrange
        var saldo1 = Saldo.Criar(100m);
        var saldo2 = Saldo.Criar(100m);

        // Act & Assert
        Assert.Equal(saldo1, saldo2);
    }

    [Fact]
    public void Igualdade_SaldosComValoresDiferentes_NaoDevemSerIguais()
    {
        // Arrange
        var saldo1 = Saldo.Criar(100m);
        var saldo2 = Saldo.Criar(200m);

        // Act & Assert
        Assert.NotEqual(saldo1, saldo2);
    }
}
