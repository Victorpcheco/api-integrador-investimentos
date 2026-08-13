using Core.ValueObjects;

namespace Tests.ValueObjects;

public class ValorTransacaoTests
{
    [Fact]
    public void Criar_ValorPositivo_DeveCriarInstanciaComSucesso()
    {
        // Arrange
        decimal valor = 10.50m;

        // Act
        var valorTransacao = ValorTransacao.Criar(valor);

        // Assert
        Assert.Equal(valor, valorTransacao.Valor);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100.5)]
    [InlineData(-0.01)]
    public void Criar_ValorZeroOuNegativo_DeveLancarArgumentException(decimal valorInvalido)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => ValorTransacao.Criar(valorInvalido));
        Assert.Contains("O valor da transação deve ser estritamente maior que zero.", exception.Message);
    }
    
    [Fact]
    public void Igualdade_ValoresIguais_DevemSerConsideradosIguaisPeloRecord()
    {
        // Arrange
        var valor1 = ValorTransacao.Criar(50m);
        var valor2 = ValorTransacao.Criar(50m);

        // Act & Assert
        Assert.Equal(valor1, valor2);
    }
}
