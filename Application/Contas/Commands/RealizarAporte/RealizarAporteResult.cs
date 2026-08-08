namespace Application.Contas.Commands.RealizarAporte;

public record RealizarAporteResult(
    Guid TransacaoId,
    decimal SaldoAtualizado,
    DateTime DataTransacao
);
