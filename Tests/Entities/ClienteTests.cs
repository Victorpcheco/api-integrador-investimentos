using Core.Entities;

namespace Tests.Entities;

public class ClienteTests
{
    private const string CpfValido = "52998224725";
    private const string NomeValido = "Victor Pacheco";

    [Fact]
    public void Criar_DadosValidos_DeveRetornarClienteComPropriedadesPreenchidas()
    {
        var cliente = Cliente.Criar(NomeValido, CpfValido);

        Assert.NotEqual(Guid.Empty, cliente.Id);
        Assert.Equal(NomeValido, cliente.Nome);
        Assert.Equal(CpfValido, cliente.Cpf.Numero);
        Assert.True(cliente.DataCriacao <= DateTime.UtcNow);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Criar_NomeVazioOuNulo_DeveLancarArgumentException(string? nome)
    {
        Assert.Throws<ArgumentException>(() => Cliente.Criar(nome!, CpfValido));
    }

    [Fact]
    public void Criar_CpfInvalido_DeveLancarArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Cliente.Criar(NomeValido, "00000000000"));
    }

    [Fact]
    public void Criar_DoisClientes_DevemTerIdsDiferentes()
    {
        var cliente1 = Cliente.Criar(NomeValido, CpfValido);
        var cliente2 = Cliente.Criar("Outro Nome", "39053344705");

        Assert.NotEqual(cliente1.Id, cliente2.Id);
    }
}
