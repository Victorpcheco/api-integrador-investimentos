using Core.ValueObjects;

namespace Tests.ValueObjects;

public class SaldoTests
{
    [Fact]
    public void Criar_ValorPositivo_DeveRetornarInstancia()
    {
        var saldo = Saldo.Criar(100m);

        Assert.Equal(100m, saldo.Valor);
    }

    [Fact]
    public void Criar_ValorZero_DeveRetornarInstancia()
    {
        var saldo = Saldo.Criar(0m);

        Assert.Equal(0m, saldo.Valor);
    }

    [Fact]
    public void Criar_ValorNegativo_DeveLancarArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Saldo.Criar(-1m));
    }

    [Fact]
    public void Zero_DeveCriarSaldoComValorZero()
    {
        var saldo = Saldo.Zero();

        Assert.Equal(0m, saldo.Valor);
    }

    [Fact]
    public void Creditar_ValorPositivo_DeveRetornarNovoSaldoAcrescido()
    {
        var saldo = Saldo.Criar(100m);

        var novoSaldo = saldo.Creditar(50m);

        Assert.Equal(150m, novoSaldo.Valor);
    }

    [Fact]
    public void Creditar_NaoDeveAlterarSaldoOriginal()
    {
        var saldo = Saldo.Criar(100m);

        saldo.Creditar(50m);

        Assert.Equal(100m, saldo.Valor);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Creditar_ValorZeroOuNegativo_DeveLancarArgumentException(decimal valor)
    {
        var saldo = Saldo.Criar(100m);

        Assert.Throws<ArgumentException>(() => saldo.Creditar(valor));
    }

    [Fact]
    public void Debitar_ValorValido_DeveRetornarNovoSaldoSubtraido()
    {
        var saldo = Saldo.Criar(100m);

        var novoSaldo = saldo.Debitar(30m);

        Assert.Equal(70m, novoSaldo.Valor);
    }

    [Fact]
    public void Debitar_ValorIgualAoSaldo_DeveRetornarSaldoZerado()
    {
        var saldo = Saldo.Criar(100m);

        var novoSaldo = saldo.Debitar(100m);

        Assert.Equal(0m, novoSaldo.Valor);
    }

    [Fact]
    public void Debitar_NaoDeveAlterarSaldoOriginal()
    {
        var saldo = Saldo.Criar(100m);

        saldo.Debitar(30m);

        Assert.Equal(100m, saldo.Valor);
    }

    [Fact]
    public void Debitar_SaldoInsuficiente_DeveLancarInvalidOperationException()
    {
        var saldo = Saldo.Criar(50m);

        Assert.Throws<InvalidOperationException>(() => saldo.Debitar(100m));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Debitar_ValorZeroOuNegativo_DeveLancarArgumentException(decimal valor)
    {
        var saldo = Saldo.Criar(100m);

        Assert.Throws<ArgumentException>(() => saldo.Debitar(valor));
    }

    [Fact]
    public void Igualdade_SaldosComMesmoValor_DevemSerIguais()
    {
        var saldo1 = Saldo.Criar(100m);
        var saldo2 = Saldo.Criar(100m);

        Assert.Equal(saldo1, saldo2);
    }

    [Fact]
    public void Igualdade_SaldosComValoresDiferentes_NaoDevemSerIguais()
    {
        var saldo1 = Saldo.Criar(100m);
        var saldo2 = Saldo.Criar(200m);

        Assert.NotEqual(saldo1, saldo2);
    }
}
