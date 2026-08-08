using MediatR;

namespace Application.Contas.Commands.RealizarAporte;

public record RealizarAporteCommand(
    Guid ContaId,
    decimal Valor
) : IRequest<RealizarAporteResult>;
