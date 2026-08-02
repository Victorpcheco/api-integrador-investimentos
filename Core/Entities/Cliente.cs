using Core.ValueObjects;

namespace Core.Entities;
public class Cliente
{
    public Guid Id { get; private init; }
    public string Nome { get; private set; } = string.Empty;
    public Cpf Cpf { get; private set; } = null!;
    public DateTime DataCriacao { get; private set; }

    public ContaCorrente ContaCorrente { get; private set; } = null!;

    private Cliente() { }

    private Cliente(string nome, Cpf cpf)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Cpf = cpf;
        DataCriacao = DateTime.UtcNow;
    }

    public static Cliente Criar(string nome, string cpf)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do cliente não pode ser vazio.", nameof(nome));

        var cpfValido = Cpf.Criar(cpf);

        return new Cliente(nome, cpfValido);
    }
}
