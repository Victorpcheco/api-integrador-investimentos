namespace Application.Contas.Commands.RealizarResgate;

public record RealizarResgateResult(
    Guid TransacaoId,
    decimal SaldoAtualizado,
    DateTime DataTransacao
);
