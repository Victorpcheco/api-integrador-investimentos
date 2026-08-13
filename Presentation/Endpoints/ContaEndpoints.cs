using Application.Contas.Commands.RealizarAporte;
using Application.Contas.Commands.RealizarResgate;
using MediatR;

namespace Presentation.Endpoints;

public static class ContaEndpoints
{
    public static void MapContaEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/contas")
            .WithTags("Contas");

        group.MapPost("/{id:guid}/aporte", async (
            Guid id,
            RealizarAporteRequest request,
            IMediator mediator) =>
        {
            var command = new RealizarAporteCommand(id, request.Valor);
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithName("RealizarAporte")
        .WithOpenApi();

        group.MapPost("/{id:guid}/resgate", async (
            Guid id,
            RealizarResgateRequest request,
            IMediator mediator) =>
        {
            var command = new RealizarResgateCommand(id, request.Valor);
            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithName("RealizarResgate")
        .WithOpenApi();
    }
}

public record RealizarAporteRequest(decimal Valor);
public record RealizarResgateRequest(decimal Valor);
