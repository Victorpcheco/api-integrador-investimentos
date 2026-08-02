namespace Core.ValueObjects;
public record Cpf
{
    public string Numero { get; }

    private Cpf(string numero) => Numero = numero;

    public static Cpf Criar(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("O CPF não pode ser vazio.", nameof(valor));

        var soDigitos = ExtrairDigitos(valor);

        if (soDigitos.Length != 11)
            throw new ArgumentException("O CPF deve conter exatamente 11 dígitos.", nameof(valor));

        if (TodosDigitosIguais(soDigitos))
            throw new ArgumentException("O CPF informado é inválido.", nameof(valor));

        if (!DigitoVerificadorValido(soDigitos))
            throw new ArgumentException("O CPF informado é inválido.", nameof(valor));

        return new Cpf(soDigitos);
    }

    private static string ExtrairDigitos(string valor)
    {
        return new string(valor.Where(char.IsDigit).ToArray());
    }

    private static bool TodosDigitosIguais(string digitos)
    {
        return digitos.Distinct().Count() == 1;
    }

    private static bool DigitoVerificadorValido(string digitos)
    {
        var primeiroDigito = CalcularDigito(digitos, 9);
        var segundoDigito = CalcularDigito(digitos, 10);

        return digitos[9] - '0' == primeiroDigito
            && digitos[10] - '0' == segundoDigito;
    }

    private static int CalcularDigito(string digitos, int tamanho)
    {
        var soma = 0;
        var peso = tamanho + 1;

        for (var i = 0; i < tamanho; i++)
        {
            soma += (digitos[i] - '0') * peso;
            peso--;
        }

        var resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}
