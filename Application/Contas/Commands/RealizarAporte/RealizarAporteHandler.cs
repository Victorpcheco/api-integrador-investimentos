using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using MediatR;

namespace Application.Contas.Commands.RealizarAporte;

public class RealizarAporteHandler(
    IContaRepository contaRepository,
    ITransacaoContaRepository transacaoContaRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<RealizarAporteCommand, RealizarAporteResult>
{
    public async Task<RealizarAporteResult> Handle(
        RealizarAporteCommand request,
        CancellationToken cancellationToken)
    {
        var conta = await contaRepository.ObterPorIdAsync(request.ContaId, cancellationToken)
            ?? throw new KeyNotFoundException(
                $"Conta corrente com Id '{request.ContaId}' não encontrada.");

        conta.Creditar(request.Valor);

        var transacao = TransacaoConta.Criar(
            conta.Id,
            request.Valor,
            TipoTransacao.Aporte);

        await transacaoContaRepository.AdicionarAsync(transacao, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RealizarAporteResult(
            transacao.Id,
            conta.Saldo.Valor,
            transacao.DataOperacao);
    }
}
