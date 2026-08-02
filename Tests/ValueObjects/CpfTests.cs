using Core.ValueObjects;

namespace Tests.ValueObjects;

public class CpfTests
{
    [Theory]
    [InlineData("52998224725")]
    [InlineData("529.982.247-25")]
    [InlineData("  529.982.247-25  ")]
    public void Criar_CpfValido_DeveRetornarInstancia(string valor)
    {
        var cpf = Cpf.Criar(valor);

        Assert.Equal("52998224725", cpf.Numero);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Criar_ValorVazioOuNulo_DeveLancarArgumentException(string? valor)
    {
        Assert.Throws<ArgumentException>(() => Cpf.Criar(valor!));
    }

    [Theory]
    [InlineData("123")]
    [InlineData("1234567890")]
    [InlineData("123456789012")]
    public void Criar_QuantidadeDigitosInvalida_DeveLancarArgumentException(string valor)
    {
        Assert.Throws<ArgumentException>(() => Cpf.Criar(valor));
    }

    [Theory]
    [InlineData("00000000000")]
    [InlineData("11111111111")]
    [InlineData("22222222222")]
    [InlineData("99999999999")]
    public void Criar_TodosDigitosIguais_DeveLancarArgumentException(string valor)
    {
        Assert.Throws<ArgumentException>(() => Cpf.Criar(valor));
    }

    [Theory]
    [InlineData("52998224720")]
    [InlineData("12345678901")]
    public void Criar_DigitoVerificadorInvalido_DeveLancarArgumentException(string valor)
    {
        Assert.Throws<ArgumentException>(() => Cpf.Criar(valor));
    }

    [Fact]
    public void Criar_DoisCpfsIguais_DevemSerIguaisPorValor()
    {
        var cpf1 = Cpf.Criar("52998224725");
        var cpf2 = Cpf.Criar("529.982.247-25");

        Assert.Equal(cpf1, cpf2);
    }

    [Fact]
    public void Criar_DoisCpfsDiferentes_NaoDevemSerIguais()
    {
        var cpf1 = Cpf.Criar("52998224725");
        var cpf2 = Cpf.Criar("39053344705");

        Assert.NotEqual(cpf1, cpf2);
    }
}
