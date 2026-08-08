using MediatR;

namespace Application.Contas.Commands.RealizarResgate;

public record RealizarResgateCommand(
    Guid ContaId,
    decimal Valor
) : IRequest<RealizarResgateResult>;
