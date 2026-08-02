using Core.Entities;

namespace Tests.Entities;

public class ContaCorrenteTests
{
    private readonly Guid _clienteId = Guid.NewGuid();
    private const string NumeroContaValido = "12345-6";

    [Fact]
    public void Criar_DadosValidos_DeveRetornarContaComSaldoZerado()
    {
        var conta = ContaCorrente.Criar(NumeroContaValido, _clienteId);

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
        Assert.Throws<ArgumentException>(() => ContaCorrente.Criar(numeroConta!, _clienteId));
    }

    [Fact]
    public void Creditar_ValorPositivo_DeveAumentarSaldo()
    {
        var conta = ContaCorrente.Criar(NumeroContaValido, _clienteId);

        conta.Creditar(200m);

        Assert.Equal(200m, conta.Saldo.Valor);
    }

    [Fact]
    public void Creditar_MultiplosCreditos_DeveAcumularSaldo()
    {
        var conta = ContaCorrente.Criar(NumeroContaValido, _clienteId);

        conta.Creditar(100m);
        conta.Creditar(50m);

        Assert.Equal(150m, conta.Saldo.Valor);
    }

    [Fact]
    public void Creditar_ValorZero_DeveLancarArgumentException()
    {
        var conta = ContaCorrente.Criar(NumeroContaValido, _clienteId);

        Assert.Throws<ArgumentException>(() => conta.Creditar(0m));
    }

    [Fact]
    public void Debitar_ValorValido_DeveDiminuirSaldo()
    {
        var conta = ContaCorrente.Criar(NumeroContaValido, _clienteId);
        conta.Creditar(200m);

        conta.Debitar(80m);

        Assert.Equal(120m, conta.Saldo.Valor);
    }

    [Fact]
    public void Debitar_SaldoInsuficiente_DeveLancarInvalidOperationException()
    {
        var conta = ContaCorrente.Criar(NumeroContaValido, _clienteId);
        conta.Creditar(50m);

        Assert.Throws<InvalidOperationException>(() => conta.Debitar(100m));
    }

    [Fact]
    public void Debitar_ValorIgualAoSaldo_DeveZerarSaldo()
    {
        var conta = ContaCorrente.Criar(NumeroContaValido, _clienteId);
        conta.Creditar(100m);

        conta.Debitar(100m);

        Assert.Equal(0m, conta.Saldo.Valor);
    }

    [Fact]
    public void CreditarEDebitar_FluxoCompleto_DeveCalcularSaldoCorretamente()
    {
        var conta = ContaCorrente.Criar(NumeroContaValido, _clienteId);

        conta.Creditar(500m);
        conta.Debitar(200m);
        conta.Creditar(100m);
        conta.Debitar(50m);

        Assert.Equal(350m, conta.Saldo.Valor);
    }
}
